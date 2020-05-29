using System;
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
using System.Configuration;

namespace PulsacionesGUI
{
    public partial class FrmConsultar : Form
    {
        IList<Persona> personas = new List<Persona>();
        PersonaService personaService;
        public FrmConsultar()
        {

            InitializeComponent();
            
            personaService = new PersonaService(ConfigConnection.connectionString);
            
        }

        private void BtnActualizar_Click(object sender, EventArgs e)
        {
           
        }

        private void BtnVolver_Click(object sender, EventArgs e)
        {

            FrmPrincipal frmPrincipal = new FrmPrincipal();
            frmPrincipal.Show();
        }

        private void BtnConsultar_Click(object sender, EventArgs e)
        {
            if (CmbFiltro.Text.Equals("Todos"))
            {
                DtgPersonas.DataSource = null;
                personas.Clear();
                personas = personaService.Consultar();
                DtgPersonas.DataSource = personas;
                //RespuestaTotal total = new RespuestaTotal();
                // total= personaService.TotalPersonas();
                //TxtPersonas.Text = total.Total.ToString();
                // total = personaService.Totaltipo("M");
                // TxtHombres.Text = total.Total.ToString();
                //total = personaService.Totaltipo("F");
                // TxtMujeres.Text = total.Total.ToString();
            }
            else if (CmbFiltro.Text.Equals("M"))
            {
                DtgPersonas.DataSource = null;
                RespuestaListaTipo respuesta = new RespuestaListaTipo();
                RespuestaTotal total = new RespuestaTotal();
                respuesta = personaService.listarTipo("M");
                MessageBox.Show(respuesta.Mensaje);
                DtgPersonas.DataSource = respuesta.personas;
                TxtPersonas.Text = "";
               total = personaService.Totaltipo("M");
                TxtHombres.Text = total.Total.ToString();
                
                TxtMujeres.Text = "";
            }
            else if(CmbFiltro.Text.Equals("F"))
            {
                DtgPersonas.DataSource = null;
                RespuestaListaTipo respuesta = new RespuestaListaTipo();
                RespuestaTotal total = new RespuestaTotal();
                respuesta = personaService.listarTipo("F");
                MessageBox.Show(respuesta.Mensaje);
                DtgPersonas.DataSource = respuesta.personas;
                TxtPersonas.Text = "";
                TxtHombres.Text = "";
               total = personaService.Totaltipo("F");
                TxtMujeres.Text = total.Total.ToString();
            }
        }
    }
}
