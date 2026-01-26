namespace Project.Core.Entities.SPModels.CardOperations
{
    public class SP_GetMenuWithModule
    {
        public int? MasterId { get; set; }
        public int? SubId { get; set; }
        public string MasterName { get; set; }
        public string? Subname { get; set; }
        public string ModelName { get; set; }
        public short? Orderby { get; set; }
    }
}
