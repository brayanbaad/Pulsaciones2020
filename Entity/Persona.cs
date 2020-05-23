using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Persona
    {
        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public int Edad { get; set; }
        public string Sexo  { get; set; }

        public decimal Pulsacion { get; set; }

        public void CalcularPulsaciones()
        {
            if (Sexo == "F")
            {
                Pulsacion = (220 - Edad) / 10;
            }
            else if (Sexo == "M")
            {
                Pulsacion = (210 - Edad) / 10;
            }
            else
            {
                Pulsacion = 0;
            }
        }


        public override string ToString()
        {
            return $"Identificacion: {Identificacion} Nombre: {Nombre} Edad: {Edad} Sexo: {Sexo} Pulsaciones/10seg: {Pulsacion}";
        }



    }
}
