using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShopTARgv24KMelnikov.Core.Dto;
using ShopTARgv24KMelnikov.Core.ServiceInterface;
using ShopTARgv24KMelnikov.Data;
using ShopTARgv24KMelnikov.Models.Spaceships;

namespace ShopTARgv24KMelnikov.Controllers
{
    public class SpaceshipsController : Controller
    {
        private readonly ShopTARgv24KMelnikovContext _context;
        private readonly ISpaceshipsServices _spaceshipsServices;
        public SpaceshipsController
            (
                ShopTARgv24KMelnikovContext context,
                ISpaceshipsServices spaceshipsServices
            )
        {
            _context = context;
            _spaceshipsServices = spaceshipsServices;
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
                    Crew = x.Crew
                });

            return View(result);
        }

        [HttpGet]
        public IActionResult Create()
        {
            SpaceshipsCreateViewModel result = new();

            return View("Create", result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpaceshipsCreateViewModel vm)
        {
            var dto = new SpaceshipDto()
            {
                Id = vm.Id,
                Name = vm.Name,
                TypeName = vm.TypeName,
                BuiltDate = vm.BuiltDate,
                Crew = vm.Crew,
                EnginePower = vm.EnginePower,
                Passengers = vm.Passengers,
                InnerVolume = vm.InnerVolume,
                CreatedAt = vm.CreatedAt,
                UpdatedAt = vm.UpdatedAt,
                ModifiedAt = vm.ModifiedAt
            };

            var result = await _spaceshipsServices.Create(dto);


            if (result == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult>Delete(Guid id)
        {
            var spaceship = await _spaceshipsServices.DetailAsync(id);

            if(spaceship == null)
            {
                return NotFound();
            }

            var vm = new SpaceshipsDeleteViewModel();

            vm.Id = spaceship.Id;
            vm.Name = spaceship.Name;
            vm.TypeName = spaceship.TypeName;
            vm.BuiltDate = spaceship.BuiltDate;
            vm.Crew = spaceship.Crew;
            vm.EnginePower = spaceship.EnginePower;
            vm.Passengers = spaceship.Passengers;
            vm.InnerVolume = spaceship.InnerVolume;
            vm.CreatedAt = spaceship.CreatedAt;
            vm.UpdatedAt = spaceship.UpdatedAt;
            vm.ModifiedAt = spaceship.ModifiedAt;

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(Guid id)
        {
            var spaceship = await _spaceshipsServices.Delete(id);
            if (spaceship == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
