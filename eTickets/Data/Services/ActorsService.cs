namespace eTickets.Data.Services
{
    public class ActorsService : EntityBaseRepo<Actor>, IActorsService
    {
        public ActorsService(AppDbContext context) : base(context) { }
       

    }
}
