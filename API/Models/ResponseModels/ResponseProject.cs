namespace API.Models.ResponseModels
{
    public class ResponseProject
    {
        public int ProjectId { get; set; }
        public int? ProjectStatusId { get; set; }
        public string? ProjectStatus { get; set; }
        public string? ProjectTitle { get; set; }
        public int? CustomerId { get; set; }
        public string? ProjectCustomer { get; set; }
        public int? ProjectDirectBossUserId { get; set; }       
        public string? ProjectDirectBoss { get; set; }
        public int? ProjectImmediateBossUserId { get; set; }
        public string? ProjectImmediateBoss { get; set; }

    }
}
