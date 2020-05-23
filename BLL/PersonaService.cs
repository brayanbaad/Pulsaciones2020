using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using DAL;


namespace BLL
{
    public class PersonaService
    {
        private PersonaRepository personaRepository;


        public PersonaService()
        {
            personaRepository = new PersonaRepository();
        }

        public void CalcularPulsacion(Persona persona)
        {
            if (persona.Sexo.ToUpper().Equals('F'))
            {
                persona.Pulsacion = (220 - persona.Edad) / 10;
            }
            else
            {

                persona.Pulsacion = (210 - persona.Edad) / 10;

            }

        }

        public string Guardar(Persona persona)
        {
            try
            {
                if (personaRepository.Buscar(persona.Identificacion) == null)
                {
                    personaRepository.Guardar(persona);
                    return $"Los datos de la persona {persona.Nombre} han sido guardados satiafactoriamente";
                }
                else
                {
                    return $"Lo sentimos, los datos de {persona.Nombre} ya se encuantran registrados";
                }
            }
            catch (Exception e)
            {
                return $"Error de la aplicacion: {e.Message}";
            }


        }

        public RespuestaConsultar Consultar()
        {
            RespuestaConsultar respuesta = new RespuestaConsultar();
            try
            {
                respuesta.Error = false;
                respuesta.personas = personaRepository.Consultar();
                if (respuesta.personas!=null)
                {
                    respuesta.Mensaje = "Se consulta la informacion de personas";
                }
                else
                {
                    respuesta.Mensaje = "No existen datos para consultar";
                }

            }
            catch (Exception e)
            {
                respuesta.Error = true;
                respuesta.Mensaje = $"error de datos"+e.Message;

            }
            return respuesta;
        }

        public  RespuestaBusqueda Buscar(string identificacion)
        {
            RespuestaBusqueda respuesta = new RespuestaBusqueda();
            try
            {
                respuesta.Error = false;
                respuesta.persona = personaRepository.Buscar(identificacion);
                if (respuesta.persona!=null)
                {
                    respuesta.Mensaje = "Se consultaron los datos satisfariamente";
                }
                else
                {
                    respuesta.Mensaje = "La persona solicitada no existe";
                }
                return respuesta;
            }
            catch (Exception e)
            {
                respuesta.Error = true;
                respuesta.Mensaje = "error de datos" + e.Message;
                respuesta.persona = null;
                return respuesta;
            }
        }



        public string Eliminar(string identificacion)
        {
            try
            {
                Persona persona = personaRepository.Buscar(identificacion);

                if (persona != null)
                {
                    personaRepository.Eliminar(persona);
                    return $"Los datos de la persona {persona.Nombre} han sido eliminados satiafactoriamente";
                }
                else
                {
                    return $"Lo sentimos, los datos de {persona.Nombre} no se encuantran registrados";
                }
            }
            catch (Exception e)
            {
                return $"Error de la aplicacion: {e.Message}";
            }
        }


        public string ModificarEnArchivo(Persona persona)

        {
            PersonaRepository personaRepository = new PersonaRepository();
            Persona persona1 = new Persona();
            persona1 = personaRepository.Buscar(persona.Identificacion);

            if (persona1 != null)
            {
                persona1.Identificacion = persona.Identificacion;
                persona1.Nombre = persona.Nombre;
                persona1.Edad = persona.Edad;
                persona1.Sexo = persona.Sexo;
                personaRepository.ModificarEnArchivo(persona);
                return $"La persona se ha modificado correctamente";
            }

            return $"La persona no se encuentra registrada";

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
            catch (Exception e)
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
