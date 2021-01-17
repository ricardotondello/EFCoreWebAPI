using System;
using System.Linq;
using System.Threading.Tasks;
using EFCore.Domain;
using EFCore.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BatalhaController: ControllerBase
    {
        private readonly IRepository _repo;
        public BatalhaController(IRepository repo)
        {
            _repo = repo;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _repo.GetAllBatalhas());
            }
            catch(Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        [HttpGet("{id}", Name="GetBatalha")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _repo.GetBatalhaById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post(Batalha model)
        {
            try
            {
                _repo.Update(model);
                await _repo.SaveChangeAsync();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Batalha model)
        {
            try
            {
                if (await _repo.GetBatalhaById(id) == default) throw new Exception($"Batalha com o id: {id} nao existe");
                    
                _repo.Update(model);
                await _repo.SaveChangeAsync();

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
                var model = await _repo.GetBatalhaById(id);
                if (model == default) throw new Exception($"Batalha com o id: {id} nao existe");
                _repo.Delete(model);
                await _repo.SaveChangeAsync();

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }
    }
}