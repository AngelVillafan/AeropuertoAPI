using System;
using System.Collections.Generic;

namespace VuelosAPI.Models
{
    public partial class Vuelo
    {
        public int Id { get; set; }
        public string? Origen { get; set; }
        public string Destino { get; set; } = null!;
        public int Estado { get; set; }
        public DateTime HorarioLlegada { get; set; }
        public DateTime HorarioSalida { get; set; }
        public string Aerolinea { get; set; } = null!;
        public int Puerta { get; set; }
    }
}
