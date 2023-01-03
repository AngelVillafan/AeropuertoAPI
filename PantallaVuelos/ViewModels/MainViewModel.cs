using PantallaVuelos.Models;
using PantallaVuelos.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PantallaVuelos.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Vuelo> Arrivals { get; set; } = new ObservableCollection<Vuelo>();

        VuelosService service;

        public MainViewModel()
        {
            service = new VuelosService();
            Arrivals = new(service.Get().Result);
            DispatcherTimer GetArrivals = new DispatcherTimer();
            GetArrivals.Interval = TimeSpan.FromSeconds(1);
            GetArrivals.Tick += GetArrivals_Tick;
            GetArrivals.Start();
        }

        private void GetArrivals_Tick(object? sender, EventArgs e)
        {
            Arrivals.Clear();
            service.Get().Result.ForEach(x => Arrivals.Add(x));
            Lanzar(nameof(Arrivals));
        }

        public void Lanzar(string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
