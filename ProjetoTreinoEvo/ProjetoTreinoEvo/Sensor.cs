using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoTreinoEvo
{
    public class Sensor
    {
        public int Id { get; set; }

        public string Endereco { get; set; } = string.Empty;

        public string Numeracao { get; set; } = string.Empty;

    }
}
