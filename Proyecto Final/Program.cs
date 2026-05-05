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

            /*Console.WriteLine("Contraseña:");
            string iPass = Console.ReadLine();
            if (iNumCuenta == 123456 && iPass == "asd")
            {
                Opciones(cuentas);
            }
            ;
            */

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

        // O P C I O N E S
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
            /*
            Console.Clear();
            Titulo("Nueva cuenta");

            Console.Write("Nombre: ");
            string nom = Console.ReadLine();

            Console.Write("Apellido: ");
            string ape = Console.ReadLine();

            Console.Write("Edad: ");
            int edad = int.Parse(Console.ReadLine());

            Console.Write("Tipo (chequera/credito/inversion): ");
            string tipo = Console.ReadLine().ToLower();

            int id = cuentas.Count > 0 ? cuentas.Max(c => c.Registro) + 1 : 1;

            Cuenta nueva = null;

            if (tipo == "chequera")
                nueva = new Chequera(nom, ape, edad);

            else if (tipo == "credito")
            {
                nueva = new Credito(nom, ape, edad);
                Console.Write("Numero tarjeta: ");
                ((Credito)nueva).NumTarjeta = int.Parse(Console.ReadLine());
            }

            else if (tipo == "inversion")
            {
                nueva = new Inversion(nom, ape, edad);
                Console.Write("Tasa: ");
                ((Inversion)nueva).Tasa = double.Parse(Console.ReadLine());
                Console.Write("Periodo: ");
                ((Inversion)nueva).Periodo = Console.ReadLine();
            }

            if (nueva != null)
            {
                nueva.Registro = id;
                nueva.Apertura = DateTime.Now.ToString("yyyy-MM-dd");

                cuentas.Add(nueva);
                Console.WriteLine("Cuenta creada");
            }*/
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

        // M A I N 
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

            /*Console.Clear();
            List<Cuenta> cuentas = new List<Cuenta>();
            GestorBanco.CargarCSV(cuentas);

            switch (Menu())
            {
                case 1:
                    Ingreso();
                    /*
                    Console.WriteLine("Proporcione los siguientes datos.");
                    Console.Write("Usuario: ");
                    string user = Console.ReadLine();
                    Console.Write("Contraseña: ");
                    string pass = Console.ReadLine();
                    string ususarioCorrecto = "admin";
                    string passCorrecto = "123456";

                    if (user == ususarioCorrecto && pass == passCorrecto)
                    {
                        Titulo("Acceso correcto.");

                        int opcion;
                        do
                        {
                            Console.WriteLine("\n1.Agregar cuenta");
                            Console.WriteLine("2.Ver cuentas");
                            Console.WriteLine("3.Depositar");
                            Console.WriteLine("4.Retirar");
                            Console.WriteLine("5.Buscar cuenta");
                            Console.WriteLine("6.Salir");

                            opcion = Convert.ToInt32(Console.ReadLine());

                            Titulo("UATPay");

                            switch (opcion)
                            {
                                case 1:
                                    //Agregar cuenta
                                    Console.WriteLine("Nombre: ");
                                    string rName = Console.ReadLine();

                                    if (rName.Contains(","))
                                    {
                                        Console.WriteLine("El nombre no puede contener comas.");
                                        break;
                                    }

                                    Console.WriteLine("Apellido: ");
                                    string rLastN = Console.ReadLine();

                                    if (rLastN.Contains(","))
                                    {
                                        Console.WriteLine("El apellido no puede contener comas.");
                                        break;
                                    }

                                    Console.WriteLine("Edad: ");
                                    int rAge = Convert.ToInt32(Console.ReadLine());

                                    Console.WriteLine("Tipo de cuenta (chequera / credito / inversion ): ");
                                    string rType = Console.ReadLine().ToLower();

                                    Cuenta nueva = null;

                                    if (rType == "chequera")
                                    {
                                        nueva = new Chequera(rName, rLastN, rAge);
                                    }
                                    else if (rType == "credito")
                                    {
                                        nueva = new Credito(rName, rLastN, rAge);
                                    }
                                    else if (rType == "inversion")
                                    {
                                        nueva = new Inversion(rName, rLastN, rAge);
                                    }
                                    if (nueva != null)
                                    {
                                        cuentas.Add(nueva);
                                        Console.WriteLine("Cuenta creada exitosamente.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Tipo de cuenta no válido.");
                                    }
                                    break;

                                case 2:
                                    if (cuentas.Count == 0)
                                    {
                                        Console.WriteLine("No hay cuentas registradas.");
                                    }
                                    else
                                    {
                                        foreach (var c in cuentas)
                                        {
                                            Console.WriteLine($"{c.Registro} - {c.Nombre} - {c.Tipo} - saldo: ${c.Saldo}");
                                        }
                                    }
                                    break;

                                case 3:
                                    Console.WriteLine("Ingrese el numero de cuenta: ");
                                    int numDep = Convert.ToInt32(Console.ReadLine());

                                    var cuentaDep = cuentas.FirstOrDefault(c => c.Registro == numDep);

                                    if (cuentaDep != null)
                                    {
                                        Console.Write("Cantidad a depositar: ");
                                        double cant = Convert.ToDouble(Console.ReadLine());

                                        cuentaDep.Depositar(cant);
                                        Console.WriteLine("Depósito exitoso.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Cuenta no encontrada.");
                                    }
                                    break;

                                case 4:
                                    Console.WriteLine("Ingresa numero de cuenta: ");
                                    int numRet = Convert.ToInt32(Console.ReadLine());

                                    var cuentaRet = cuentas.Find(c => c.Registro == numRet);
                                    if (cuentaRet != null)
                                    {
                                        Console.Write("Cantidad a retirar: ");
                                        double cant = Convert.ToDouble(Console.ReadLine());

                                        cuentaRet.Retirar(cant);
                                    }
                                    else
                                    {
                                        Console.WriteLine("Cuenta no encontrada.");
                                    }
                                    break;
                                case 5:
                                    Console.Write("Ingresar numero de cuenta: ");
                                    int numBus = Convert.ToInt32(Console.ReadLine());

                                    var cuentaBus = cuentas.Find(c => c.Registro == numBus);

                                    if (cuentaBus != null)
                                    {
                                        Console.WriteLine($"{cuentaBus.Registro} -{cuentaBus.Nombre} - {cuentaBus.Apellido} - {cuentaBus.Tipo} - saldo: ${cuentaBus.Saldo}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Cuenta no encontrada.");
                                    }
                                    break;
                                case 6:
                                    GestorBanco.GuardarCSV(cuentas);
                                    Console.WriteLine("Datos Guardados. Saliendo del sistema...");
                                    break;

                            }

                        } while (opcion != 6);
                    }
                    else
                    {
                        Console.WriteLine("Acceso denegado");
                    }
                    
                    break;

                case 2:
                    Registro();
                    /*
                    Console.WriteLine("Proporcione los siguientes datos.");

                    Console.Write("Nombre: ");
                    string rName2 = Console.ReadLine();

                    Console.Write("Apellidos: ");
                    string rLastN2 = Console.ReadLine();

                    Console.Write("Edad: ");
                    int rAge2 = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Tipo de cuenta (chequera / credito / inversion ): ");
                    string rType2 = Console.ReadLine().ToLower();

                    Cuenta nueva2 = null;

                    if (rType2 == "chequera")
                    {
                        nueva2 = new Chequera(rName2, rLastN2, rAge2);
                    }
                    else if (rType2 == "credito")
                    {
                        nueva2 = new Credito(rName2, rLastN2, rAge2);
                    }
                    else if (rType2 == "inversion")
                    {
                        nueva2 = new Inversion(rName2, rLastN2, rAge2);
                    }
                    if (nueva2 != null)
                    {
                        cuentas.Add(nueva2);
                        Console.WriteLine("Cuenta creada exitosamente.");
                    }
                    else
                    {
                        Console.WriteLine("Tipo de cuenta no válido.");
                    }
                    break;
                default:

                    break;
             */
        }
    }
}