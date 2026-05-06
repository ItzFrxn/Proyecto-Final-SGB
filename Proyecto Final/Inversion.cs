using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final
{
    public class Inversion : Cuenta
    {
        private double tasa;
        private string periodo;

        public double Tasa
        {
            get { return this.tasa; }
            set { this.tasa = value; }
        }
        public string Periodo
        {
            get { return this.periodo; }
            set { this.periodo = value; }
        }

        public Inversion(int id, int nr, string nombre, string apellido, int edad, string fecha, double saldo, double tasas, string periodos) : base(id, nr, nombre, apellido, edad, "Inversion", saldo, fecha)
        {
            Tasa = tasas;
            Periodo = periodos;
        }
    }
}
