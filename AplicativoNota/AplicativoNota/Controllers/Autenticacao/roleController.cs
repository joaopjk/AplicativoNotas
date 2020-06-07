using Api.DTO.Autentication;
using Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AplicativoNota.Controllers.Autenticacao
{
    [Route("api/[controller]")]
    [ApiController]
    public class roleController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        public roleController(RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpGet]
        //[AllowAnonymous]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Get()
        {
            return Ok(new
            {
                role = new RoleRequest(),
                updateUserRole = new UpdateUserRole()
            });
        }
        [HttpPost("CreateRole")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateRole(RoleRequest Request)
        {
            try
            {
                var retorno = await _roleManager.CreateAsync(new Role { Name = Request.Name });

                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"ERROR {ex.Message}");
            }
        }
        [HttpPut("UpdateUserRole")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUserRoles(UpdateUserRole model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null)
                {
                    if (model.Delete)
                        await _userManager.RemoveFromRoleAsync(user, model.Role);
                    else
                        await _userManager.AddToRoleAsync(user, model.Role);
                }
                else
                {
                    return Ok("Usuário não encontrado");
                }

                return Ok("Sucesso");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"ERROR {ex.Message}");
            }
        }
    }
}