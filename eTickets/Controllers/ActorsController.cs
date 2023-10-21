using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _actorsService;
        public ActorsController(IActorsService actorsService)
        {
            _actorsService = actorsService;
        }

        public async Task<IActionResult> Index()
        {
            var actors = await _actorsService.GetAllAsync();
            return View(actors);
        }

        //we delete async task because there is no data manipulation
        //Get: Actors/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Actor actor)
        {
            if (!ModelState.IsValid)
                return View(actor);
            await _actorsService.AddAsync(actor);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var actor = await _actorsService.GetByIdAsync(id);
            if (actor == null)
                return View("NotFound");
            return View(actor);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var actor = await _actorsService.GetByIdAsync(id);
            if (actor == null)
                return View("NotFound");
            return View(actor);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (!ModelState.IsValid)
                return View(actor);
            await _actorsService.UpdateAsync(id, actor);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var actor = await _actorsService.GetByIdAsync(id);
            if (actor == null)
                return View("NotFound");
            return View(actor);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var actor = await _actorsService.GetByIdAsync(id);
            if (actor == null)
                return View("NotFound");
            await _actorsService.RemoveAsync(id);
            return RedirectToAction("Index");
        }

    }
}
