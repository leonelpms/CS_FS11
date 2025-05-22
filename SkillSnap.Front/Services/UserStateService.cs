using System;
using System.Collections.Generic;

namespace SkillSnap.Front.Services
{
    public class UserStateService
    {
        public string? JwtToken { get; private set; }
        public string? UserId { get; private set; }
        public string? Email { get; private set; }
        public List<string> Roles { get; private set; } = new();

        public event Action? OnChange;

        public void SetUser(string jwtToken, string userId, string email, List<string> roles)
        {
            JwtToken = jwtToken;
            UserId = userId;
            Email = email;
            Roles = roles ?? new();
            NotifyStateChanged();
        }

        public void ClearUser()
        {
            JwtToken = null;
            UserId = null;
            Email = null;
            Roles = new();
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}