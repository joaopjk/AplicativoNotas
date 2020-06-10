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
    public class disciplinaController : ControllerBase
    {
        public IDisciplinaRepository _repo;
        public disciplinaController(IDisciplinaRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetAllDisciplina")]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            try
            {
                var disciplina = await _repo.GetAllDisciplina();
                if (disciplina.Length == 0)
                {
                    return NotFound(MSG.NaoExisteDisciplina);
                }
                return Ok(disciplina);
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
                var disciplina = await _repo.GetDisciplinaById(Id);
                if (disciplina == null)
                {
                    return NotFound(MSG.NaoExisteDisciplina);
                }
                return Ok(disciplina);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status501NotImplemented, MSG.BancoDadosFalhou);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(Disciplina Request)
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
                    return Created($"/api/[controler]/Disciplina{Request.Id}", Request);
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
        public async Task<IActionResult> Put(Disciplina Request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Request);
                }
                var disciplina = await _repo.GetDisciplinaById(Request.Id);
                if (disciplina == null)
                {
                    return NotFound(MSG.NaoExisteDisciplina);
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
                var disciplina = await _repo.GetDisciplinaById(id);
                if (disciplina == null)
                {
                    return NotFound(MSG.NaoExisteCurso);
                }
                _repo.Delete(disciplina);
                if (await _repo.SaveChangesAsync())
                {
                    return Ok(MSG.DeleteDisciplina);
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
