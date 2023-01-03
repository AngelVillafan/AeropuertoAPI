using Newtonsoft.Json;
using PantallaVuelos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PantallaVuelos.Services
{
    public class VuelosService
    {
        HttpClient client = new HttpClient()
        {
            BaseAddress = new Uri("https://aeromexico.sistemas19.com/api/vuelos")
        };

        public async Task<List<Vuelo>> Get()
        {
            List<Vuelo>? vuelos = new List<Vuelo>();
            var response =  client.GetAsync("");
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
