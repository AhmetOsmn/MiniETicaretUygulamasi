using FluentValidation;
using MiniETicaretAPI.Application.ViewModels.Products;

namespace MiniETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator:  AbstractValidator<CreateProductVM>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                    .WithMessage("Ürün adı boş olmamalı.")
                .NotNull()
                    .WithMessage("Ürün adı boş olmamalı.")
                .MaximumLength(150)
                .MinimumLength(5)
                    .WithMessage("Lütfen ürün adını 5 ile 150 karakter arasında giriniz.");

            RuleFor(p => p.Stock)
                .NotEmpty()
                    .WithMessage("Stok bilgisi boş olmamalı.")
                .NotNull()
                    .WithMessage("Stok bilgisi boş olmamalı.")
                .Must(s => s >= 0)
                    .WithMessage("Stok bilgisi negatif olamaz.");

            RuleFor(p => p.Price)
                .NotEmpty()
                    .WithMessage("Fiyat bilgisi boş olmamalı.")
                .NotNull()
                    .WithMessage("Fiyat bilgisi boş olmamalı.")
                .Must(s => s >= 0)
                    .WithMessage("Fiyat bilgisi negatif olamaz.");

        }
    }
}
