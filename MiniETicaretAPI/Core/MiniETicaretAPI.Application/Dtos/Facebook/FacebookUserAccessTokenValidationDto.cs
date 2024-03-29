﻿using System.Text.Json.Serialization;

namespace MiniETicaretAPI.Application.Dtos.Facebook
{
    public class FacebookUserAccessTokenValidationDto
    {
        [JsonPropertyName("data")]
        public FacebookUserAccessTokenValidationData Data { get; set; }
    }
    public class FacebookUserAccessTokenValidationData
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; }

        [JsonPropertyName("is_valid")]
        public bool IsValid { get; set; }
    }
}
