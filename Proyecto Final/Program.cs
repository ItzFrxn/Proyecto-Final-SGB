using System;
using System.Collections;
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
            Console.Title = "UATPay";
            Console.WriteLine("====================================");
            Console.WriteLine($"\tUATPay - {msg}");
            Console.WriteLine("====================================");
        }
		static int LeerInt(string msg)
		{
			int valor;
			do
			{
				Console.Write(msg);
				if (int.TryParse(Console.ReadLine(), out valor))
					return valor;

				Console.WriteLine("Error: ingrese un número válido.");
			} while (true);
		}
		static double LeerDouble(string msg)
		{
			double valor;
			do
			{
				Console.Write(msg);
				if (double.TryParse(Console.ReadLine(), out valor) && valor > 0)
					return valor;

				Console.WriteLine("Error: ingrese una cantidad válida.");
			} while (true);
		}
		static string LeerString(string msg)
		{
			string valor;
			do
			{
				Console.Write(msg);
				valor = Console.ReadLine();

				if (!string.IsNullOrWhiteSpace(valor) && !valor.Contains("|"))
					return valor;

				Console.WriteLine("Error: entrada inválida (vacía o con '|').");
			} while (true);
		}
		static string LeerPeriodo(string msg)
		{
			string[] validos = { "diario", "semanal", "mensual", "anual" };

			while (true)
			{
				string p = LeerString(msg).ToLower();
				if (validos.Contains(p))
					return p;

				Console.WriteLine("Error: periodo inválido.");
			}
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
            return LeerInt("> ");
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
            string iNombre = LeerString("Nombre o ID: ");
            string iPass = LeerString("Contraseña: ");

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
            Titulo("Crear Perfil");

            Console.WriteLine("Ingrese los siguientes datos.");
            string cUser = LeerString("Usuario: ");
            string cPass = LeerString("Contraseña: ");

            if (user.Any(u => u.Nombre == cUser))
            {
                Console.WriteLine("Error: Usuario ya existe.\n[ENTER] Para continuar.");
                Console.ReadKey();
                return false;
            }
            int cID = user.Count > 0 ? user.Max(u => u.ID) + 1 : 1;
            user.Add(new Usuario(cID, cUser, cPass));
            GestorBanco.GuardarUsuarios(user);

            uPrincipal = user.Find(u => u.ID == cID);
            return true;

        }
        static void VerPerfil(List<Usuario> usuarios)
        {
            int iOpcion;
            do
			{
				Console.Clear();
				Titulo("Usuario");
				Console.WriteLine($"Nombre: {uPrincipal.Nombre}");
                Console.WriteLine($"ID: {uPrincipal.ID}");
                Console.WriteLine($"Contraseña: {uPrincipal.Pass}");
                Console.WriteLine($"\n1) Editar datos");
                Console.WriteLine($"2) Eliminar perfil");
                Console.WriteLine($"3) Regresar");
                iOpcion = LeerInt("> ");
                switch (iOpcion)
                {
                    case 1: UserMod(usuarios); break;
                    case 2: UserEliminar(usuarios); return;
                    case 3: break;
                }
            } while (iOpcion != 3);
		}
        static void UserMod(List<Usuario> usuarios)
        {
            Console.Clear();
            Titulo("Editar Usuario");
            var usuario = usuarios.Find(u => u.ID == uPrincipal.ID);
            usuario.Nombre = LeerString("Nuevo usuario: ");
            usuario.Pass = LeerString("Nueva contraseña: ");

            GestorBanco.GuardarUsuarios(usuarios);
            Console.WriteLine("Datos actualizados.");
        }
        static void UserEliminar(List<Usuario> usuarios)
        {
            Console.Clear();
            Titulo("Eliminar Usuario");
            string uConfirmacion = LeerString("Desea eliminar su perfil? (si/no)\n> ").ToLower();
            if (uConfirmacion == "si")
            {
                usuarios.RemoveAll(u => u.ID == uPrincipal.ID);
                GestorBanco.GuardarUsuarios(usuarios);
                Console.WriteLine("Usuario eliminado.\n[ENTER] Para regresar.");
                Console.ReadKey();
                Cerrar();
            }
		}
		static void Cerrar()
		{
			Console.Clear();
			Titulo("Cerrar sesión");

			uPrincipal = null;
			//Console.WriteLine("Sesión cerrada correctamente.\n[ENTER] para continuar...");
			//Console.ReadKey();
		}
		// C U E N T A S
		static int GenTarjeta()
		{
            Random random = new Random();
			string gPrefijo = "26";
			int gAleatorio = random.Next(0, 1000);
			string gNumero = gPrefijo + gAleatorio.ToString("D3");
			return int.Parse(gNumero);
		}
		static void Registro(List<Cuenta> cuentas)
        {
            Console.Clear();
            Titulo("Registro Cuenta");

            Console.WriteLine("Proporcione los siguientes datos");
            string rNombre = LeerString("Nombres(S)");
            string rApellido = LeerString("Apellidos(S):");
            int rEdad = LeerInt("Edad: ");
			if (rEdad <= 0 || rEdad > 120)
			{
				Console.WriteLine("Edad inválida.");
				return;
			}
			int rNR = cuentas.Count > 0 ? cuentas.Max(c => c.Registro) + 1 : 1;
            string cTipo = LeerString("Tipo de cuenta (chequera / credito / inversion): ").ToLower();
            string cFecha = DateTime.Now.ToString("yyyy-MM-dd");
			int rID = uPrincipal.ID;
			Cuenta nueva = null;

            switch (cTipo)
            {
                case "chequera":
                    nueva = new Chequera(rID, rNR, rNombre, rApellido, rEdad, cFecha);
                    Console.WriteLine("Cuenta Chequera");
                    break;
                case "credito":
                    int rNT = GenTarjeta();
                    nueva = new Credito(rID, rNR, rNombre, rApellido, rEdad, cFecha, 0, rNT);
                    Console.WriteLine("Cuenta Credito");
                    break;
                case "inversion":
                    Console.WriteLine("Cuenta Inversion");
                    double rTasa = LeerDouble("Tasa: ");
                    string rPeriodo = LeerPeriodo("Periodo (diario / semanal / mensaul / anual):");
					nueva = new Inversion(rID, rNR, rNombre, rApellido, rEdad, cFecha, 0, rTasa, rPeriodo);
					break;
                default:
                    Console.WriteLine("Error: Opcion incorrecta.");
                    break;
            }
            cuentas.Add(nueva);
            GestorBanco.Guardar(cuentas);
            Console.WriteLine($"Cuenta creada ID: {rNR}");
            Console.WriteLine("[ENTER] Para continuar...");
            Console.ReadKey();
        }
        static void Opciones(List<Cuenta> cuentas, List<Usuario> usuarios)
        {
            int oOpcion;
            do
            {
                Console.Clear();
                Titulo("Menú");
                Console.WriteLine($"Bienvenido {uPrincipal.Nombre}");
                Console.WriteLine("1) Ver cuentas");
                Console.WriteLine("2) Ver perfil");
                Console.WriteLine("3) Cerrar sesion");
                oOpcion = LeerInt("> ");

                switch (oOpcion)
                {
                    case 1: VerCuenta(cuentas); break;
                    case 2: VerPerfil(usuarios); break;
                    case 3: Cerrar(); return;
                    /*case 1: Ver(cuentas); break;
                    case 2: Registro(cuentas); break;
                    case 3: Modificar(cuentas); break;
                    case 4: Eliminar(cuentas); break;
                   //case 5: InfoUsuario(uPrincipal); break;
                    case 6: Depositar(cuentas); break;
                    case 7: Retirar(cuentas); break;
                    case 8: Cerrar(); return;*/
                    default: Console.WriteLine("Opción inválida\n[ENTER] para continuar..."); break;
                }

               // Console.ReadKey();

            } while (uPrincipal != null);
        }
		static void CuentaMenu(List<Cuenta> cuentas, Cuenta cuenta)
		{
			int opcion;

			do
			{
				Console.Clear();
				Titulo("Cuenta");

				Console.WriteLine($"ID: {cuenta.Registro}");
				Console.WriteLine($"Nombre: {cuenta.Nombre} {cuenta.Apellido}");
				Console.WriteLine($"Tipo: {cuenta.Tipo}");
				Console.WriteLine($"Saldo: {cuenta.Saldo}");
				Console.WriteLine($"Fecha: {cuenta.Apertura}");

				Console.WriteLine("\n1) Modificar");
				Console.WriteLine("2) Eliminar");
				Console.WriteLine("3) Depositar");
				Console.WriteLine("4) Retirar");
				Console.WriteLine("5) Regresar");
				opcion = LeerInt("> ");

				switch (opcion)
				{
					case 1:

						Console.Clear();
						Titulo("Editar Cuenta");
						cuenta.Nombre = LeerString("Nuevo nombre: ");
						cuenta.Apellido = LeerString("Nuevo apellido: ");
						cuenta.Edad = LeerInt("Nueva edad: ");
						GestorBanco.Guardar(cuentas);
						break;

					case 2:
						Console.Clear();
						Titulo("Eliminar Cuenta");
                        string mConfir = LeerString("Eliminar cuenta? (si/no): ").ToLower();
                        if (mConfir == "si")
                        {
                            cuentas.Remove(cuenta);
                            GestorBanco.Guardar(cuentas);
                            Console.WriteLine("Cuenta eliminada.");
                            Console.ReadKey();
                            return;
                        }
                        break;
					case 3:
						Console.Clear();
						Titulo("Depositar Cuenta");
						double cDepositar = LeerDouble("Cantidad: ");
						cuenta.Depositar(cDepositar);
						GestorBanco.Guardar(cuentas);
						break;

					case 4:
						Console.Clear();
						Titulo("Retirar Cuenta");
						double cRetirar = LeerDouble("Cantidad: ");
						cuenta.Retirar(cRetirar);
						GestorBanco.Guardar(cuentas);
						break;
				}

			} while (opcion != 5);
		}
		static void Salir(List<Cuenta> cuentas)
        {
            Console.Clear();
            Titulo("Exit");
            Iniciar = false;
            GestorBanco.Guardar(cuentas);
            Console.WriteLine("Finalizo el programa.");
            Environment.Exit(0);
        }

        static void VerCuenta(List<Cuenta> cuentas)
        {
            int opcion;
            do
            {
                Console.Clear();
                Titulo("Cuentas");

                var cUser = cuentas.Where(c => c.ID == uPrincipal.ID).ToList();
				if (cUser.Count == 0)
				{
					Console.WriteLine("No tienes cuentas.");
					//Console.ReadKey();
				}

				foreach (var c in cUser)
                    Console.WriteLine($"{c.Registro} | {c.Nombre} | {c.Apellido} | {c.Tipo}");

                Console.WriteLine("\n1) Agregar cuenta");
                Console.WriteLine("2) Ver cuenta");
                Console.WriteLine("3) Regresar");
                opcion = LeerInt("> ");

                switch (opcion)
                {
                    case 1:
                        Registro(cuentas); break;
                    case 2:
                        int id = LeerInt("ID de cuenta: ");
						var cuenta = cUser.Find(c => c.Registro == id);
						//var cuenta = cuentas.Find(c => c.Registro == id && c.ID == uPrincipal.ID);

						if (cuenta != null)
                            CuentaMenu(cuentas, cuenta);
                        else
                        {
                            Console.WriteLine("Error: Cuenta no encontrada.\n[ENTER] Para continuar.");
                            Console.ReadKey();
                        }
                        break;
                }

            } while (opcion != 3);
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
                    case 1: if (Sesion(usuarios)) Opciones(cuentas, usuarios); break;
                    case 2: if (Crear(usuarios)) Opciones(cuentas, usuarios); break;
                    case 3: Info(); break;
                    case 4: Salir(cuentas); break;
                    default: break;
                }
            }
        }
    }
}