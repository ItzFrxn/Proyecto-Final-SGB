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
        public Credito(int id, int nr, string nombre, string apellido, int edad, string fecha, double saldo, int tarjeta ) : base(id, nr, nombre, apellido, edad, "Credito", saldo, fecha)
        {
            NumTarjeta = tarjeta;
        }

        public override void Retirar(double cantidad)
        {
            if (cantidad <= 0)
            {
                Console.WriteLine("Error: Cantidad inválida.");
                return;
            }
            Saldo -= cantidad;
            Console.WriteLine($"Has retirado {cantidad} de tu cuenta de crédito. Saldo actual: {Saldo}");
        }
	}
}
