using AutoMapper;
using GerenciadorTarefas.Data;
using GerenciadorTarefas.Extensions;
using GerenciadorTarefas.Models;
using GerenciadorTarefas.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorTarefas.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class TarefaController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public TarefaController(IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync([FromServices] DataContext context)
        {
            try
            {
                var userIdAuthenticated = User.GetUserId();

                if (userIdAuthenticated == Guid.Empty) return StatusCode(500, new ResultViewModel<List<Tarefa>>("05X04 - Id do Usuário logado não encontrado"));

                var tarefas = User.IsAdmin()
                        ? await context.Tarefas.ToListAsync()
                        : await context.Tarefas
                            .Where(t => t.UserId == userIdAuthenticated)
                            .ToListAsync();

                var retorno = _mapper.Map<List<RetornoTarefaViewModel>>(tarefas);

                return Ok(new ResultViewModel<List<RetornoTarefaViewModel>>(retorno));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Tarefa>>("05X04 - Falha interna no servidor"));
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] Guid id,
            [FromServices] DataContext context)
        {
            try
            {
                var userIdAuthenticated = User.GetUserId();

                var tarefa = await GetTarefaByIdAsync(id, userIdAuthenticated, context);

                if (tarefa == null)
                    return NotFound(new ResultViewModel<Tarefa>("Tarefa não encontrada"));

                var retorno = _mapper.Map<RetornoTarefaViewModel>(tarefa);

                return Ok(new ResultViewModel<RetornoTarefaViewModel>(retorno));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Tarefa>("05X05 - Falha interna no servidor"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(
            [FromBody] TarefaViewModel model,
            [FromServices] DataContext context
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<Tarefa>(ModelState.GetErrors()));

            var userIdAuthenticated = User.GetUserId();

            if (userIdAuthenticated == Guid.Empty) return StatusCode(500, new ResultViewModel<List<Tarefa>>("05X04 - Id do Usuário logado não encontrado"));

            try
            {
                var tarefa = new Tarefa()
                {
                    Id = Guid.NewGuid(),
                    Titulo = model.Titulo,
                    Descricao = model.Descricao,
                    DataVencimento = model.DataVencimento,
                    Responsavel = model.Responsavel,
                    StatusTarefa = model.StatusTarefa,
                    UserId = userIdAuthenticated,
                };

                await context.Tarefas.AddAsync(tarefa);
                await context.SaveChangesAsync();

                var retorno = _mapper.Map<RetornoTarefaViewModel>(tarefa);

                return Created($"{tarefa.Id}", new ResultViewModel<RetornoTarefaViewModel>(retorno));

            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Tarefa>("05XE9 - Não foi possível incluir a tarefa"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Tarefa>("05X10 - Falha interna no servidor"));
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] Guid id,
            [FromBody] TarefaViewModel model,
            [FromServices] DataContext context
            )
        {
            try
            {
                var userIdAuthenticated = User.GetUserId();

                var tarefa = await GetTarefaByIdAsync(id, userIdAuthenticated, context);

                if (tarefa == null)
                    return NotFound(new ResultViewModel<Tarefa>("Tarefa não encontrada"));

                tarefa.Titulo = model.Titulo;
                tarefa.StatusTarefa = model.StatusTarefa;
                tarefa.Responsavel = model.Responsavel;
                tarefa.Descricao = model.Descricao;
                tarefa.DataVencimento = model.DataVencimento;

                context.Tarefas.Update(tarefa);
                await context.SaveChangesAsync();

                var retorno = _mapper.Map<RetornoTarefaViewModel>(tarefa);

                return Ok(new ResultViewModel<RetornoTarefaViewModel>(retorno));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Tarefa>("05XE8 - Não foi possível alterar a tarefa"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Tarefa>("05X11 - Falha interna no servidor"));
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid id,
            [FromServices] DataContext context
            )
        {
            try
            {
                var userIdAuthenticated = User.GetUserId();

                var tarefa = await GetTarefaByIdAsync(id, userIdAuthenticated, context);

                if (tarefa == null)
                    return NotFound(new ResultViewModel<Tarefa>("Tarefa não encontrada"));

                context.Tarefas.Remove(tarefa);
                await context.SaveChangesAsync();

                var retorno = _mapper.Map<RetornoTarefaViewModel>(tarefa);

                return Ok(new ResultViewModel<RetornoTarefaViewModel>(retorno));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<Tarefa>("05XE7 - Não foi possível excluir a tarefa"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Tarefa>("05X12 - Falha interna no servidor"));
            }
        }

        #region Métodos Privados
        private async Task<Tarefa?> GetTarefaByIdAsync(Guid id, Guid userIdAuthenticated, DataContext context)
        {
            if (userIdAuthenticated == Guid.Empty)
                throw new ArgumentException("Id do Usuário logado não encontrado");

            return User.IsAdmin()
                ? await context.Tarefas.FirstOrDefaultAsync(x => x.Id == id)
                : await context.Tarefas.FirstOrDefaultAsync(t => t.UserId == userIdAuthenticated && t.Id == id);
        }
        #endregion


    }
}
