using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticaDomain
{
    public class CuentaAhorro
    {
        protected List<Movimiento> _movimientos;
        public string Numero { get; private set; }
        public string Nombre { get; private set; }
        public string Ciudad { get; private set; }
        public decimal Saldo { get; protected set; }

        public CuentaAhorro(string numero, string nombre, string ciudad)
        {
            _movimientos = new List<Movimiento>();
            Numero = numero;
            Nombre = nombre;
            Ciudad = ciudad;
        }
        public IReadOnlyCollection<Movimiento> Movimientos => _movimientos.AsReadOnly();

        public string Consignar(decimal valorConsignacion, DateTime fecha, string ciudad)
        {
            if (valorConsignacion < 0 && Ciudad.Equals(ciudad))
            {
                return "El valor a consignar es incorrecto";
            }
            if (valorConsignacion >= 50000 && !_movimientos.Any() && Ciudad.Equals(ciudad))
            {
                Saldo += valorConsignacion;
                _movimientos.Add(new Movimiento(cuentaAhorro: this, fecha: fecha, tipo: "CONSIGNACION", valor: valorConsignacion));
                return $"Su Nuevo Saldo es de {Saldo} pesos m/c";
            }
            throw new NotImplementedException();
        }
    }

    
}
