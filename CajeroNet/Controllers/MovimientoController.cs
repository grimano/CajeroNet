using CajeroNet.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CajeroNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientoController : ControllerBase
    {
        private readonly dbContext _context;

        public MovimientoController(dbContext context)
        {
            _context = context;
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
                Movimiento mov = new Movimiento();
                mov.idTarjeta = ta.idTarjeta;
                mov.Importe = 0;
                mov.TipoOperacion = "Balance";
                mov.fecha = DateTime.Now;
                re.obj = mov;
                _context.Add(mov);
                _context.SaveChanges();
            }
            return JsonConvert.SerializeObject(re);
        }

    }
}
