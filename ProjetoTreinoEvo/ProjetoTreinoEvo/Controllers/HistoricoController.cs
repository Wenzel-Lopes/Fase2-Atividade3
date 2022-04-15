using Microsoft.AspNetCore.Mvc;

namespace ProjetoTreinoEvo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoController : ControllerBase
    {
        private readonly DataContext context;

        public HistoricoController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Historico>>> Get()
        {
            return Ok(await context.Funcionarios.Include(c => c.Sensor).ToListAsync());
        }

        [Route("getById")]
        [HttpGet]
        public async Task<ActionResult<List<Historico>>> Get([FromQuery] int id)
        {
            var historico_join = await context.Funcionarios.Include(c => c.Sensor).ToListAsync();

            historico_join = historico_join.FindAll(c => c.Sensor.Id == id);

            if (historico_join == null)
                return BadRequest("Histórico não encontrado.");

            return Ok(historico_join);
        }

        [HttpPost]
        public async Task<ActionResult<List<HistoricoDTO>>> AddHistorico(HistoricoDTO hist)
        {
            var dto = await context.Departamentos.FirstOrDefaultAsync(d => d.Id == hist.IdDepartamento);

            var novo_hist = new Historico();

            novo_hist.Id = hist.Id;
            novo_hist.Status = hist.Nome;
            novo_hist.Foto = hist.Foto;
            novo_hist.Rg = hist.Rg;
            novo_hist.Departamento = dto;

            await context.Funcionarios.AddAsync(novo_hist);
            await context.SaveChangesAsync();

            return Ok(await context.Funcionarios.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<HistoricoDTO>>> UpdateHistorico(HistoricoDTO request)
        {
            var dto = await context.Departamentos.FirstOrDefaultAsync(d => d.Id == request.IdDepartamento);

            var dbhistorico = await context.Funcionarios.FindAsync(request.Id);
            if (dbhistorico == null)
                return BadRequest("Funcionário não encontrado.");

            dbhistorico.Nome = request.Nome;
            dbhistorico.Foto = request.Foto;
            dbhistorico.Rg = request.Rg;
            dbhistorico.Departamento = dto;

            await context.SaveChangesAsync();

            return Ok(await context.Funcionarios.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Historico>>> Delete(int id)
        {
            var dbhistorico = await context.Funcionarios.FirstOrDefaultAsync(h => h.Id == id);
            if (dbhistorico == null)
                return BadRequest("Funcionário não encontrado.");

            context.Funcionarios.Remove(dbhistorico);
            await context.SaveChangesAsync();

            return Ok(await context.Funcionarios.ToListAsync());
        }
    }
}
