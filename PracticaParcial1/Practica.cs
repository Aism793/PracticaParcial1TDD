using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PracticaParcial1
{
    public class Practica
    {
        /*
        Escenario: Valor de consignaci�n -1
        H1: COMO Cajero del Banco QUIERO realizar consignaciones a una cuenta de ahorro PARA salvaguardar el dinero.
        Criterio de Aceptaci�n:
        1.2 El valor de la consignaci�n no puede ser menor o igual a 0.
        //El ejemplo o escenario
        Dado El cliente tiene una cuenta de ahorro 
        N�mero 10001, Nombre �Cuenta ejemplo�, Saldo de 0
        Cuando Va a consignar un valor -1
        Entonces El sistema presentar� el mensaje. �El valor a consignar es incorrecto�
         */
        [Test]
        public void NoPuedeConsignarValorMenosUno()
        {
            //Arrange - Preparar la prueba
            var cuentaAhorro = new CuentaAhorro(numero: "100", nombre: "Ejemplo", ciudad: "Valle");
            decimal valorConsignacion = -1;

            //Act - Ejecutar la prueba
            string resultado = cuentaAhorro.Consignar(valorConsignacion: valorConsignacion, fecha: new DateTime(2020, 2, 1), ciudad: "Valle");

            //Assert - Validar la prueba
            Assert.AreEqual("El valor a consignar es incorrecto", resultado);
        }

        /*
          Escenario: Consignaci�n Inicial Correcta
            HU: Como Usuario quiero realizar consignaciones a una cuenta de ahorro para salvaguardar el 
            dinero.
            Criterio de Aceptaci�n:
           
            1.1 La consignaci�n inicial debe ser mayor o igual a 50 mil pesos
            1.3 El valor de la consignaci�n se le adicionar� al valor del saldo aumentar�

            Dado El cliente tiene una cuenta de ahorro 
            N�mero 10001, Nombre �Cuenta ejemplo�, Saldo de 0
            Cuando Va a consignar el valor inicial de 50 mil pesos 
            Entonces El sistema registrar� la consignaci�n
            AND presentar� el mensaje. �Su Nuevo Saldo es de $50.000,00 pesos m/c�.
         */
        [Test]
        public void PuedeHacerConsignacionInicialCorrecta()
        {
            var cuentaAhorro = new CuentaAhorro(numero: "100", nombre: "Ejemplo", ciudad: "Valle");
            decimal valorConsignacion = 50000;
            string respuesta = cuentaAhorro.Consignar(valorConsignacion: valorConsignacion, fecha: new DateTime(2020, 2, 1), ciudad: "Valle");
            Assert.AreEqual(1, cuentaAhorro.Movimientos.Count);//Criterio general
            Assert.AreEqual("Su Nuevo Saldo es de 50000 pesos m/c", respuesta);
        }
    }

    internal class CuentaAhorro
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

        internal string Consignar(decimal valorConsignacion, DateTime fecha, string ciudad)
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

    internal class Movimiento
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