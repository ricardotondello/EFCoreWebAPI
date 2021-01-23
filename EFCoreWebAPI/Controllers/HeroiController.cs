using System;
using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EFCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class HeroiController : ControllerBase
    {
        private readonly IRepository<Heroi> _repo;
        public HeroiController(IRepository<Heroi> repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<Heroi>))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
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
        [ProducesResponseType((200), Type = typeof(Heroi))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _repo.GetById(id));
        }

        [HttpPost]
        [ProducesResponseType((200), Type = typeof(Heroi))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
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
        [ProducesResponseType((200), Type = typeof(Heroi))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
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
        [ProducesResponseType((200), Type = typeof(Heroi))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
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