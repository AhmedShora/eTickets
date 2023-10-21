
using eTickets.Data.Base;

namespace eTickets.Models
{
    public class Actor : IEntityBase
    {
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Profile Picture")]
        public string ProfilePictureUrl { get; set; }

        [Display(Name = "Biography")]
        public string Bio { get; set; }

        //relationships
        //m to m
        public List<ActorMovie>? ActorMovies { get; set; }

    }
}
