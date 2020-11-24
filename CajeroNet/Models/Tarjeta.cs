using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CajeroNet.Models
{
    public class Tarjeta
    {
        [Key]
        public long idTarjeta { get; set; }
        public string Numero { get; set; }
        public string Clave { get; set; }
        public double Saldo { get; set; }
        public int Intentos { get; set; }
        public Int16 Bloqueada { get; set; }
        public DateTime? fechaVencimiento { get; set; }

    }

    public class TarjetaRequest
    {
        public string numero { get; set; }
        public string Clave { get; set; }
    }

    public class Movimiento
    {
        [Key]
        public long idMovimiento { get; set; }
        public long idTarjeta { get; set; }
        public string TipoOperacion { get; set; }
        public double Importe { get; set; }
        public DateTime? fecha { get; set; }

    }
}
