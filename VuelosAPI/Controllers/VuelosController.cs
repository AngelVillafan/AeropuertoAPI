using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VuelosAPI.DTOs;
using VuelosAPI.Models;
using VuelosAPI.Repositories;

namespace VuelosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VuelosController : ControllerBase
    {
        VeulosAPIRepository<Vuelo> repository;

        public VuelosController(sistem21_aeromexicoContext context)
        {
            repository = new(context);
        }

        [HttpGet]
        public ActionResult Get()
        {

            CambiarEstado();

            var v = repository.Get().OrderBy(x => x.HorarioLlegada);

            if (v.Count() == 0)
                return NotFound();

            return Ok(v);
        }

        private void CambiarEstado()
        {
            var ahora = DateTime.Now;
            var v = repository.Get().Where(x => x.HorarioSalida < ahora).ToList();
            for (int i = 0; i < v.Count(); i++)
            {
                Vuelo actual = v[i];

                actual.Estado = (int)Estado.Retrasado;
                repository.Update(actual);

            }
        }

        [HttpPost]
        public ActionResult Post(VuelosDTO v)
        {
            var errores = "";
            //Agregar dos horas
            DateTime horavalida = DateTime.Now;

            if (v == null)
                return BadRequest("Ingrese un vuelo para continuar");

            if (v.Estado < 0)
                errores += "Debe escribir un estado para poder agregar un vuelo." + Environment.NewLine;
            if (string.IsNullOrWhiteSpace(v.Destino))
                errores += "Debe escribir un destino para poder agregar un vuelo." + Environment.NewLine;
            if (string.IsNullOrWhiteSpace(v.Aerolinea))
                errores += "Debe escribir una aerolinea para poder agregar un vuelo." + Environment.NewLine;
            if (string.IsNullOrWhiteSpace(v.Origen))
                errores += "Debe escribir un origen para poder agregar un vuelo." + Environment.NewLine;
            if (v.HorarioLlegada < horavalida)
                errores += "Fecha de llegada invalida." + Environment.NewLine;
            if (v.HorarioSalida < v.HorarioLlegada)
                errores += "Fechas de llegada y de salida invalidas." + Environment.NewLine;
            if (repository.Get().Select(x => x.Puerta).Any(x => x == v.Puerta))
                errores += "La puerta ya esta siendo usada." + Environment.NewLine;

            if (errores == "")
            {
                Vuelo vuelo = new Vuelo()
                {
                    Aerolinea = v.Aerolinea,
                    Destino = v.Destino,
                    Estado = v.Estado,
                    HorarioSalida = v.HorarioSalida,
                    HorarioLlegada = v.HorarioLlegada,
                    Origen = v.Origen,
                    Puerta = v.Puerta
                };
                repository.Insert(vuelo);
                return Ok();
            }

            return BadRequest(errores);
        }

        [HttpPut]
        public ActionResult Put(VuelosDTO v)
        {
            var errores = "";
            //Agregar dos horas
            DateTime horavalida = DateTime.Now;

            var oldvuelo = repository.Get(v.Id);

            if (oldvuelo == null)
                return BadRequest("Ingrese un vuelo para continuar");

            if (v.Estado < 0)
                errores += "Debe escribir un estado para poder agregar un vuelo." + Environment.NewLine;
            if (string.IsNullOrWhiteSpace(v.Aerolinea))
                errores += "Debe escribir una aerolinea para poder agregar un vuelo." + Environment.NewLine;
            if (string.IsNullOrWhiteSpace(v.Origen))
                errores += "Debe escribir un origen para poder agregar un vuelo." + Environment.NewLine;
            if (string.IsNullOrWhiteSpace(v.Destino))
                errores += "Debe escribir destino para poder agregar un vuelo." + Environment.NewLine;
            //if (v.HorarioLlegada < horavalida)
            //    errores += "Fecha de llegada invalida." + Environment.NewLine;
            if (v.HorarioSalida < v.HorarioLlegada)
                errores += "Fechas de llegada y de salida invalidas." + Environment.NewLine;
            if (repository.Get().Select(x => new Vuelo { Id = x.Id, Puerta = x.Puerta }).Any(x => x.Puerta == v.Puerta && v.Id != x.Id))
                errores += "La puerta ya esta siendo usada." + Environment.NewLine;

            if (errores == "")
            {
                oldvuelo.Estado = v.Estado;
                oldvuelo.Aerolinea = v.Aerolinea;
                oldvuelo.Origen = v.Origen;
                oldvuelo.Puerta = v.Puerta;
                oldvuelo.HorarioLlegada = v.HorarioLlegada;
                oldvuelo.HorarioSalida = v.HorarioSalida;
                oldvuelo.Destino = v.Destino;

                repository.Update(oldvuelo);
                return Ok();
            }

            return BadRequest(errores);
        }

        public IActionResult Delete(VuelosDTO v)
        {
            if (v == null)
                return BadRequest("Ingrese un vuele e intente de nuevo");

            var vuelo = repository.Get(v.Id);

            if (vuelo == null)
                return NotFound();

            //Si se encontro el vuelo y se pudo eliminar
            repository.Delete(vuelo);
            return Ok();

        }
    }
}
