using System.Collections.Generic;

namespace SkillSnap.Api.Models
{
    public class PortfolioUser
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Bio { get; set; }
        public string ProfileImageUrl { get; set; }

        public List<Project> Projects { get; set; } = new();
        public List<Skill> Skills { get; set; } = new();
    }
}