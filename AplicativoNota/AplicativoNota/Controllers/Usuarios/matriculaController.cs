using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.DTO;
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
    public class matriculaController : ControllerBase
    {
        private IMatriculaRepository _repo;
        public matriculaController(IMatriculaRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetAllMatricula")]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repo.GetAllMatricula();
                if(results.Length == 0)
                {
                    return NotFound(MSG.NaoExisteMatricula);
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
        public async Task<IActionResult>Get(int Id)
        {
            try
            {
                var results = await _repo.GetMatriculaById(Id);
                if(results == null)
                {
                    return NotFound(MSG.NaoExisteMatricula);
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
        public async Task<IActionResult> Post (Matricula Request)
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
                    return Created($"/api/[controler]/Matricula{Request.Tipo}", Request);
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
        public async Task<IActionResult>Put(Matricula Request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Request);
                }
                var matricula = await _repo.GetMatriculaById(Request.Id);
                if(matricula == null)
                {
                    return NotFound(MSG.NaoExisteMatricula);
                }
                _repo.Update(Request);
                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/[controler]/Matricula{Request.Tipo}", Request);
                }
                return BadRequest();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status501NotImplemented, MSG.BancoDadosFalhou);
            }
        }
    }
}