using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using BLL;


namespace Pulsacion2020
{
    class Program
    {
        static Persona persona = new Persona();
        static PersonaService personaService = new PersonaService();
        static void Main(string[] args)
        {

            Menu();


        }

        public static void Menu()
        {
            int opcion;
            do
            {

                Console.Clear();
                Console.WriteLine("  °°°°°  MENU    °°°°°");
                Console.WriteLine(" 1. Registrar");
                Console.WriteLine(" 2. Eliminar");
                Console.WriteLine(" 3. ConsultarRegistros");
                Console.WriteLine(" 4. Buscar");
                Console.WriteLine(" 5. Modificar");
                Console.WriteLine(" 6. Salir");
                Console.Write("\n DIGITE UNA OPCION >>> ");
                opcion = Int32.Parse(Console.ReadLine());

                switch (opcion)
                {

                    case 1: Registrar(); break;
                    case 2: Eliminar(); break;
                    case 3: ConsultarRegistros(); break;
                    case 4: Buscar(); break;
                    case 5: Modificar(); break;
                    case 6:
                        Console.WriteLine(" Saliendo ...");
                        Console.ReadKey(); break;
                    default:
                        Console.Write("\n Numero fuera de rango intente de nuevo...");
                        Console.ReadKey(); break;

                }


            } while (opcion != 5);
        }



        public static void Registrar()
        {
            ConsoleKeyInfo continuar;

            do
            {

                Console.WriteLine(" Digite la Identificacion");
                persona.Identificacion = Console.ReadLine();

                Console.WriteLine(" Digite la Nombre");
                persona.Nombre = Console.ReadLine();

                Console.WriteLine("Digite sexo de la persona  M/F :");
                persona.Sexo = Console.ReadLine();

                Console.WriteLine(" Digite la Edad");
                persona.Edad = Convert.ToInt32(Console.ReadLine());

                personaService.CalcularPulsacion(persona);
                string mensaje = personaService.Guardar(persona);
                Console.WriteLine(mensaje);

                Console.WriteLine("Desea  registrar otra persona ? (s/n)");
                continuar = Console.ReadKey();
            } while (continuar.KeyChar == ('s') || continuar.KeyChar == ('S'));

            
        }


        public static void ConsultarRegistros()
        {
            Console.WriteLine("------------lista de usuarios---------------");

            foreach (var item in personaService.Consultar().ToString());
            {
                Console.WriteLine();

            }
            Console.WriteLine("--------------------------------------------");
            Console.ReadKey();
        }

        public static void Eliminar()
        {
            ConsoleKeyInfo continuar;
            Console.WriteLine("Digite la identificacion para eliminar :");
            string identificacion = Console.ReadLine();
            string mensajeEliminar = personaService.Eliminar(identificacion);
            Console.WriteLine(mensajeEliminar);
            continuar = Console.ReadKey();
        }

        public static void Buscar()
        {
            ConsoleKeyInfo continuar;
            Console.WriteLine("Digite la identificacion a buscar");
            string identificacionBuscada = Console.ReadLine();
            string personaBuscada = personaService.Buscar(identificacionBuscada).ToString();
            Console.WriteLine(personaBuscada);
            Console.WriteLine("--------------------------------------------");
            continuar = Console.ReadKey();

        }


        public static void Modificar()
        {
            Persona persona = new Persona();
            Console.WriteLine("Digite la identificacion a modificar :");
             persona.Identificacion = Console.ReadLine();
            if (personaService.Buscar(persona.Identificacion) !=null)
            {
                Console.WriteLine("Digite el Nombre a modificar :");
                persona.Nombre = Console.ReadLine();
                Console.WriteLine("Digite la Edad a modificar: ");
                persona.Edad = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Digite el sexo a modificar :");
                persona.Sexo = Console.ReadLine();
                personaService.CalcularPulsacion(persona);
                string mensaje = personaService.ModificarEnArchivo(persona);
                Console.WriteLine(mensaje);
                Menu();

            }

            Console.ReadKey();


        }



        
            

        }

    }


        
 

