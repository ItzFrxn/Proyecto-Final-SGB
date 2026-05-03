using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final
{
    public class Credito : Cuenta
    {
        private int numTarjeta;
        private int limite;

        public int NumTarjeta
        {
            get { return this.numTarjeta; }
            set { this.numTarjeta = value; }
        }
        public int Limite
        {
            get { return this.limite; }
            set { this.limite = value; }
        }

        public Credito(string nombre, string apellido, int edad, double saldo = 00.00) : base(nombre, apellido, edad, "Credito", saldo)
        {
        }

        public override void Retirar(double cantidad)
        {
            Saldo -= cantidad;
            Console.WriteLine($"Has retirado {cantidad} de tu cuenta de crédito. Saldo actual: {Saldo}");
        }
    }
}
