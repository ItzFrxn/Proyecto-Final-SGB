using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final
{
    public class Chequera : Cuenta
    {
        public Chequera(string nombre, string apellido, int edad, double saldo = 00.00) : base(nombre, apellido, edad, "Chequera", saldo)
        {
        }
    }
}