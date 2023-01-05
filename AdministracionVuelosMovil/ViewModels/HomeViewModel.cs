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
            VerInfoCommand = new Command(VerInfo);
            VerHomeCommand = new Command(VerHome);
            EliminarCommand = new Command(EliminarVuelo);
            Service.Confirmar += Service_Confirmar;

            Rellenar();
        }

        private async void Service_Confirmar(string obj)
        {
            await App.Current.MainPage.DisplayAlert("Informacion", obj,"ACEPTAR");
        }

        private void EliminarVuelo()
        {
        }

        private async void VerInfo()
        {
            await Application.Current.MainPage.Navigation.PushAsync(VistaInfo);

        }

        private void Editar()
        {
        }

        private async void VerEditar()
        {
            await Application.Current.MainPage.Navigation.PushAsync(VistaEdit);

        }

        private void Agregar()
        {
            //Validas los datos
            if(Avion!=null)
            {
                if (EstadoVuelo == "A Tiempo")
                    Avion.Estado =(int) Estado.ATiempo;
                else if (EstadoVuelo == "Retrasado")
                    Avion.Estado = (int)Estado.Retrasado;
                else if (EstadoVuelo == "Aborando")
                    Avion.Estado = (int)Estado.Abordando;
                else 
                    Avion.Estado = (int)Estado.Despego;

                Avion.HorarioLlegada = Avion.HorarioLlegada.Add(HoraLlegada);
                Avion.HorarioSalida = Avion.HorarioSalida.Add(HoraLlegada);
                Service.Agregar(Avion);
                Application.Current.MainPage.Navigation.PopAsync();
                
            }
        }

        private async void VerHome()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void Rellenar()
        {
            VistaAgg = new AgregarView() { BindingContext =this};
            VistaEdit = new EditView() { BindingContext =this};
            VistaInfo = new InfoView() { BindingContext = this };
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

        public void Actualizar(string nombre)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(nombre)));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
