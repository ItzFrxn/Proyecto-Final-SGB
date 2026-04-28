using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //HOLAAAAAAAAAAAAAAAAAA
            Console.WriteLine("Holaaa");
            Console.WriteLine("=======================");
            Console.WriteLine("\tUATPay");
            Console.WriteLine("=======================");
            Console.WriteLine("Welcome to UAT-Pay!"); 
            Console.WriteLine("1) Ingresar\n2) Registrarse");
            int op = Convert.ToInt32(Console.ReadLine());
            switch (op)
            {
                case 1:
                    Console.WriteLine("=======================");
                    Console.WriteLine("\tUATPay");
                    Console.WriteLine("=======================");
                    Console.WriteLine("Proporcione los siguientes datos.");
                    Console.Write("Usuario: ");
                    string user = Console.ReadLine();
                    Console.Write("Contraseña: ");
                    string pass = Console.ReadLine();
                    break;
                case 2:
                    Console.WriteLine("=======================");
                    Console.WriteLine("\tUATPay");
                    Console.WriteLine("=======================");
                    Console.WriteLine("Proporcione los siguientes datos.");
                    Console.Write("Nombre: ");
                    string rName = Console.ReadLine();
                    Console.Write("Apellidos: ");
                    string rLastN = Console.ReadLine();
                    Console.Write("Edad: ");
                    int rAge = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Tipo de cuenta (chequera / credito / inversion ): ");
                    string rType = Console.ReadLine();
                    break;
            }
            Console.WriteLine("Gracias por usar UAT-Pay!");
            
        }
    }
}
