using Microsoft.AspNetCore.Mvc;

namespace HTTWebApplication.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var joinedData = _context.GetJoinedData();
            return View(joinedData);
        }
    }
}
