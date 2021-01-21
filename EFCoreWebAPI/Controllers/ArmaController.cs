using System.Threading.Tasks;
using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArmaController: ControllerBase
    {
        private readonly IRepository<Arma> _repo;
        public ArmaController(IRepository<Arma> repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _repo.GetAll());
        }
    }
}