using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MiniETicaretAPI.API.Configurations.CustomColumnWriters;
using MiniETicaretAPI.API.Extensions;
using MiniETicaretAPI.API.Filters;
using MiniETicaretAPI.Application;
using MiniETicaretAPI.Application.Validators.Products;
using MiniETicaretAPI.Infrastructure;
using MiniETicaretAPI.Infrastructure.Filters;
using MiniETicaretAPI.Infrastructure.Services.Storage.Azure;
using MiniETicaretAPI.Persistence;
using MiniETicaretAPI.SignalR;
using Serilog;
using Serilog.Context;
using Serilog.Sinks.PostgreSQL;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddSignalRServices();

builder.Services.AddStorage<AzureStorage>();
//builder.Services.AddStorage(StorageType.Local);

var logger = new Serilog.LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(
        builder.Configuration.GetConnectionString("PostgreSQL"),
        "logs",
        needAutoCreateTable: true,
        columnOptions: new Dictionary<string, ColumnWriterBase>
        {
            {"message", new RenderedMessageColumnWriter() },
            {"message_template", new MessageTemplateColumnWriter() },
            {"level", new LevelColumnWriter() },
            {"time_stamp", new TimestampColumnWriter() },
            {"exception", new ExceptionColumnWriter() },
            {"log_event", new LogEventSerializedColumnWriter() },
            {"user_name", new UsernameColumnWriter() },
        })
    .WriteTo.Seq(builder.Configuration["Seq:ServerUrl"])
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(logger);

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()
));

builder.Services.AddControllers(options =>
    {
        options.Filters.Add<ValidationFilter>();
        options.Filters.Add<RolePermissionFilter>();
    })
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Admin", options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
        NameClaimType = ClaimTypes.Name
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseStaticFiles();

app.UseSerilogRequestLogging();

app.UseHttpLogging();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    var userName = context.User?.Identity?.IsAuthenticated != null || true ? context!.User!.Identity!.Name : null;

    LogContext.PushProperty("user_name", userName);

    await next();
});

app.MapControllers();

app.MapHubs();

app.Run();
