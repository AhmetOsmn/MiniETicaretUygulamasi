﻿using Microsoft.AspNetCore.Identity;

namespace MiniETicaretAPI.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string NameSurname { get; set; }
    }
}
