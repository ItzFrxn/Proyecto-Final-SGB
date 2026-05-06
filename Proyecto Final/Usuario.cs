using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final
{
    internal class Usuario
    {
        private string nombre;
        private string pass;
        private int id;

        public string Nombre
        {
            get { return this.nombre; }
            set { this.nombre = value; }
        }
        public int ID
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public string Pass
        {
            get { return this.pass; } set { this.pass = value; }
        }
        public Usuario(int id, string nombre, string pass)
        {
            ID = id;
            Nombre = nombre;
            Pass = pass;
        }
    }
}
