using AdministracionVuelosMovil.Models;
using AdministracionVuelosMovil.Services;
using AdministracionVuelosMovil.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministracionVuelosMovil.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {

        public Command VerAgregarCommand { get; set; }
        public Command AgregarCommand { get; set; }
        public Command VerEditarCommand { get; set; }
        public Command EditarCommand { get; set; }
        public Command VerInfoCommand { get; set; }
        public Command VerHomeCommand { get; set; }
        public Command EliminarCommand { get; set; }

        public Vuelo Avion { get; set; }


        public ObservableCollection<Vuelo> ListaDeVuelos { get; set; }
        public ObservableCollection<EstadoDeVuelo> EstadosDeVuelo { get; set; }

        AgregarView VistaAgg;
        EditView VistaEdit;
        InfoView VistaInfo;

        AvionesService Service = new();




        public TimeSpan HoraLlegada { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public string EstadoVuelo { get; set; }
        public HomeViewModel()
        {
            Service = new();
            ListaDeVuelos = new ObservableCollection<Vuelo>(Service.Get().Result);
            VerAgregarCommand = new Command(VerAgregar);
            AgregarCommand = new Command(Agregar);
            VerEditarCommand = new Command(VerEditar);
            EditarCommand = new Command(Editar);
            VerInfoCommand = new Command<Vuelo>(VerInfo);
            VerHomeCommand = new Command(VerHome);
            EliminarCommand = new Command(EliminarVuelo);
            Service.Confirmar += Service_Confirmar;

            Rellenar();
        }

        private async void Service_Confirmar(string obj)
        {
            await App.Current.MainPage.DisplayAlert("Informacion", obj, "ACEPTAR");
        }

        private async void EliminarVuelo()
        {
            if(Avion!=null)
            {
                await Service.Eliminar(Avion);
            }
        }

        private async void VerInfo(Vuelo vuelo)
        {
            Avion = vuelo;
            HoraLlegada = vuelo.HorarioLlegada.TimeOfDay;

            if (Avion.Estado == (int)Estado.ATiempo)
                EstadoVuelo = "A Tiempo";
            else if (Avion.Estado == (int)Estado.Retrasado)
                EstadoVuelo = "Retrasado";
            else if (Avion.Estado == (int)Estado.Abordando)
                EstadoVuelo = "Aborando";
            else
                EstadoVuelo = "Despego";

            HoraSalida = vuelo.HorarioSalida.TimeOfDay;
            Actualizar("");
            await Application.Current.MainPage.Navigation.PushAsync(VistaInfo);

        }

        private void Editar()
        {
        }

        private async void VerEditar()
        {
            await Application.Current.MainPage.Navigation.PushAsync(VistaEdit);

        }

        private async void Agregar()
        {
            //Validas los datos
            if (Avion != null)
            {
                if (EstadoVuelo == "A Tiempo")
                    Avion.Estado = (int)Estado.ATiempo;
                else if (EstadoVuelo == "Retrasado")
                    Avion.Estado = (int)Estado.Retrasado;
                else if (EstadoVuelo == "Aborando")
                    Avion.Estado = (int)Estado.Abordando;
                else
                    Avion.Estado = (int)Estado.Despego;
                Avion.HorarioLlegada = Avion.HorarioLlegada.Date;
                Avion.HorarioSalida = Avion.HorarioSalida.Date;
                Avion.HorarioLlegada = Avion.HorarioLlegada.Add(HoraLlegada);
                Avion.HorarioSalida = Avion.HorarioSalida.Add(HoraSalida);
                //Avion.HorarioSalida = Avion.HorarioSalida.AddHours(2);
                //Avion.HorarioLlegada = Avion.HorarioLlegada.AddHours(2);
                await Service.Agregar(Avion);
                ListaDeVuelos.Clear();
                var vuelos = await Service.Get();
                vuelos.ForEach(x=>ListaDeVuelos.Add(x));
                await Application.Current.MainPage.Navigation.PopAsync();

            }
        }

        private async void VerHome()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void Rellenar()
        {
            VistaAgg = new AgregarView() { BindingContext = this };
            VistaEdit = new EditView() { BindingContext = this };
            VistaInfo = new InfoView() { BindingContext = this };
            EstadosDeVuelo = new();

            EstadoDeVuelo ATiempo = new() { Estado = "A Tiempo" };
            EstadoDeVuelo Retrasado = new() { Estado = "Retrasado" };
            EstadoDeVuelo Abordando = new() { Estado = "Abordando" };
            EstadoDeVuelo Despego = new() { Estado = "Despego" };
            EstadosDeVuelo.Add(ATiempo);
            EstadosDeVuelo.Add(Retrasado);
            EstadosDeVuelo.Add(Abordando);
            EstadosDeVuelo.Add(Despego);
            Actualizar("");
            //    ATiempo = 1,
            //Retrasado = 2,
            //Abordando = 3,
            //Despego = 4
        }

        private async void VerAgregar()
        {
            Avion = new Vuelo();
            Actualizar(nameof(Avion));
            Avion.HorarioSalida = DateTime.Now.Date;
            Avion.HorarioLlegada = DateTime.Now.Date;
            HoraLlegada = DateTime.Now.TimeOfDay;
            HoraSalida = DateTime.Now.TimeOfDay;
            Actualizar("");
            await Application.Current.MainPage.Navigation.PushAsync(VistaAgg);
        }

        public void Actualizar(string nombre="")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(nombre)));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
