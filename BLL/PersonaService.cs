using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;
using System.Data.SqlClient;


namespace BLL
{
    public class PersonaService
    {
        SqlConnection connection;
        string CadenaConexion = @"Data Source=SERVIDOR\SQLEXPRESS;Initial Catalog=BDPulsacion2020;Integrated Security=True";
        PersonaRepository personaRepository;
        
       


        public PersonaService()
        {
            connection = new SqlConnection(CadenaConexion);
            personaRepository = new PersonaRepository(connection);
        }



        public string Guardar(Persona persona)
        {
            try
            {
                connection.Open();
                personaRepository.Guardar(persona);
                return $"Los datos de la persona {persona.Nombre} han sido guardados satiafactoriamente";


            }
            catch (Exception e)
            {
                return $"Error de base de datos: {e.Message}";
            }
            finally
            {
                connection.Close();
            }


        }

        public RespuestaConsultar Consultar()
        {
            RespuestaConsultar respuesta = new RespuestaConsultar();
            try
            {
                connection.Open();
                respuesta.Error = false;
                respuesta.personas = new List<Persona>();
                respuesta.personas = personaRepository.Consultar();
                connection.Close();
                return respuesta;

            }
            catch (Exception e)
            {
                respuesta.Error = true;
                respuesta.Mensaje = $"error de datos" + e.Message;

            }
            return null;
        }

        public RespuestaBusqueda Buscar(string identificacion)
        {
            RespuestaBusqueda respuesta = new RespuestaBusqueda();
            try
            {
                connection.Open();
                respuesta.Error = false;
                respuesta.persona = personaRepository.Buscar(identificacion);
                connection.Close();
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.Error = true;
                respuesta.Mensaje = "error de datos" + e.Message;
            }
            return null;
        }



        public string Eliminar(string identificacion)
        {
            try
            {
                connection.Open();
                    personaRepository.Eliminar(identificacion);
                    return $"Los datos de la persona han sido eliminados satiafactoriamente";
                
               
            }
            catch (Exception e)
            {
                return $"Error de la aplicacion: {e.Message}";
            }
            finally
            {
                connection.Close();
            }
        }


        public string Modificar(Persona persona)

        {
            try
            {
                connection.Open();
                personaRepository.Modificar(persona);
                connection.Close();

                return "Registro Modificado correctamente";
            }
            catch (Exception e)
            {

                return $"Error de base de datos {e.Message.ToString()}";
            }
            finally
            {
                connection.Close();
            }

        }

        public RespuestaTotal TotalPersonas()
        {
            RespuestaTotal respuesta = new RespuestaTotal();
            try
            {
                respuesta.Error = false;
                respuesta.Total = personaRepository.TotalPersonas();
                if (respuesta.Total==0)
                {
                    respuesta.Mensaje = "No hay datos, no se puede totalizar";
                }
            }
            catch (Exception e)
            {

                respuesta.Error = true;
                respuesta.Mensaje = "Numero de personas encontradas correctamente"+e.Message;
                
            }
            return respuesta;
        }

        public RespuestaTotal Totaltipo(string tipo)
        {

            RespuestaTotal respuesta = new RespuestaTotal();
            try
            {
                respuesta.Error = false;
                respuesta.Total = personaRepository.Totaltipo(tipo);
                if (respuesta.Total == 0)
                {
                    respuesta.Mensaje = "No hay datos, no se puede encontrar cuantps tipos hay";
                }
            }
            catch (Exception)
            {

                respuesta.Error = true;
                respuesta.Mensaje = "Numero de tipos encontradas correctamente";

            }
            return respuesta;
        }
        public RespuestaListaTipo listarTipo(string tipo)
        {
            RespuestaListaTipo respuesta = new RespuestaListaTipo();
            try
            {
                respuesta.personas = personaRepository.listarTipo(tipo);
                if (respuesta.personas.Count==0)
                {
                    respuesta.Mensaje = " No hay datos en el archivo";

                }
                else
                {
                    respuesta.Mensaje = "Datos consultados correctamente";
                }
               
                return respuesta;
            }
            catch (Exception e)
            {

                respuesta.Mensaje = "Error de Archivo"+e.Message;
                return respuesta;
            }
        }



       
    }

    public class RespuestaConsultar
    {
        public string Mensaje { get; set; }
        public IList<Persona> personas { get; set; }
        public bool Error { get; set; }
    }

    public class RespuestaBusqueda
    {
        public string Mensaje { get; set; }
        public Persona persona { get; set; }
        public bool Error { get; set; }
    }

    public class RespuestaListaTipo
    {
        public string Mensaje { get; set; }
        public IList<Persona> personas { get; set; }

    }

    public class RespuestaTotal
    {
        public string Mensaje { get; set; }
        public bool Error { get; set; }
        public int Total { get; set; }
    }

}
