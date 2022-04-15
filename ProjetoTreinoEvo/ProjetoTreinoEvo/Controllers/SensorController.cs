using Microsoft.AspNetCore.Mvc;

namespace ProjetoTreinoEvo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : Controller

    {
        private readonly DataContext context;

        public SensorController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Sensor>>> Get()
        {
            return Ok(await context.Departamentos.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Sensor>>> Get(int id)
        {
            var sensor = await context.Departamentos.FindAsync(id);
            if (sensor == null)
                return BadRequest("Departamento não encontrado.");
            return Ok(sensor);
        }

        [HttpPost]
        public async Task<ActionResult<List<Sensor>>> AddDepartamento(Sensor sens)
        {
            await context.Departamentos.AddAsync(sens);
            await context.SaveChangesAsync();

            return Ok(await context.Departamentos.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Sensor>>> UpdateDepartamento(Sensor request)
        {
            var dbsensor = await context.Departamentos.FindAsync(request.Id);
            if (dbsensor == null)
                return BadRequest("Departamento não encontrado.");

            dbsensor.Nome = request.Nome;
            dbsensor.Sigla = request.Sigla;

            await context.SaveChangesAsync();

            return Ok(await context.Departamentos.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Sensor>>> Delete(int id)
        {
            var funcionario_join = await context.Funcionarios.Include(c => c.Sensor).ToListAsync();
            funcionario_join = funcionario_join.FindAll(c => c.Sensor.Id == id);
            var dbsensor = await context.Departamentos.FindAsync(id);
            if (dbsensor == null)
            {
                return BadRequest("Departamento não encontrado.");
            }
            if (funcionario_join.Count() == 1) 
            {
                return BadRequest("Não pode deletar o departamento pois contém funcionarios");
            }
            context.Departamentos.Remove(dbsensor);
            await context.SaveChangesAsync();

            return Ok(await context.Departamentos.ToListAsync());
        }
    }
}
