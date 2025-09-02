using Microsoft.AspNetCore.Mvc;
using ShopTARgv24KMelnikov.Data;

namespace ShopTARgv24KMelnikov.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly ShopTARgv24KMelnikovContext _context;

        public SpaceshipsController
            (
                ShopTARgv24KMelnikovContext context
            )
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var result = _context.Spaceships
                .Select(x => new Models.Spaceships.SpaceshipsIndexViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    TypeName = x.TypeName,
                    BuiltDate = x.BuiltDate,
                });

            return View(result);
        }
    }
}
