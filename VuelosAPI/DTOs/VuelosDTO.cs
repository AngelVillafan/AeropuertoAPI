namespace VuelosAPI.DTOs
{

    public enum Estado
    {
        ATiempo = 1,
        Retrasado=2,
    }

    public class VuelosDTO
    {
        public int Id { get; set; }
        public string? Origen { get; set; }
        public string Destino { get; set; } ="";
        public int Estado { get; set; }
        public DateTime HorarioLlegada { get; set; }
        public DateTime HorarioSalida { get; set; }
        public string Aerolinea { get; set; } = ""!;
        public int Puerta { get; set; }
    }
}
