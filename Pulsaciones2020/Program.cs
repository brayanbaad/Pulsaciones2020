using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pulsaciones2020
{
    class Program
    {
       

        static void Main(string[] args)
        {
            int edad;
            char sexo;
            decimal pulsacion;
            Console.WriteLine("Digite sexo de la persona  m/f :");
            sexo = Convert.ToChar( Console.ReadLine());
            Console.WriteLine(" Digite la edad ");
            edad = Convert.ToInt32(Console.ReadLine());
            if (sexo == 'f')
            {
                pulsacion = (220 - edad) / 10;
            }
            else
            {
             
                    pulsacion = (210 - edad) / 10;

            }
            Console.WriteLine( $" la pulsacion es de {pulsacion}");
            Console.ReadKey();
                

        }
    }
}
