using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final
{
    internal class Program
    {
        public static bool Iniciar = true;
        public static Usuario uPrincipal = null;

        // I N I C I O
        static void Titulo(string msg)
        {
            Console.WriteLine("====================================");
            Console.WriteLine($"\tUATPay - {msg}");
            Console.WriteLine("====================================");
        }
        static int Menu()
        {
            Console.Clear();
            Titulo("Inicio");
            Console.WriteLine("Bienvenido usuario!");
            Console.WriteLine("1) Iniciar sesión");
            Console.WriteLine("2) Crear cuenta");
            Console.WriteLine("3) Informacion");
            Console.WriteLine("4) Salir");
            Console.Write("> ");
            return int.TryParse(Console.ReadLine(), out int op) ? op : 0;
        }
        static void Info()
        {
            Console.Clear();
            Titulo("Informacion");
            Console.WriteLine("Proyecto Final de Programacion Orientada a Objetos");
            Console.WriteLine("\nCreadores:\nRivera Cruz Pablo\nMelchor Carrillo Francisco");
            Console.WriteLine("\n[ENTER] Para regresar.");
            Console.ReadKey();
        }
        // U S U A R I O
        static bool Sesion(List<Usuario> user)
        {
            Console.Clear();
            Titulo("Iniciar Sesión");
            Console.Write("Nombre o ID: ");
            string iNombre = Console.ReadLine();
            Console.Write("Contraseña: ");
            string iPass = Console.ReadLine();

            Usuario usuario = null;

            if (int.TryParse(iNombre, out int iUsers))
                usuario = user.Find(u => u.ID == iUsers && u.Pass == iPass);
            else
                usuario = user.Find(u => u.Nombre == iNombre && u.Pass == iPass);
            
            if (usuario != null)
            {
                uPrincipal = usuario;
                return true;
            }

            Console.WriteLine("Error: Datos incorrectos.\n[ENTER] Para regresar.");
            Console.ReadKey();
            return false;
        }
        static bool Crear(List<Usuario> user)
        {
            Console.Clear();
            Titulo("Crear cuenta");
            Console.WriteLine("Ingrese los siguientes datos.");
            Console.Write("Usuario: ");
            string cUser = Console.ReadLine();
            Console.Write("Contraseña: ");
            string cPass = Console.ReadLine();
            if (cUser.Contains("|") || cPass.Contains("|"))
            {
                Console.WriteLine("Error: No usar | en los datos.\n[ENTER] Para regresar,");
                Console.ReadKey();
                return false;
            }
            else
            {
                if (user.Any(u => u.Nombre == cUser))
                {
                    Console.WriteLine("Error: Usuario ya existe...");
                    Console.ReadKey();
                    return false;
                }
                int cID = user.Count > 0 ? user.Max(u => u.ID) + 1 : 1;
                user.Add(new Usuario(cID, cUser, cPass));
                GestorBanco.GuardarUsuarios(user);

                uPrincipal = user.Find(u => u.ID == cID);
                return true;
            }
        }
        static void InfoUsuario()
        {
            Console.Clear();
            Titulo("Usuario");
            Console.WriteLine($"Nombre: {uPrincipal.Nombre}");
            Console.WriteLine($"ID: {uPrincipal.ID}");
            Console.WriteLine($"Contraseña: {uPrincipal.Pass}");
            Console.ReadKey();
        }
        // C U E N T A S
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
            int rID = uPrincipal.ID;
            switch (cTipo)
            {
                case "chequera":
                    nueva = new Chequera(rID, rNR, rNombre, rApellido, rEdad, cFecha);
                    Console.WriteLine("Cuenta Chequera");
                    break;
                case "credito":
                    nueva = new Credito(rID, rNR, rNombre, rApellido, rEdad, cFecha);
                    Console.WriteLine("Cuenta Credito");
                    break;
                case "inversion":
                    nueva = new Inversion(rID, rNR, rNombre, rApellido, rEdad, cFecha);
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
                Titulo("Menú");
                Console.WriteLine($"Bienvenido {uPrincipal.Nombre}");
                Console.WriteLine("1) Ver cuentas");
                Console.WriteLine("2) Agregar cuenta");
                Console.WriteLine("3) Modificar cuenta");
                Console.WriteLine("4) Eliminar cuenta");
                Console.WriteLine("5) Ver usuario");
                Console.WriteLine("6) Depositar");
                Console.WriteLine("7) Retirar");
                Console.WriteLine("8) Cerrar sesion");
                Console.Write("> ");

                oOpcion = Convert.ToInt32(Console.ReadLine());

                switch (oOpcion)
                {
                    case 1: Ver(cuentas); break;
                    case 2: Registro(cuentas); break;
                    case 3: Modificar(cuentas); break;
                    case 4: Eliminar(cuentas); break;
                    case 5: InfoUsuario(); break;
                    case 6: Depositar(cuentas); break;
                    case 7: Retirar(cuentas); break;
                    case 8: Salir(cuentas); break;
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
            Environment.Exit(0);
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
            List<Usuario> usuarios = new List<Usuario>();
            GestorBanco.Cargar(cuentas);
            GestorBanco.CargarUsuarios(usuarios);

            while (Iniciar)
            {
                switch (Menu())
                {
                    case 4:
                        Info();
                        break;
                    case 1:
                        if (Sesion(usuarios)){
                            Opciones(cuentas);
                        };
                        break;
                    case 2:
                        if (Crear(usuarios))
                        {
                            Opciones(cuentas);
                        }
                        ;
                        break;
                    case 3:
                        Salir(cuentas);
                        break;
                    default:
                        /*Console.Clear();
                        Titulo("UATPay - Error");
                        Console.WriteLine("Opcion no encontrada.\n[ENTER] para continuar...");
                        //Console.ReadKey();*/
                        break;
                }
            }
        }
    }
}