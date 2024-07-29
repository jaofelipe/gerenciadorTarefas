using GerenciadorTarefas.Data;
using GerenciadorTarefas.Extensions;
using GerenciadorTarefas.Models;
using GerenciadorTarefas.Services;
using GerenciadorTarefas.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;

namespace GerenciadorTarefas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost("register")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Post(
            [FromBody] RegisterViewModel model,
            [FromServices] DataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var userId = Guid.NewGuid();

            var user = new User
            {
                Id = userId,
                Name = model.Name,
                Email = model.Email,
                Slug = model.Email.Replace("@", "-").Replace(".", "-"),
            };

            //var password = PasswordGenerator.Generate(25);
            var password = model.Password;
            user.PasswordHash = PasswordHasher.Hash(password);

            try
            {
                await context.Users.AddAsync(user);

                Guid? roleId = model.IsAdmin ? user.GetRoleId() :
                    context.Roles?.FirstOrDefaultAsync(r => r.Name == "Usuario")?.Result?.Id;

                if (roleId == null)
                {
                    return StatusCode(500, new ResultViewModel<string>("05X04 - RoleId não encontrado"));
                }

                var userRole = new UserRole
                {
                    UserId = userId,
                    RoleId = roleId.Value
                };

                // Adicionando a relação UserRole ao contexto
                await context.UserRoles.AddAsync(userRole);

                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    user = user.Email,
                    password
                }));
            }
            catch (DbUpdateException)
            {
                return StatusCode(400, new ResultViewModel<string>("05X99 - Este e-mail já está cadastrado"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("05X04 - Falha interna no servidor"));
            }

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginViewModel model,
            [FromServices] DataContext context,
            [FromServices] TokenService tokenService)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<string>(ModelState.GetErrors()));

            var user = await context.Users
                .AsNoTracking()
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Email == model.Email);

            if (user == null)
                return StatusCode(401, new ResultViewModel<string>("Usuário ou senha inválidos"));

            if (!PasswordHasher.Verify(user.PasswordHash, model.Password))
                return StatusCode(401, new ResultViewModel<string>("Senha inválida"));

            try
            {
                var token = tokenService.GenerateToken(user);
                return Ok(new ResultViewModel<string>(token, null));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<string>("05X04 - Falha interna no servidor"));
            }

        }
    }
}
