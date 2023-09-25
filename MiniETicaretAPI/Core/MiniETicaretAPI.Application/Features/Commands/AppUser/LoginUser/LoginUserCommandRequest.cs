﻿using MediatR;

namespace MiniETicaretAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {
        public string UsernameOrEmail { get; set; } 
        public string Password { get; set; } 
    }
}
