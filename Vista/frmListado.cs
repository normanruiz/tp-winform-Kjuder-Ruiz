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
    public partial class frmListado : Form
    {
        private List<Articulo> listadoArticulos;
        public frmListado()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            foreach ( var item in Application.OpenForms)
            {
                if (item.GetType() == typeof(Detalles)) return;
            }
            Detalles formulario = new Detalles();
            formulario.Show();

        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            frmCreaActuliza VentanaCrear = new frmCreaActuliza();
            VentanaCrear.ShowDialog();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Articulo articulo = new Articulo();
            frmCreaActuliza VentanaCrear = new frmCreaActuliza(articulo);
            VentanaCrear.ShowDialog();
        }

        private void frmListado_Load(object sender, EventArgs e)
        {
            actualizarListado();
            this.dgvListado.Columns["Id"].Visible = false;
            this.dgvListado.Columns["Descripcion"].Visible = false; 
            this.dgvListado.Columns["Marca"].Visible = false;
            this.dgvListado.Columns["Categoria"].Visible = false;
            this.dgvListado.Columns["ImagenURL"].Visible = false;
            this.dgvListado.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.dgvListado.Columns["Precio"].DefaultCellStyle.Format = "C";
            this.dgvListado.Columns["Precio"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        private void actualizarListado()
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            try
            {
                listadoArticulos = articuloNegocio.listar();
                dgvListado.DataSource = listadoArticulos;
            }
            catch (Exception excepcion)
            {
                MessageBox.Show("Verificar la conexion y/o configuracion", "Base de Datos",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tbxFiltro_TextChanged(object sender, EventArgs e)
        {
            if(tbxFiltro.Text.Length == 0)
            {
                dgvListado.DataSource = listadoArticulos;
            }
            else
            {
                List<Articulo> listadoFiltrado;
                string filtro = tbxFiltro.Text;
                listadoFiltrado = listadoArticulos.FindAll(x => x.Codigo.Contains(filtro) || x.Nombre.ToLower().Contains(filtro.ToLower()) || x.Precio.ToString().Contains(filtro));
                dgvListado.DataSource = listadoFiltrado;
            }
        }
    }
}
