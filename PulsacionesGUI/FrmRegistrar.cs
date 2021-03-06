﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entity;
using BLL;


namespace PulsacionesGUI
{
    public partial class FrmRegistrar : Form
    {
        
        PersonaService personaService;
        public FrmRegistrar()
        {
            InitializeComponent();
            
            personaService = new PersonaService(ConfigConnection.connectionString);
            
        }
       
        private void BtnAgragr_Click(object sender, EventArgs e)
        {
            Persona persona = new Persona();
            persona.Identificacion = TxtIdentificacion.Text;
            persona.Nombre = TxtNombre.Text;
            persona.Edad = Convert.ToInt32(TxtEdad.Text);
            persona.Sexo = CmbGenero.Text;
            persona.CalcularPulsaciones();
            TxtPulsacion.Text = persona.Pulsacion.ToString();
            string mensaje = personaService.Guardar(persona);
            MessageBox.Show(mensaje, "Mensaje de Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Limpiar();
        }


        private void Limpiar()
        {
            TxtIdentificacion.Text = "";
            TxtNombre.Text = "";
            TxtEdad.Text = "";
            CmbGenero.Text = " ";
            TxtPulsacion.Text = "";
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string identificacion = TxtIdentificacion.Text;

            if (identificacion != "")
            {
                
                  Persona persona= personaService.Buscar(identificacion);
                if (persona != null)
                {
                    TxtNombre.Text = persona.Nombre;
                    TxtEdad.Text = persona.Edad.ToString();
                    CmbGenero.Text = persona.Sexo;
                    TxtPulsacion.Text = persona.Pulsacion.ToString();
                }
                else
                {
                    MessageBox.Show("la persona no se encuentra en el sistema");
                    
                }
            }
            else
            {
                MessageBox.Show("Por favor digite la identificacion a buscar");

            }
            TxtIdentificacion.Focus();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            string identificacion = TxtIdentificacion.Text;
            if (identificacion != "")
            {

                Persona persona = personaService.Buscar(identificacion);
                if (persona != null)
                {
                    var respuestaa = MessageBox.Show("Esta seguro que desea eliminar esta persona?", "", MessageBoxButtons.YesNo);
                    if (respuestaa == DialogResult.Yes)
                    {
                        string mensaje = personaService.Eliminar(identificacion);
                        MessageBox.Show(mensaje, "Mesaje de Eliminacion", MessageBoxButtons.OKCancel);
                        Limpiar();

                    }
                    else
                    {
                        MessageBox.Show($" la identificacion {identificacion} no esta en el sistema");
                    }

                }
                else
                {
                    MessageBox.Show($" Digite la identificacion por favor ");
                    TxtIdentificacion.Focus();
                }
            }
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            TxtIdentificacion.Text = "";
            TxtNombre.Text = "";
            TxtEdad.Text = "";
            CmbGenero.Text = "Seleccione";
            TxtPulsacion.Text = "";
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            
            string identificacion = TxtIdentificacion.Text;
            if (identificacion!="")
            {
                
                Persona persona = personaService.Buscar(identificacion);
                if (persona != null)
                {
                    persona.Nombre = TxtNombre.Text;
                    persona.Edad = Convert.ToInt32(TxtEdad.Text);
                    persona.Sexo = CmbGenero.Text;
                    persona.CalcularPulsaciones();
                    TxtPulsacion.Text =persona.Pulsacion.ToString();
                    var respuestaa = MessageBox.Show("Esta seguro que desea modificar al usuario?", "", MessageBoxButtons.YesNo);
                    if (respuestaa == DialogResult.Yes)
                    {
                        string mensaje = personaService.Modificar(persona);
                        MessageBox.Show(mensaje, "Mensaje de Modificacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Limpiar();
                    }

                }
                

            }
            else
            {
                MessageBox.Show("Por favor digite la busque la identificacion para modificar ");
                TxtIdentificacion.Focus();
            }
        }

        private void BtnVolver_Click(object sender, EventArgs e)
        {
            FrmPrincipal frmPrincipal = new FrmPrincipal();
            frmPrincipal.Show();
        }
    }
}



