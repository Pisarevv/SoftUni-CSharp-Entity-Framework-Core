namespace FastFood.Web.Controllers
{
    using System.Linq;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Data;
    using FastFood.Core.ViewModels.Positions;
    using FastFood.Models;
    using FastFood.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class PositionsController : Controller
    {
        private readonly IPositionsServices positionsServices;
  

        public PositionsController(IPositionsServices positionsServices)
        {
            this.positionsServices = positionsServices;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePositionInputModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }

            await this.positionsServices.CreateAsync(model);

            return RedirectToAction("All", "Positions");
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<PositionsAllViewModel> positions =
               await this.positionsServices.GetAllPositionsAsync();

            return View(positions.ToList());
        }
    }
}
