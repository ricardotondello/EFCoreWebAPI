using System;
using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EFCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroiController : ControllerBase
    {
        private readonly IRepository<Heroi> _repo;
        public HeroiController(IRepository<Heroi> repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _repo.GetAll());
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        [HttpGet("{id}", Name="Get")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _repo.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Heroi model)
        {
            try
            {
                _repo.Update(model);
                await _repo.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Heroi model)
        {
            try
            {
                if (await _repo.GetById(model.Id) == default) throw new Exception($"Heroi com o id: {id} nao existe");
                _repo.Update(model);
                await _repo.SaveChangesAsync();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await _repo.GetById(id);
                if (model == default) throw new Exception($"Heroi com o id: {id} nao existe");

                _repo.Update(model);
                await _repo.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }
    }
}