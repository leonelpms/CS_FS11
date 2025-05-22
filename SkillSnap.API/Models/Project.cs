namespace SkillSnap.Api.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Título { get; set; }
        public string Descripción { get; set; }
        public string ImageUrl { get; set; }

        public int PortfolioUserId { get; set; }
        public PortfolioUser PortfolioUser { get; set; }
    }
}