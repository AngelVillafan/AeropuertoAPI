using AdministracionVuelosMovil.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministracionVuelosMovil.Services
{
    public class AvionesService
    {
        HttpClient cliente;

        public AvionesService()
        {
            cliente= new HttpClient() { BaseAddress = new Uri("https://aeromexico.sistemas19.com") };
        }

        public async Task<List<Vuelo>> Get()
        {
            List<Vuelo>? vuelos = new List<Vuelo>();
            var response = cliente.GetAsync("/api/vuelos");
            response.Wait();
            if (response.Result.IsSuccessStatusCode)
            {
                var json = await response.Result.Content.ReadAsStringAsync();
                vuelos = JsonConvert.DeserializeObject<List<Vuelo>>(json);
            }

            if (vuelos == null)
                return new List<Vuelo>();
            else
                return vuelos;

        }
    }
}
