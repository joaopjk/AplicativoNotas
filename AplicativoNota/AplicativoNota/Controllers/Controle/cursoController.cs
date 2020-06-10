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

namespace AplicativoNota.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class cursoController : ControllerBase
    {
        public ICursoRepository _repo;
        public cursoController(ICursoRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetAllCurso")]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            try
            {
                var curso = await _repo.GetAllCurso();
                if(curso.Length == 0)
                {
                    return NotFound(MSG.NaoExisteCurso);
                }
                return Ok(curso);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status501NotImplemented, MSG.BancoDadosFalhou);
            }
        }
        [HttpGet("{Id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get (int Id)
        {
            try
            {
                var curso = await _repo.GetCursoById(Id);
                if (curso == null)
                {
                    return NotFound(MSG.NaoExisteCurso);
                }
                return Ok(curso);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status501NotImplemented, MSG.BancoDadosFalhou);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post (Curso Request)
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
                    return Created($"/api/[controler]/Curso{Request.Id}", Request);
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
                var curso = await _repo.GetCursoById(Request.Id);
                if(curso == null)
                {
                    return NotFound(MSG.NaoExisteCurso);
                }
                _repo.Update(Request);
                if(await _repo.SaveChangesAsync())
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
                var curso = await _repo.GetCursoById(id);
                if(curso == null)
                {
                    return NotFound(MSG.NaoExisteCurso);
                }
                _repo.Delete(curso);
                if(await _repo.SaveChangesAsync())
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