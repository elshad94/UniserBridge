namespace Project.Core.Entities.Abstract
{
    public interface IAuditDto
    {
        public int AuditId { get; set; }

        public string LoprType { get; set; }

        public int? LuserId { get; set; }

        public DateTime? Ldate { get; set; }
    }
}
