using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PantallaVuelos.Models
{
    public enum Estado
    {
        ATiempo = 1,
        Retrasado = 2,
        Abordando =3,
        Despego = 4
    }

    public class Vuelo
    {
        public int Id { get; set; }
        public string? Origen { get; set; }
        public string Destino { get; set; } = "";
        public int Estado { get; set; }
        public DateTime HorarioLlegada { get; set; }
        public DateTime HorarioSalida { get; set; }
        public string Aerolinea { get; set; } = ""!;
        public int Puerta { get; set; }
    }
}
