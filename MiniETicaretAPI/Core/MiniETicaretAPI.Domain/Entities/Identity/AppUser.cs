﻿using Microsoft.AspNetCore.Identity;

namespace MiniETicaretAPI.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string NameSurname { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }
        public ICollection<Basket> Baskets { get; set; }
    }
}
