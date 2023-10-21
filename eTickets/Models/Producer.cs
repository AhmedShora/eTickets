namespace eTickets.Models
{
    public class Producer : IEntityBase
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Full Name must be between 10 and 50 charactar")]
        public string FullName { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile picture is required !!")]
        public string ProfilePictureUrl { get; set; }
        public string Bio { get; set; }
        //Relationships
        public List<Movie>? Movies { get; set; }

    }
}
