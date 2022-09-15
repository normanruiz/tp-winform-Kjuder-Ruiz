using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;
using Controlador;

namespace Vista
{
    public partial class frmCreaActuliza : Form
    {
        public frmCreaActuliza()
        {
            InitializeComponent();
            lblTitulo.Text = "Alta de Articulo";
        }

        public frmCreaActuliza(Articulo ariculo)
        {
            InitializeComponent();
            lblTitulo.Text = "Actualizacion de Articulo";
            tbxCodigo.Enabled = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCreaActuliza_Load(object sender, EventArgs e)
        {
            CargarMarcas();
            CargarCategorias();
        }

        private void CargarMarcas()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            try
            {
                cbxMarca.DataSource = marcaNegocio.listar();
            }
            catch (Exception excepcion)
            {
                MessageBox.Show("Verificar la conexion y/o configuracion", "Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CargarCategorias()
        {
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
            try
            {
                cbxCategoria.DataSource = categoriaNegocio.listar();
            }
            catch (Exception excepcion)
            {
                MessageBox.Show("Verificar la conexion y/o configuracion", "Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
