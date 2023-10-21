//namespace eTickets.Data.Services
//{
//    public class ActorsService : IActorsService
//    {
//        private readonly AppDbContext _context;

//        public ActorsService(AppDbContext context)
//        {
//            this._context = context;
//        }
//        public async Task AddActor(Actor actor)
//        {
//            await _context.Actors.AddAsync(actor);
//            await _context.SaveChangesAsync();
//        }

//        public async Task<Actor> GetActor(int id)
//        {
//            var actor = await _context.Actors.SingleOrDefaultAsync(x => x.Id == id);
//            return actor;
//        }

//        public async Task<IEnumerable<Actor>> GetActors()
//        {
//            var actors = await _context.Actors.ToListAsync();
//            return actors;
//        }

//        public async Task RemoveActor(int id)
//        {
//            var actor = await _context.Actors.SingleOrDefaultAsync(_ => _.Id == id);
//            _context.Actors.Remove(actor);
//            await _context.SaveChangesAsync();
//        }

//        public async Task<Actor> UpdateActor(int id, Actor newActor)
//        {
//            _context.Update(newActor);
//            await _context.SaveChangesAsync();
//            return newActor;
//        }
//    }
//}
