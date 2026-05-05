using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_Final
{
    internal class Program
    {
        public static bool Iniciar = true;
        public static string Datos = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "cuentas.csv");
        static void Titulo(string msg)
        {
            Console.WriteLine("====================================");
            Console.WriteLine($"\t{msg}");
            Console.WriteLine("====================================");
        }

        static int Menu()
        {
            Console.Clear();
            Titulo("UATPay");
            Console.WriteLine("Bienvenido usuario!");
            Console.WriteLine("1) Iniciar sesión");
            Console.WriteLine("2) Crear cuenta");
            Console.WriteLine("3) Salir");
            Console.Write("> ");
            int op = Convert.ToInt32(Console.ReadLine());
            return op;
        }
        static void Ingreso(List<Cuenta> cuentas)
        {
            Console.Clear();
            Titulo("UATPay - Iniciar Sesión");
            Console.WriteLine("Numero de Registro (ID): ");
            int iNumCuenta = Convert.ToInt32(Console.ReadLine());
            var cuenta = cuentas.Find(c => c.Registro == iNumCuenta);

            if (cuenta != null)
            {
                Opciones(cuentas);
            }
            else
            {
                Console.WriteLine("Cuenta no encontrada.");
                Console.ReadKey();
            }
        }
        static void Registro(List<Cuenta> cuentas)
        {
            Console.Clear();
            Titulo("UATPay - Registro");
            Console.WriteLine("Proporcione los siguientes datos");
            Console.WriteLine("Nombre(s): ");
            string rNombre = Console.ReadLine();
            Console.WriteLine("Apellido(s): ");
            string rApellido = Console.ReadLine();
            Console.WriteLine("Edad:");
            int rEdad = Convert.ToInt32(Console.ReadLine());
            int rNR = cuentas.Count > 0 ? cuentas.Max(c => c.Registro) + 1 : 1;
            Console.WriteLine("Tipo de cuenta: Chequera / Credito / Inversion");
            string cTipo = Console.ReadLine().ToLower();
            string cFecha = DateTime.Now.ToString("yyyy-MM-dd");
            Cuenta nueva = null;

            switch (cTipo)
            {
                case "chequera":
                    nueva = new Chequera(rNR, rNombre, rApellido, rEdad, cFecha);
                    Console.WriteLine("Chequera");
                    break;
                case "credito":
                    nueva = new Credito(rNR, rNombre, rApellido, rEdad, cFecha);
                    Console.WriteLine("Credito");
                    break;
                case "inversion":
                    nueva = new Inversion(rNR, rNombre, rApellido, rEdad, cFecha);
                    Console.WriteLine("Inversion");
                    Console.Write("Tasa: ");
                    ((Inversion)nueva).Tasa = double.Parse(Console.ReadLine());
                    Console.Write("Periodo: ");
                    ((Inversion)nueva).Periodo = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Error: Opcion incorrecta.");
                    break;
            }
            cuentas.Add(nueva);
            GuardarCSV(cuentas);
        }
        static void Opciones(List<Cuenta> cuentas)
        {
            int oOpcion;
            do
            {
                Console.Clear();
                Titulo("UATPay - Menú");
                Console.WriteLine("1) Ver cuentas");
                Console.WriteLine("2) Agregar cuenta");
                Console.WriteLine("3) Modificar cuenta");
                Console.WriteLine("4) Eliminar cuenta");
                Console.WriteLine("5) Depositar");
                Console.WriteLine("6) Retirar");
                Console.WriteLine("7) Salir");
                Console.Write("> ");

                oOpcion = Convert.ToInt32(Console.ReadLine());

                switch (oOpcion)
                {
                    case 1: Ver(cuentas); break;
                    case 2: Agregar(cuentas); break;
                    case 3: Modificar(cuentas); break;
                    case 4: Eliminar(cuentas); break;
                    case 5: Depositar(cuentas); break;
                    case 6: Retirar(cuentas); break;
                    case 7: Salir(cuentas); break;
                    default: Console.WriteLine("Opción inválida\n[ENTER] para continuar..."); break;
                }

                Console.ReadKey();

            } while (oOpcion != 7);
        }
        static void Salir(List<Cuenta> x)
        {
            Console.Clear();
            Titulo("UATPay");
            Iniciar = false;
            GuardarCSV(x);
            Console.WriteLine("Finalizo el programa.");
        }

        static void Ver(List<Cuenta> cuentas)
        {
            Console.Clear();
            Titulo("UATPay - Cuentas");

            foreach (var c in cuentas)
                Console.WriteLine($"{c.Registro} | {c.Nombre} | {c.Apellido} | {c.Tipo} ");

            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            var cuenta = cuentas.Find(c => c.Registro == id);

            if (cuenta != null)
                Console.WriteLine($"{cuenta.Registro} | {cuenta.Nombre} {cuenta.Apellido} | {cuenta.Tipo} | Saldo: {cuenta.Saldo} | {cuenta.Apertura}");
            else
                Console.WriteLine("No encontrada");
        }

        static void Agregar(List<Cuenta> cuentas)
        {
            Registro(cuentas);
        }
        
        static void Modificar(List<Cuenta> cuentas)
        {
            Console.Write("ID a modificar: ");
            int id = int.Parse(Console.ReadLine());

            var c = cuentas.Find(x => x.Registro == id);

            if (c != null)
            {
                Console.Write("Nuevo nombre: ");
                c.Nombre = Console.ReadLine();

                Console.Write("Nuevo apellido: ");
                c.Apellido = Console.ReadLine();

                Console.Write("Nueva edad: ");
                c.Edad = int.Parse(Console.ReadLine());

                GuardarCSV(cuentas);
                Console.WriteLine("Actualizado");
            }
        }
        
        static void Eliminar(List<Cuenta> cuentas)
        {
            Console.Write("ID a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            cuentas.RemoveAll(c => c.Registro == id);
            GuardarCSV(cuentas);
            Console.WriteLine("Cuenta eliminada.");
        }

        static void Depositar(List<Cuenta> cuentas)
        {
            Console.Write("Numero de Cuenta: ");
            int id = int.Parse(Console.ReadLine());

            var c = cuentas.Find(x => x.Registro == id);

            if (c != null)
            {
                Console.Write("Cantidad: ");
                double m = double.Parse(Console.ReadLine());

                c.Depositar(m);
                GuardarCSV(cuentas);
                Console.WriteLine($"Se deposito a la cuenta ${m}");
            }
        }

        static void Retirar(List<Cuenta> cuentas)
        {
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());

            var c = cuentas.Find(x => x.Registro == id);

            if (c != null)
            {
                Console.Write("Cantidad: ");
                double m = double.Parse(Console.ReadLine());

                c.Retirar(m);
                GuardarCSV(cuentas);
                Console.WriteLine($"Se retiro a la cuenta ${m}");
            }
        } 
        public static void Cargar(List<Cuenta> cuentas)
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
                    c.Registro = int.Parse(d[0]);
                    c.Apertura = d[6];
                    cuentas.Add(c);
                }
            }
        }
        public static void GuardarCSV(List<Cuenta> cuentas)
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

        static void Main(string[] args)
        {
            List<Cuenta> cuentas = new List<Cuenta>();
            Cargar(cuentas);

            while (Iniciar)
            {
                switch (Menu())
                {
                    case 1:
                        Ingreso(cuentas);
                        break;
                    case 2:
                        Registro(cuentas);
                        break;
                    case 3:
                        Salir(cuentas);
                        break;
                    default:
                        Console.Clear();
                        Titulo("UATPay - Error");
                        Console.WriteLine("Opcion no encontrada, intentelo de nuevo.\n[ENTER] para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}