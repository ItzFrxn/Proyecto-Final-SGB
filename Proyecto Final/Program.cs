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
            Console.WriteLine("\n0) Informacion");
            Console.Write("> ");
            return int.TryParse(Console.ReadLine(), out int op) ? op : 0;
        }
        static void Info()
        {
            Console.Clear();
            Titulo("UATPay - Informacion");
            Console.WriteLine("idk");
            Console.ReadKey();
        }
        static void Ingreso(List<Cuenta> cuentas)
        {
            Console.Clear();
            Titulo("UATPay - Iniciar Sesión");
            Console.WriteLine("Numero de Registro (ID): ");
            if (!int.TryParse(Console.ReadLine(), out int iNumCuenta))
            {
                Console.WriteLine("Error: ID invalida...");
                return;
            }
            var cuenta = cuentas.Find(c => c.Registro == iNumCuenta);

            if (cuenta != null)
            {
                Opciones(cuentas);
            }
            else
            {
                Console.WriteLine("Cuenta no encontrada.\n[ENTER] Para continuar...");
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
            if (rNombre.Contains("|") || rApellido.Contains("|"))
            {
                Console.WriteLine("Error: No usar | en los datos");
                return;
            }
            Console.WriteLine("Edad:");
            if (!int.TryParse(Console.ReadLine(), out int rEdad))
            {
                Console.WriteLine("Error: Edad inválido...");
                return;
            }

            int rNR = cuentas.Count > 0 ? cuentas.Max(c => c.Registro) + 1 : 1;
            Console.WriteLine("Tipo de cuenta: Chequera / Credito / Inversion");
            string cTipo = Console.ReadLine().ToLower();
            string cFecha = DateTime.Now.ToString("yyyy-MM-dd");
            Cuenta nueva = null;

            switch (cTipo)
            {
                case "chequera":
                    nueva = new Chequera(rNR, rNombre, rApellido, rEdad, cFecha);
                    Console.WriteLine("Cuenta Chequera");
                    break;
                case "credito":
                    nueva = new Credito(rNR, rNombre, rApellido, rEdad, cFecha);
                    Console.WriteLine("Cuenta Credito");
                    break;
                case "inversion":
                    nueva = new Inversion(rNR, rNombre, rApellido, rEdad, cFecha);
                    Console.WriteLine("Cuenta Inversion");
                    Console.Write("Tasa: ");
                    ((Inversion)nueva).Tasa = double.Parse(Console.ReadLine());
                    Console.Write("Periodo (semanal, mensual, anual): ");
                    ((Inversion)nueva).Periodo = Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Error: Opcion incorrecta.");
                    break;
            }
            cuentas.Add(nueva);
            GestorBanco.Guardar(cuentas);
            Console.WriteLine($"Cuenta creada ID: {rNR}");
            Console.WriteLine("[{ENTER}] Para continuar...");
            Console.ReadKey();
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
                    case 2: Registro(cuentas); break;
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
            GestorBanco.Guardar(x);
            Console.WriteLine("Finalizo el programa.");
        }

        static void Ver(List<Cuenta> cuentas)
        {
            Console.Clear();
            Titulo("UATPay - Cuentas");

            foreach (var c in cuentas)
                Console.WriteLine($"{c.Registro} | {c.Nombre} | {c.Apellido} | {c.Tipo} ");

            Console.Write("ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Error: ID inválido...");
                return;
            }

            var cuenta = cuentas.Find(c => c.Registro == id);

            if (cuenta != null)
                Console.WriteLine($"{cuenta.Registro} | {cuenta.Nombre} {cuenta.Apellido} | {cuenta.Tipo} | Saldo: {cuenta.Saldo} | {cuenta.Apertura}");
            else
                Console.WriteLine("Error: Cuenta no encontrada...");
        }
        
        static void Modificar(List<Cuenta> cuentas)
        {
            Console.Write("ID a modificar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Eror: ID inválido...");
                return;
            }

            var c = cuentas.Find(x => x.Registro == id);

            if (c != null)
            {
                Console.Write("Nuevo nombre: ");
                c.Nombre = Console.ReadLine();

                Console.Write("Nuevo apellido: ");
                c.Apellido = Console.ReadLine();

                Console.Write("Nueva edad: ");
                c.Edad = int.Parse(Console.ReadLine());

                GestorBanco.Guardar(cuentas);
                Console.WriteLine("Actualizado");
            } if (c == null)
            {
                Console.WriteLine("Error: Cuenta no encontrada...");
            }
        }
        
        static void Eliminar(List<Cuenta> cuentas)
        {
            Console.Write("ID a eliminar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Eror: ID inválido...");
                return;
            }

            cuentas.RemoveAll(c => c.Registro == id);
            GestorBanco.Guardar(cuentas);
            Console.WriteLine("Cuenta eliminada.");
        }

        static void Depositar(List<Cuenta> cuentas)
        {
            Console.Write("Numero de Cuenta: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Eror: ID inválido...");
                return;
            }

            var c = cuentas.Find(x => x.Registro == id);

            if (c != null)
            {
                Console.Write("Cantidad: ");
                if (!double.TryParse(Console.ReadLine(), out double m) || m <= 0)
                {
                    Console.WriteLine("Error: cantidad inválida");
                    return;
                }
                c.Depositar(m);
                GestorBanco.Guardar(cuentas);
                Console.WriteLine($"Se deposito a la cuenta ${m}");
            }
        }

        static void Retirar(List<Cuenta> cuentas)
        {
            Console.Write("ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Eror: ID inválido...");
                return;
            }

            var c = cuentas.Find(x => x.Registro == id);

            if (c != null)
            {
                Console.Write("Cantidad: "); 
                if (!double.TryParse(Console.ReadLine(), out double m) || m <= 0)
                {
                    Console.WriteLine("Error: cantidad inválida");
                    return;
                }
                c.Retirar(m);
                GestorBanco.Guardar(cuentas);
                Console.WriteLine($"Has retirado {m} de tu cuenta de crédito. Saldo actual: {c.Saldo}");
            }
        }

        static void Main(string[] args)
        {
            List<Cuenta> cuentas = new List<Cuenta>();
            GestorBanco.Cargar(cuentas);

            while (Iniciar)
            {
                switch (Menu())
                {
                    case 0:
                        Info();
                        break;
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
                        Console.WriteLine("Opcion no encontrada.\n[ENTER] para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}