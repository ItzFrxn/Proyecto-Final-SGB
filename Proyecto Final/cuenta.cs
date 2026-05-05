using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final
{
    public abstract class Cuenta
    {
        private int numRegistro;
        private string nombres;
        private string apellidos;
        private int edad;
        private string fechaApt;
        private double saldo;
        private string tipo;

        public int Registro
        {
            get { return this.numRegistro; }
            set { this.numRegistro = value; }
        }
        public string Nombre
        {
            get { return this.nombres; }
            set { this.nombres = value; }
        }
        public string Apellido
        {
            get { return this.apellidos; }
            set { this.apellidos = value; }
        }
        public int Edad
        {
            get { return this.edad; }
            set { this.edad = value; }
        }
        public string Apertura
        {
            get { return this.fechaApt; }
            set { this.fechaApt = value; }
        }
        public double Saldo
        {
            get { return this.saldo; }
            set { this.saldo = value; }
        }
        public string Tipo
        {
            get { return this.tipo; }
            set { this.tipo = value; }
        }


        public Cuenta(string nombre, string apellido, int edad, string tipo, double saldo)
        {
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
            Tipo = tipo;
            Saldo = saldo;
        }

        public Cuenta(int registro, string nombre, string apellido, int edad, string tipo, double saldo, string fecha)
        {
            Registro = registro;
            Nombre = nombre;
            Apellido = apellido;
            Edad = edad;
            Tipo= tipo;
            Saldo= saldo;
            Apertura = fecha;
        }

        public void Depositar(double cantidad)
        {
            Saldo += cantidad;
        }

        public virtual void Retirar(double cantidad)
        {
            if (cantidad <= Saldo)
            {
                Saldo -= cantidad;
            }
            else
            {
                Console.WriteLine("Saldo insuficiente.");
            }
        }
    }
}
