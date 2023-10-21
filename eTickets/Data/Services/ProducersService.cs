namespace eTickets.Data.Services
{
    public class ProducersService : EntityBaseRepo<Producer>,IProducersService
    {
        public ProducersService(AppDbContext context) : base(context)
        {
        }
    }
}
