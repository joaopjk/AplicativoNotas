using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Models;
using Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Util.Utilitarios;

namespace AplicativoNota.Controllers.Controle
{
    [Route("api/[controller]")]
    [ApiController]
    public class discturmaController : ControllerBase
    {
        public IDiscTurmaRepository _repo;
        public discturmaController(IDiscTurmaRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetAllDiscTurma")]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            try
            {
                var discturma = await _repo.GetAllDiscTurma();
                if (discturma.Length == 0)
                {
                    return NotFound(MSG.NaoExisteDiscTurma);
                }
                return Ok(discturma);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status501NotImplemented, MSG.BancoDadosFalhou);
            }
        }
        [HttpGet("{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int Id)
        {
            try
            {
                var discturma = await _repo.GetDiscTurmaById(Id);
                if (discturma == null)
                {
                    return NotFound(MSG.NaoExisteDiscTurma);
                }
                return Ok(discturma);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status501NotImplemented, MSG.BancoDadosFalhou);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(DiscTurma Request)
        {
            try
            {
                _repo.Add(Request);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/[controler]/DiscTurma{Request.Id}", Request);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status501NotImplemented, MSG.BancoDadosFalhou);
            }
            return BadRequest();
        }
        [HttpPut]
        [AllowAnonymous]
        public async Task<IActionResult> Put(DiscTurma Request)
        {
            try
            {
                var discturma = await _repo.GetDiscTurmaById(Request.Id);
                if (discturma == null)
                {
                    return NotFound(MSG.NaoExisteDiscTurma);
                }
                _repo.Update(Request);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/[controler]/DiscTurma{Request.Id}", Request);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status501NotImplemented, MSG.BancoDadosFalhou);
            }
            return BadRequest();
        }
        [HttpDelete]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var discturma = await _repo.GetDiscTurmaById(id);
                if (discturma == null)
                {
                    return NotFound(MSG.NaoExisteDiscTurma);
                }
                _repo.Delete(discturma);
                if (await _repo.SaveChangesAsync())
                {
                    return Ok(MSG.DeleteCurso);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status501NotImplemented, MSG.BancoDadosFalhou);
            }
            return BadRequest();
        }
    }
}
