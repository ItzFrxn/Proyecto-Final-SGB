using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final
{
    public class Chequera : Cuenta
    {
        public Chequera(int id, int nr, string nombre, string apellido, int edad, string fecha, double saldo = 00.00) : base(id, nr, nombre, apellido, edad, "Chequera", saldo, fecha)
        {
        }
    }
}