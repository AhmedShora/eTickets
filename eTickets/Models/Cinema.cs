namespace eTickets.Models
{
    public class Cinema:IEntityBase
    {
        public int Id { get; set; }
        [Display(Name = "Cinema Name")]
        public string Name { get; set; }
        [Display(Name = "Cinema Logo")]
        public string Logo { get; set; }
        [Display(Name = "Cinema Description")]
        public string Description { get; set; }
    }
}
