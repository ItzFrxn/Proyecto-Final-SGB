using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final
{
    // CLASE PRINCIPAL - CUENTA
    public abstract class Cuenta
    {
        private int numRegistro = new Random().Next(1000, 9999);
        private string nombres;
        private string apellidos;
        private int edad;
        private string fechaApt = DateTime.Now.ToString("yyyy-MM-dd");
        private double saldo = 0;
        private string tipo;

        // Propiedades
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

        // Constructores
        public Cuenta(string nombre, string apellido, int edad, string tipo)
        {

        }

    }
    // CLASE  SECUNDARIAS - CHEQUERA
    public class Chequera : Cuenta
    {
        public Chequera(string nombre, string apellido, int edad) : base(nombre, apellido, edad, "Chequera")
        {
        }
    }
    // CLASE - CREDITO
    public class Credito : Cuenta
    {
        private int numTarjeta;

        public Credito(string nombre, string apellido, int edad) : base(nombre, apellido, edad, "Credito")
        {
        }
    }
    // CLASE - INVERSION
    public class Inversion : Cuenta
    {
        private double tasa;
        private string periodo;

        public Inversion(string nombre, string apellido, int edad) : base(nombre, apellido, edad, "Inversion")
        {
        }

        public void Depositar(double cantidad)
        {
           Saldo += cantidad;
        }
        public void Retirar(double cantidad)
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
        static List<Cuenta> cuentas = new List<Cuenta>();


    }
}
