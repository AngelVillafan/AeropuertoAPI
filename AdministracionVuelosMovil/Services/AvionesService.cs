using AdministracionVuelosMovil.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdministracionVuelosMovil.Services
{
    public class AvionesService
    {
        HttpClient cliente;
        public event Action<string> Confirmar;

        public AvionesService()
        {
            cliente = new HttpClient() { BaseAddress = new Uri("https://aeromexico.sistemas19.com") };
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

        public async Task Agregar(Vuelo v)
        {
            var json = JsonConvert.SerializeObject(v);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = cliente.PostAsync("/api/vuelos", content);
            response.Wait();
            if (!response.Result.IsSuccessStatusCode)
            {
                Confirmar.Invoke(await response.Result.Content.ReadAsStringAsync());
            }
        }

        public async Task Eliminar(Vuelo vuelo)
        {

            var stringContent = new StringContent(JsonConvert.SerializeObject(vuelo), Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Delete, "");
            request.Content = stringContent;
            var response = cliente.SendAsync(request);
            response.Wait();
            if (!response.Result.IsSuccessStatusCode)
            {
                Confirmar.Invoke(await response.Result.Content.ReadAsStringAsync());
            }
        }
    }
}
