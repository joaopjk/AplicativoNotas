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

namespace AplicativoNota.Controllers.Usuarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class professorController : ControllerBase
    {
        IProfessorRepository _repo;
        public professorController(IProfessorRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetAllProfessor")]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repo.GetAllProfessores();
                if(results.Length == 0)
                {
                    return NotFound(MSG.NaoExisteProfessor);
                }
                return Ok(results);
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
                var results = await _repo.GetProfessorById(Id);
                if (results == null)
                {
                    return NotFound(MSG.NaoExisteProfessor);
                }
                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status501NotImplemented, MSG.BancoDadosFalhou);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(Professor Request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Request);
                }
                _repo.Add(Request);
                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/[controler]/Professor{Request.Nome}", Request);
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
        public async Task<IActionResult> Put(Professor Request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Request);
                }
                var professor = _repo.GetProfessorById(Request.Id);
                if(professor == null)
                {
                    return NotFound(MSG.NaoExisteProfessor);
                }
                _repo.Update(Request);
                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/[controler]/Professor{Request.Nome}", Request);
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
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                var professor = _repo.GetProfessorById(Id);
                if(professor == null)
                {
                    return NotFound(MSG.NaoExisteProfessor);
                }
                _repo.Delete(professor);
                if(await _repo.SaveChangesAsync())
                {
                    return Ok(MSG.DeleteProfessor);
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