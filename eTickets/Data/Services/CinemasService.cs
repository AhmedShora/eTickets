namespace eTickets.Data.Services
{
    public class CinemasService : EntityBaseRepo<Cinema>,ICinemasService
    {
        public CinemasService(AppDbContext context) : base(context)
        {
        }
    }
}
