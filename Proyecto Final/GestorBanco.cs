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
        static string Datos = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "cuentas.csv");
        static public void Cargar(List<Cuenta> cuentas)
        {
            if (!File.Exists(Datos))
                File.WriteAllText(Datos, "ID|NOMBRE|APELLIDO|EDAD|SALDO|TIPO|FECHA\n");

            var lineas = File.ReadAllLines(Datos);

            foreach (var l in lineas.Skip(1))
            {
                var d = l.Split('|');

                Cuenta c = null;

                if (d[5] == "Chequera")
                    c = new Chequera(int.Parse(d[0]), d[1], d[2], int.Parse(d[3]), d[6], double.Parse(d[4]));

                else if (d[5] == "Credito")
                    c = new Credito(int.Parse(d[0]), d[1], d[2], int.Parse(d[3]), d[6], double.Parse(d[4]));

                else if (d[5] == "Inversion")
                    c = new Inversion(int.Parse(d[0]), d[1], d[2], int.Parse(d[3]), d[6], double.Parse(d[4]));

                if (c != null)
                {
                    cuentas.Add(c);
                }
            }
        }
        public static void Guardar(List<Cuenta> cuentas)
        {
            using (StreamWriter sw = new StreamWriter(Datos))
            {
                sw.WriteLine("ID|NOMBRE|APELLIDO|EDAD|SALDO|TIPO|FECHA");

                foreach (var c in cuentas)
                {
                    sw.WriteLine($"{c.Registro}|{c.Nombre}|{c.Apellido}|{c.Edad}|{c.Saldo}|{c.Tipo}|{c.Apertura}");
                }
            }
        }
    }
}
