using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValueController: ControllerBase
    {
        private readonly HeroiContext _context;
        public ValueController(HeroiContext context)
        {
            _context = context;
        }

        [HttpGet("{name}")]
        public ActionResult Get(string name)
        {
            var heroi = new Heroi 
            { 
                Nome = name
            };
            _context.Herois.Add(heroi);
            _context.SaveChanges();
            
            return Ok();
        }
    }
}