﻿namespace Domain.DTO
{
    public class AppSettings
    {
        public Jwt Jwt { get; set; } = new();
    }

    public class Jwt
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SigningKey { get; set; } = string.Empty;
        public int ExpiredTimeMinutes { get; set; }
    }
}
