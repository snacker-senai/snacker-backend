﻿using Snacker.Domain.Entities;

namespace Snacker.Domain.Interfaces
{
    public interface IAuthService
    {
        User ValidateUser(string email, string password);
        object Login(string email, string password);
        object GenerateClientToken(long tableId);
        string GetTokenValue(string token, string claimType);
    }
}
