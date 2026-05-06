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
        static string Users = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName, "usuarios.csv");

        static public void Cargar(List<Cuenta> cuentas)
        {
            if (!File.Exists(Datos))    //0        1        2       3      4    5     6     7
                File.WriteAllText(Datos, "ID|NUMREGISTRO|NOMBRE|APELLIDO|EDAD|FECHA|SALDO|TIPO\n");

            var lineas = File.ReadAllLines(Datos);

            foreach (var l in lineas.Skip(1))
            {
                var d = l.Split('|');

                int cID = int.Parse(d[0]);
                int cNR = int.Parse(d[1]);
                string cNombre = d[2];
                string cApellido = d[3];
                int cEdad = int.Parse(d[4]);
                string cFecha = d[5];
                double cSaldo = double.Parse(d[6]);
                string cTipo = d[7];
                Cuenta c = null;

                if (cTipo == "Chequera")
                    c = new Chequera(cID,cNR, cNombre, cApellido, cEdad, cFecha, cSaldo);

                else if (cTipo == "Credito")
                    c = new Credito(cID, cNR, cNombre, cApellido, cEdad, cFecha, cSaldo);

                else if (cTipo == "Inversion")
                    c = new Inversion(cID, cNR, cNombre, cApellido, cEdad, cFecha, cSaldo);

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
                sw.WriteLine("ID|NUMREGISTRO|NOMBRE|APELLIDO|EDAD|FECHA|SALDO|TIPO");

                foreach (var c in cuentas)
                {
                    sw.WriteLine($"{c.ID}|{c.Registro}|{c.Nombre}|{c.Apellido}|{c.Edad}|{c.Apertura}|{c.Saldo}|{c.Tipo}");
                }
            }
        }
        public static void CargarUsuarios(List<Usuario> usuarios)
        {
            if (!File.Exists(Users))
                File.WriteAllText(Users, "ID|NOMBRE|PASSWORD\n1|admin|1234\n");

            var lineas = File.ReadAllLines(Users);

            foreach (var l in lineas.Skip(1))
            {
                var d = l.Split('|');

                Usuario u = new Usuario(
                    int.Parse(d[0]),
                    d[1],
                    d[2]
                );

                usuarios.Add(u);
            }
        }
        public static void GuardarUsuarios(List<Usuario> usuarios)
        {
            using (StreamWriter sw = new StreamWriter(Users))
            {
                sw.WriteLine("ID|USUARIO|PASSWORD");

                foreach (var u in usuarios)
                {
                    sw.WriteLine($"{u.ID}|{u.Nombre}|{u.Pass}");
                }
            }
        }
    }
}
