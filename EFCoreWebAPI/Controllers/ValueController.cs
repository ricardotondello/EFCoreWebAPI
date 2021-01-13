using System.Linq;
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

        [HttpGet]
        public ActionResult Get()
        {
            var list = _context.Herois.ToList();
            return Ok(list);
        }

        [HttpPut]
        public ActionResult Update(int id)
        {
            var h = _context.Herois.Where(h => h.Id == id).FirstOrDefault();
            if (h == default) return BadRequest();

            _context.Herois.Update(h);
            _context.SaveChanges();
            return Ok(h);

        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var h = _context.Herois.Where(h => h.Id == id).FirstOrDefault();
            if (h == default) return BadRequest();

            _context.Herois.Remove(h);
            _context.SaveChanges();
            return Ok(h);
        }
    }
}