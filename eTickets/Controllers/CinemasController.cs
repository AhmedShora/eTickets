using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasService _cinemasService;

        public CinemasController(ICinemasService cinemasService)
        {
            _cinemasService = cinemasService;
        }

        public async Task<IActionResult> Index()
        {
            var cinemas = await _cinemasService.GetAllAsync();
            return View(cinemas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cinema cinema)
        {
            if (!ModelState.IsValid) return View(cinema);
            await _cinemasService.AddAsync(cinema);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var cinemaDetails = await _cinemasService.GetByIdAsync(id);
            if (cinemaDetails == null) return View("NotFound");
            return View(cinemaDetails);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cinema = await _cinemasService.GetByIdAsync(id);
            if (cinema == null)
                return View("NotFound");
            return View(cinema);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cinema cinema)
        {
            if (!ModelState.IsValid)
                return View(cinema);
            await _cinemasService.UpdateAsync(id, cinema);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await _cinemasService.GetByIdAsync(id);
            if (cinema == null)
                return View("NotFound");
            return View(cinema);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCinema(int id)
        {
            var cinema = await _cinemasService.GetByIdAsync(id);
            if (cinema == null)
                return View("NotFound");
            await _cinemasService.RemoveAsync(id);
            return RedirectToAction("Index");
        }

    }
}
