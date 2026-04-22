using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Final
{
    // CLASE PRINCIPAL - CUENTA
    public abstract class Cuenta
    {
        private int numRegistro;
        private string nombres;
        private string apellidos;
        private int edad;
        private string fechaApt;
        private double saldo;
        private string tipo;


    }
    // CLASE  SECUNDARIAS - CHEQUERA
    public class Chequera : Cuenta
    {

    }
    // CLASE - CREDITO
    public class Credito : Cuenta
    {
        private int numTarjeta;
    }
    // CLASE - INVERSION
    public class Inversion : Cuenta
    {
        private double tasa;
        private string periodo;
    }
}
