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
        SqlConnection connection;
        
        List<Persona> personas;

        public PersonaRepository(SqlConnection connectionDb)
        {
            connection = connectionDb;
            personas = new List<Persona>();
        }

        public void Guardar (Persona persona)
        {
            using (var comando = connection.CreateCommand())
            {
                comando.CommandText = " Insert  Into Persona(Identificacion,Nombre,Edad,Sexo,Pulsacion)" +
                    "Values(@identificacion,@Nombre,@Edad,@Sexo,@Pulsacion)";
                comando.Parameters.AddWithValue("@identificacion", persona.Identificacion);
                comando.Parameters.AddWithValue("@Nombre", persona.Nombre);
                comando.Parameters.AddWithValue("@Edad", persona.Edad);
                comando.Parameters.AddWithValue("@Sexo", persona.Sexo);
                comando.Parameters.AddWithValue("@Pulsacion", persona.Pulsacion);
                comando.ExecuteNonQuery();

            }
        }

        public Persona Buscar(string identificacion)
        {
            using (var Comando = connection.CreateCommand())
            {
                Comando.CommandText = "Select * from Persona where Identificacion =@identificacion";
                Comando.Parameters.AddWithValue("@Identificacion", identificacion);
                var Reader = Comando.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Persona persona = new Persona();
                        persona = Mapear(Reader);
                        return Mapear(Reader);
                    }
                }
            }
            return null;
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

        public List<Persona> Consultar()
        {
            personas.Clear();
            using (var comando = connection.CreateCommand())
            {
                comando.CommandText = "Select * from Persona";
                var Reader = comando.ExecuteReader();
                while (Reader.Read())
                {
                    Persona persona = new Persona();
                    persona = Mapear(Reader);
                      personas.Add(persona);
                }
            }
                return personas;
        }

        public Persona Mapear(SqlDataReader reader)
        {
            Persona persona = new Persona();
            persona.Identificacion = (string)reader["identificacion"];
            persona.Nombre = (string)reader["Nombre"];
            persona.Edad = (int)reader["Edad"];
            persona.Sexo = (string)reader["Sexo"];
            persona.Pulsacion = (decimal)reader["Pulsacion"];
            return persona;
        }

        public void Eliminar(string identificacion)
        {
            using(var comando = connection.CreateCommand()){
                comando.CommandText = "Delete from Persona where identificacion = @Identificacion";
                comando.Parameters.AddWithValue("@Identificacion", identificacion);
                comando.ExecuteNonQuery();
            }

        }

        public void Modificar(Persona persona)
        {

            using (var comando = connection.CreateCommand())
            {
                comando.CommandText = "Update Persona set  nombre=@nombre,edad=@edad,sexo=@sexo,pulsacion=@pulsacion where identificacion = @Identificacion";
                comando.Parameters.AddWithValue("@Identificacion", persona.Identificacion);
                comando.Parameters.AddWithValue("@Nombre", persona.Nombre);
                comando.Parameters.AddWithValue("@Edad", persona.Edad);
                comando.Parameters.AddWithValue("@Sexo", persona.Sexo);
                comando.Parameters.AddWithValue("@Pulsacion", persona.Pulsacion);
                comando.ExecuteNonQuery();
            }
        }


    }

   
    
    }













    

