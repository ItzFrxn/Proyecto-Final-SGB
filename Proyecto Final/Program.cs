
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
        static void Main(string[] args)
        {
            List<Cuenta> cuentas = new List<Cuenta>();
            GestorBanco.CargarCSV(cuentas);

            void Titulo(string msg)
            {
                Console.WriteLine("====================================");
                Console.WriteLine($"\t{msg}");
                Console.WriteLine("====================================");
            }

            Titulo("UATPay");
            Console.WriteLine("Welcome to UAT-Pay!");
            Console.WriteLine("1) Ingresar\n2) Registrarse");

            int op = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Titulo("UATPay");

            switch (op)
            {
                case 1:
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
            }
        }
    }
}
