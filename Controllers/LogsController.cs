using Microsoft.AspNetCore.Mvc;
using Module32.Models.Db;

namespace Module32.Controllers
{
    public class LogsController : Controller
    {
        private IRequestRepository _requestRepository;
        public LogsController(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }
        
        public async Task<IActionResult> Index()
        {
            var requests = await _requestRepository.GetRequests();
            return View(requests);
        }
    }
}
