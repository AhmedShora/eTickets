using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersService _producersService;

        public ProducersController(IProducersService producersService)
        {
            _producersService = producersService;
        }



        public async Task<IActionResult> Index()
        {
            var producers = await _producersService.GetAllAsync();
            return View("Index", producers);
        }

        public async Task<IActionResult> Details(int id)
        {
            var producerDetails = await _producersService.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producer producer)
        {
            if (!ModelState.IsValid) return View(producer);
            await _producersService.AddAsync(producer);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var producer = await _producersService.GetByIdAsync(id);
            if (producer == null)
                return View("NotFound");
            return View(producer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Producer producer)
        {
            if (!ModelState.IsValid)
                return View(producer);
            await _producersService.UpdateAsync(id, producer);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var producer = await _producersService.GetByIdAsync(id);
            if (producer == null)
                return View("NotFound");
            return View(producer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteActor(int id)
        {
            var producer = await _producersService.GetByIdAsync(id);
            if (producer == null)
                return View("NotFound");
            await _producersService.RemoveAsync(id);
            return RedirectToAction("Index");
        }
    }
}
