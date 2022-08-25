using loan_and_sell_be.Models;
using loan_and_sell_be.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace loan_and_sell_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BeerController : ControllerBase
    {
        private readonly IBeerService _beerService;

        public BeerController(IBeerService beerService) { 
            _beerService = beerService;
        }
        [HttpGet]
        public async Task<IEnumerable<Beer>> Get() => await _beerService.Get();
    }
}
