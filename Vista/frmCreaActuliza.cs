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
        private Articulo articulo = null;
        public frmCreaActuliza()
        {
            InitializeComponent();
            lblTitulo.Text = "Alta de Articulo";
        }

        public frmCreaActuliza(Articulo articulo)
        {
            InitializeComponent();
            lblTitulo.Text = "Actualizacion de Articulo";
            tbxCodigo.Enabled = false;
            this.articulo = articulo;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCreaActuliza_Load(object sender, EventArgs e)
        {
            CargarMarcas();
            CargarCategorias();
            if(articulo != null)
            {
                lblId.Text = articulo.Id.ToString();
                tbxCodigo.Text = articulo.Codigo;
                tbxNombre.Text = articulo.Nombre;
                tbxDescripcion.Text = articulo.Descripcion;
                cbxMarca.SelectedValue = articulo.marca.Id;
                cbxCategoria.SelectedValue = articulo.categoria.Id;
                tbxPrecio.Text = articulo.Precio.ToString();
                tbxImagenUrl.Text = articulo.ImagenUrl;
                CargarImagen();
            }
        }

        private void CargarMarcas()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            try
            {
                cbxMarca.DataSource = marcaNegocio.listar();
                cbxMarca.ValueMember = "Id";
                cbxMarca.DisplayMember = "Descripcion";
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
                cbxCategoria.ValueMember = "Id";
                cbxCategoria.DisplayMember = "Descripcion";

            }
            catch (Exception excepcion)
            {
                MessageBox.Show("Verificar la conexion y/o configuracion", "Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            try
            {
                if(articulo == null)
                { 
                    articulo = new Articulo(); 
                }
                articulo.Codigo = tbxCodigo.Text;
                articulo.Nombre = tbxNombre.Text;
                articulo.Descripcion = tbxDescripcion.Text;
                articulo.marca = (Marca)cbxMarca.SelectedItem;
                articulo.categoria = (Categoria)cbxCategoria.SelectedItem;
                articulo.Precio = Convert.ToDecimal(tbxPrecio.Text);
                articulo.ImagenUrl = tbxImagenUrl.Text;

                if(articulo.Id != 0)
                {
                    articuloNegocio.actualizar(articulo);
                }
                else
                {
                    articuloNegocio.crear(articulo);
                }

                this.Close();
            }
            catch (Exception excepcion)
            {
                MessageBox.Show("Todabia no se que paso... Dios!!!!!", "Creando articulo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCargarImagen_Click(object sender, EventArgs e)
        {
            CargarImagen();
        }

        private void CargarImagen()
        {
            try
            {
                pbxImagenUrl.Load(tbxImagenUrl.Text);
            }
            catch (Exception excepcion)
            {
                pbxImagenUrl.Load("https://www.dotcom-monitor.com/blog/wp-content/uploads/sites/3/2019/09/404-error.jpg");
                tbxImagenUrl.Text = "";
                MessageBox.Show(excepcion.ToString(), "Cargando imagen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
