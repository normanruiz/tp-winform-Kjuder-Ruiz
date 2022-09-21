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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            try
            {
                Application.Exit();
            }
            catch (Exception excepcion)
            {

                MessageBox.Show(excepcion.ToString(), "Cerrando aplicacion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDetalle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListado.CurrentRow != null)
                {
                    Articulo articulo = (Articulo)dgvListado.CurrentRow.DataBoundItem;
                    foreach (var item in Application.OpenForms)
                    {
                        if (item.GetType() == typeof(Detalles)) return;
                    }
                    Detalles formulario = new Detalles(articulo);
                    formulario.Show();
                    actualizarListado();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un elemento primero.", "Actualizar articulo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception excepcion)
            {
                MessageBox.Show(excepcion.ToString(), "Actualizando articulo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }




        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                frmCreaActuliza VentanaCrear = new frmCreaActuliza();
                VentanaCrear.ShowDialog();
                actualizarListado();
            }
            catch (Exception excepcion)
            {
                MessageBox.Show(excepcion.ToString(), "Creando articulo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvListado.CurrentRow != null)
                {
                    Articulo articulo = (Articulo)dgvListado.CurrentRow.DataBoundItem;
                    frmCreaActuliza VentanaCrear = new frmCreaActuliza(articulo);
                    VentanaCrear.ShowDialog();
                    actualizarListado();
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un elemento primero.", "Actualizar articulo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception excepcion)
            {
                MessageBox.Show(excepcion.ToString(), "Actualizando articulo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmListado_Load(object sender, EventArgs e)
        {
            try
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
            catch (Exception excepcion)
            {
                MessageBox.Show(excepcion.ToString(), "Cargando listado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void actualizarListado()
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            try
            {
                listadoArticulos = articuloNegocio.listar();
                dgvListado.DataSource = listadoArticulos;
            }
            catch (InvalidCastException excepcion)
            {
                MessageBox.Show("Verifique los nulos en base de datos", "Actualizando listado",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception excepcion)
            {
                MessageBox.Show(excepcion.ToString(), "Actualizando listado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tbxFiltro_TextChanged(object sender, EventArgs e)
        {
            try
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
            catch (Exception excepcion)
            {
                MessageBox.Show(excepcion.ToString(), "Filtrando articulos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            Articulo articulo;
            try
            {
                DialogResult respuesta = MessageBox.Show("Estas seguro?", "Eliminando articulo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (respuesta == DialogResult.Yes)
                {   
                    if(dgvListado.CurrentRow != null)
                    {
                        articulo = (Articulo)dgvListado.CurrentRow.DataBoundItem;
                        articuloNegocio.Eliminar(articulo.Id);
                        actualizarListado();
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar un elemento primero.", "Eliminar articulo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (NullReferenceException excepcion)
            {
                MessageBox.Show("Debe seleccionar un elemento primero.", "Eliminando articulo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception excepcion)
            {
                MessageBox.Show(excepcion.ToString(), "Eliminando articulo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}
