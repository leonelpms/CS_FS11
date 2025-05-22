namespace SkillSnap.Api.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Nivel { get; set; }

        public int PortfolioUserId { get; set; }
        public PortfolioUser PortfolioUser { get; set; }
    }
}