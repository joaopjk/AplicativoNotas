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
    public class turmaController : ControllerBase
    {
        public ITurmaRepository _repo;
        public turmaController(ITurmaRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetAllTurmas")]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            try
            {
                var turma = await _repo.GetAllTurmas();
                if (turma.Length == 0)
                {
                    return NotFound(MSG.NaoExisteTurma);
                }
                return Ok(turma);
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
                var turma = await _repo.GetTurmaById(Id);
                if (turma == null)
                {
                    return NotFound(MSG.NaoExisteTurma);
                }
                return Ok(turma);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status501NotImplemented, MSG.BancoDadosFalhou);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(Turma Request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Request);
                }
                _repo.Add(Request);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/[controler]/Turma{Request.Id}", Request);
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
        public async Task<IActionResult> Put(Curso Request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Request);
                }
                var turma = await _repo.GetTurmaById(Request.Id);
                if (turma == null)
                {
                    return NotFound(MSG.NaoExisteTurma);
                }
                _repo.Update(Request);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/[controler]/Curso{Request.Id}", Request);
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
                var curso = await _repo.GetTurmaById(id);
                if (curso == null)
                {
                    return NotFound(MSG.NaoExisteTurma);
                }
                _repo.Delete(curso);
                if (await _repo.SaveChangesAsync())
                {
                    return Ok(MSG.DeleteTurma);
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
