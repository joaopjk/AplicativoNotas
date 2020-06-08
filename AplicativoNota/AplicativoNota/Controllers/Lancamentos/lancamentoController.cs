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


namespace AplicativoNota.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class lancamentoController : ControllerBase
    {
        private ILancamentosRepository _repo;
        public lancamentoController(ILancamentosRepository repo)
        {
            _repo = repo;
        }
        [HttpGet("GetAllNotas")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int AlunoId, int DisciplinaId)
        {
            try
            {
                var lancamentos = await _repo.GetLancamentosById(AlunoId, DisciplinaId);
                if(lancamentos.Length == 0)
                {
                    return NotFound(MSG.NaoExisteLancamento);
                }
                return Ok(lancamentos);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status501NotImplemented, MSG.BancoDadosFalhou);
            }
        }
        [HttpGet("GetAllNotasTipo")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int AlunoId, int DisciplinaId,string Tipo)
        {
            try
            {
                var lancamentos = await _repo.GetLancamentosByIdeTipo(AlunoId, DisciplinaId,Tipo.Trim());
                if (lancamentos.Length == 0)
                {
                    return NotFound(MSG.NaoExisteLancamento);
                }
                return Ok(lancamentos);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status501NotImplemented, MSG.BancoDadosFalhou);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(Lancamentos Request)
        {
            try
            {
                _repo.Add(Request);
                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/[controler]/Lancamentos{Request.Id}", Request);
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
        public async Task<IActionResult> Put(Lancamentos Request)
        {
            try
            {
                var lancamentos = await _repo.GetLancamentosById(Request.AlunoId, Request.DisciplinaId);
                if(lancamentos == null)
                {
                    return NotFound(MSG.NaoExisteLancamentoProcurado);
                }
                _repo.Update(lancamentos);
                if(await _repo.SaveChangesAsync())
                {
                    return Created($"/api/[controler]/Lancamentos{Request.Id}", Request);
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
        public async Task<IActionResult> Delete(int AlunoId,int DisciplinaId)
        {
            try
            {
                var lancamentos = await _repo.GetLancamentosById(AlunoId, DisciplinaId);
                if(lancamentos == null)
                {
                    return NotFound(MSG.NaoExisteLancamento);
                }
                _repo.Delete(lancamentos);
                if(await _repo.SaveChangesAsync())
                {
                    return Ok(MSG.DeleteLancamento);
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