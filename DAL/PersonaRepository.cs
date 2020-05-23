using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.IO;
using System.Data;
using System.Data.SqlClient;


namespace DAL
{
    public class PersonaRepository
    {
          private string FileName = @"Personas.Txt";
        
        private IList<Persona> personas;

        public PersonaRepository()
        {
            personas = new List<Persona>();
        }

        public void Guardar (Persona persona)
        {
            FileStream file = new FileStream(FileName, FileMode.Append);
            StreamWriter escritor = new StreamWriter(file);
            escritor.WriteLine(persona.Identificacion + ";" +persona.Nombre +";" +persona.Edad+";"+persona.Sexo +";"+persona.Pulsacion);
            escritor.Close();
            file.Close();
        }

        public Persona Buscar(string identificacion)
        {
            personas = Consultar();
           return personas.Where(p => p.Identificacion == identificacion).FirstOrDefault();
        }

        public int TotalPersonas()
        {
            personas = Consultar();
            return personas.Count();
        }

        //public int TotalMujeres()
        //{
        //    personas = ConsultaGeneral();
        //    return personas.Where(p=>p.Sexo.Equals("F")).Count();
        //}
        //public int TotalHombre()
        //{
        //    personas = ConsultaGeneral();
        //    return personas.Where(p => p.Sexo.Equals("M")).Count();
        //}
        public int Totaltipo( string tipo)
        {
            personas = Consultar();
            return personas.Where(p => p.Sexo.Equals(tipo)).Count();
        }
        //public IList<Persona> listarHombre()
        //{
        //    personas = ConsultaGeneral();
        //    return personas.Where(p => p.Sexo.Equals("M")).ToList();
        //}
        //public IList<Persona> listarMujeres()
        //{
        //    personas = ConsultaGeneral();
        //    return personas.Where(p => p.Sexo.Equals("F")).ToList();
        //}
        public IList<Persona> listarTipo(string tipo)
        {
            personas = Consultar();
            return personas.Where(p => p.Sexo.Equals(tipo)).ToList();
        }

        public IList<Persona> Consultar()
        {
            FileStream SourseStream = new FileStream(FileName, FileMode.OpenOrCreate);
            StreamReader reader = new StreamReader(SourseStream);
            string Linea = string.Empty;
            personas.Clear();

            while ((Linea = reader.ReadLine()) != null)
            {
                Persona persona = Mapear(Linea);
                personas.Add(persona);
            }
            reader.Close();
            SourseStream.Close();

            return personas;
        }

        private Persona Mapear(string Linea)
        {
            char delimitador = ';';
            string[] DatosPersona = Linea.Split(delimitador);
            Persona persona = new Persona();
            persona.Identificacion =( DatosPersona[0]);
            persona.Nombre = DatosPersona[1];
            persona.Edad = Convert.ToInt32(DatosPersona[2]);
            persona.Sexo = DatosPersona[3];
            persona.Pulsacion =Convert.ToDecimal( DatosPersona[4]);

            return persona;
        }

        public void Eliminar(Persona persona)
        {
            personas = Consultar();
            FileStream SourseStream = new FileStream(FileName, FileMode.Create);
            SourseStream.Close();
            foreach (var item in personas)
            {
                if (persona.Identificacion != item.Identificacion)
                {
                    Guardar(item);
                }

            }

        }

        public void ModificarEnArchivo(Persona persona)
        {
            personas.Clear();
            personas = Consultar();
            FileStream SourceStream = new FileStream(FileName, FileMode.Create);
            SourceStream.Close();
            foreach (var item in personas)
            {
                if (item.Identificacion != persona.Identificacion)
                {
                    Guardar(item);
                }
                else
                {
                    Guardar(persona);
                }
            }
        }


    }

   
    
    }













    

