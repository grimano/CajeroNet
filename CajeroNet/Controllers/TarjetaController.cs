using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CajeroNet.Models;
using Newtonsoft.Json;
using CajeroNet.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CajeroNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaController : ControllerBase
    {
        private readonly dbContext _context;

        public TarjetaController(dbContext context)
        {
            _context = context;
        }

        // GET: api/<TarjetaController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TarjetaController>/5
        [HttpGet("{idTarjeta}")]
        public string Get(string idTarjeta)
        {
            Respuesta re = new Respuesta();
            Tarjeta ta = null;
            re.mensaje = "";
            try
            {
                ta = _context.Tarjeta.First(t => t.Numero == idTarjeta);
            }
            catch
            {
                re.mensaje = "La tarjeta no existe";
            }
            if (ta is not null) {
                re.obj = ta;
                if (ta.Bloqueada > 0) {
                    re.mensaje = "La tarjeta se encuentra bloqueada";
                }
            }
            return JsonConvert.SerializeObject(re);
        }

        // POST api/<TarjetaController>
        [HttpPost]
        public string Post(TarjetaRequest data)
        {
            Respuesta re = new Respuesta();
            Tarjeta ta = null;
            re.mensaje = "";
            try
            {
                ta = _context.Tarjeta.First(t => t.Numero == data.numero);
            }
            catch
            {
                re.mensaje = "La tarjeta no existe";
            }

            if (ta is not null)
            {
                re.obj = ta;
                if (ta.Bloqueada > 0)
                {
                    re.mensaje = "La tarjeta se encuentra bloqueada";
                }
                else {
                    if (ta.Clave != data.Clave)
                    {
                        ta.Intentos += 1;
                        re.mensaje = "Clave incorrecta";
                    }
                    if (ta.Intentos == 4)
                    {
                        ta.Bloqueada = 1;
                        re.mensaje = "La tarjeta se encuentra bloqueada";
                    }
                    _context.SaveChanges();
                }   
            }
            return JsonConvert.SerializeObject(re);
        }

        // PUT api/<TarjetaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TarjetaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
