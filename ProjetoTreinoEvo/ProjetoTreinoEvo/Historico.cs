
namespace ProjetoTreinoEvo
{
    public class Historico
    {
        public int Id { get; set; }

        public string Status { get; set; } = string.Empty;

        public string Foto { get; set; } = string.Empty;

        public long Tempo { get; set; }

        public Sensor Sensor { get; set; }

    }
}
