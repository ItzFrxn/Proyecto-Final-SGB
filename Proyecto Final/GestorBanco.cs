using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final
{
    internal class GestorBanco
    {
        public static void  GuardarCSV(List<Cuenta> cuentas)
        {
            using(StreamWriter sw = new StreamWriter("cuentas.csv"))
            {
                foreach (var cuenta in cuentas)
                {
                  sw.WriteLine($"{cuenta.Registro}, {cuenta.Nombre}, {cuenta.Apellido}, {cuenta.Edad}, {cuenta.Tipo}, {cuenta.Saldo}");
                }
            }
        }
        public static void CargarCSV(List<Cuenta> cuentas)
        {
            if (File.Exists("cuentas.csv"))
            {
                var lineas = File.ReadAllLines("cuentas.csv");

                foreach (var linea in lineas)
                {
                    var datos = linea.Split(',');

                    string nombre = datos[1];
                    string apellido = datos[2];
                    int edad = Convert.ToInt32(datos[3]);
                    string tipo = datos[4];
                    double saldo = Convert.ToDouble(datos[5]);

                    Cuenta nueva = null;

                    if (tipo == "chequera")
                    {
                        nueva = new Chequera(nombre, apellido, edad);
                    }
                    else if (tipo == "credito")
                    {
                        nueva = new Credito(nombre, apellido, edad);
                    }
                    else if (tipo == "inversion")
                    {
                        nueva = new Inversion(nombre, apellido, edad);
                    }
                    if (nueva != null)
                    {
                        nueva.Saldo = saldo;
                        cuentas.Add(nueva);

                    }
                }
            }
        }
    }
}
