namespace SkillSnap.Api.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string T�tulo { get; set; }
        public string Descripci�n { get; set; }
        public string ImageUrl { get; set; }

        public int PortfolioUserId { get; set; }
        public PortfolioUser PortfolioUser { get; set; }
    }
}