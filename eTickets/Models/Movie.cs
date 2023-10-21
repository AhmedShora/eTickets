

using System;

namespace eTickets.Models
{
    public class Movie:IEntityBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MovieCategory MovieCategory { get; set; }

        //relationships
        //m to m
        public List<ActorMovie> ActorMovies { get; set; }

        //1 to m
        public Cinema Cinema { get; set; }
        public int CinemaId { get; set; }
        //1 to m
        public Producer Producer{ get; set; }
        public int ProducerId { get; set; }



    }
}
