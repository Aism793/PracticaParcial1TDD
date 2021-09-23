using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaDomain
{
    public class Movimiento
    {
        public Movimiento(CuentaAhorro cuentaAhorro, DateTime fecha, string tipo, decimal valor)
        {
            CuentaAhorro = cuentaAhorro;
            Fecha = fecha;
            Tipo = tipo;
            Valor = valor;
        }

        public CuentaAhorro CuentaAhorro { get; private set; }
        public DateTime Fecha { get; private set; }
        public string Tipo { get; private set; }
        public decimal Valor { get; private set; }
    }
}
