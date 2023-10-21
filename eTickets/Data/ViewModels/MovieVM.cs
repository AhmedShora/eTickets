namespace eTickets.Data.ViewModels
{
    public class MovieVM
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        [Display(Name  = "Movie Description")]
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Price Is Required")]
        public double Price { get; set; }
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Select a Category")]
        public MovieCategory MovieCategory { get; set; }
        
        //relations
        public List<int> ActorIds { get; set; }
        public int CinemaId { get; set; }
        public int ProducerId { get; set; }

    }
}
