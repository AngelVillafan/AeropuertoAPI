using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PantallaVuelos.Helpers
{
    public class EstadoConverer : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int estado = (int)value;
            if (estado == 1)
                return "A Tiempo";
            else if (estado == 2)
                return "Retrasado";
            else if (estado == 3)
                return "Aborando";
            else
                return "Despego";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
