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
    public class alunoController : ControllerBase
    {
        private IAlunoRepository _repo;
        public alunoController(IAlunoRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetAllAlunos")]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            try
            {
                var results = await _repo.GetAllAlunos();
                if (results.Length == 0)
                {
                    return NotFound(MSG.NaoExisteAluno);
                }
                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status501NotImplemented, MSG.BancoDadosFalhou);
            }
        }
        [HttpGet("GetAlunosByDisiciplna")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAlunosByDisiciplna(int CursoId,int DisciplinaId)
        {
            try
            {
               var results = await _repo.GetAlunosByDisiciplna(CursoId, DisciplinaId);
                if (results == null)
                {
                    return NotFound(MSG.NaoExisteAluno);
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
                var results = await _repo.GetAlunosById(Id);
                if (results == null)
                {
                    return NotFound(MSG.NaoExisteAluno);
                }
                return Ok(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status501NotImplemented, MSG.BancoDadosFalhou);
            }
        }
        [HttpGet("GetAlunosByCurso")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByCurso(int Id)
        {
            try
            {
                var results = await _repo.GetAlunosByCurso(Id);
                if (results.Length == 0)
                {
                    return NotFound(MSG.NaoExisteAlunoCurso);
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
        public async Task<IActionResult> Post(Aluno Request)
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
                    return Created($"/api/[controler]/Aluno{Request.Nome}", Request);
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
        public async Task<IActionResult> Put(Aluno Request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Request);
                }
                var aluno = await _repo.GetAlunosById(Request.Id);
                if (aluno == null)
                {
                    return NotFound(MSG.NaoExisteAlunoCurso);
                }
                _repo.Update(Request);
                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/[controler]/Aluno{Request.Nome}", Request);
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
                var aluno = await _repo.GetAlunosById(Id);
                if (aluno == null)
                {
                    return NotFound(MSG.NaoExisteAlunoCurso);
                }
                _repo.Delete(aluno);
                if (await _repo.SaveChangesAsync())
                {
                    return Ok(MSG.DeleteAluno);
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