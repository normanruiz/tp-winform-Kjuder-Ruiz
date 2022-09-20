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

        private void frmCreaActuliza_Load(object sender, EventArgs e)
        {
            try
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
            catch (Exception excepcion)
            {
                if (articulo != null)
                {
                    MessageBox.Show(excepcion.ToString(), "Cargando ventana crear articulo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(excepcion.ToString(), "Cargando ventana Actualizar articulo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
                if (validarCampos())
                {
                    if (articulo == null)
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

                    if (articulo.Id != 0)
                    {
                        articuloNegocio.actualizar(articulo);
                    }
                    else
                    {
                        articuloNegocio.crear(articulo);
                    }

                    this.Close();
                }
            }
            catch (Exception excepcion)
            {
                MessageBox.Show("Todabia no se que paso... Dios!!!!!", "Creando articulo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private Boolean validarCampos()
        {
            Boolean estado = true;
            Boolean precio = true;
            try
            {
                // Validar codigo
                if(tbxCodigo.Text.Length == 0)
                {
                    tbxCodigo.BackColor = Color.Firebrick;
                    MessageBox.Show("El codigo no puede estar vacio.", "Validando Codigo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    estado = false;
                }
                else if (articulo == null)
                {
                    if (validarCodigo(tbxCodigo.Text))
                    {
                        tbxCodigo.BackColor = Color.Firebrick;
                        MessageBox.Show("El codigo ya existe, intente con otro.", "Validando Codigo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        estado = false;
                    }
                }

                //Validar Nombre
                if (tbxNombre.Text.Length == 0)
                {
                    tbxNombre.BackColor = Color.Firebrick;
                    MessageBox.Show("El Nombre no puede estar vacio.", "Validando Nombre", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    estado = false;
                }

                //Validar Descripcion
                if (tbxDescripcion.Text.Length == 0)
                {
                    tbxDescripcion.BackColor = Color.Firebrick;
                    MessageBox.Show("El Descripcion no puede estar vacio.", "Validando Descripcion", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    estado = false;
                }

                //Validar Precio
                if (tbxPrecio.Text.Length == 0)
                {
                    tbxPrecio.BackColor = Color.Firebrick;
                    MessageBox.Show("El precio no puede estar vacio.", "Validando Precio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    estado = false;
                }
                else
                {
                    foreach (char caracter in tbxPrecio.Text)
                    {
                        if(!(char.IsNumber(caracter) || caracter.ToString() == ","))
                        {
                            tbxPrecio.BackColor = Color.Firebrick;
                            estado = false;
                            precio = false;
                        }
                    }
                    if(precio == false)
                    {
                        MessageBox.Show("El precio solo puede contener digitos numericos.", "Validando Precio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                //Validar ImagenURL
                if (tbxImagenUrl.Text.Length == 0)
                {
                    tbxImagenUrl.BackColor = Color.Firebrick;
                }

                return estado;
            }
            catch (Exception excepcion)
            {
                MessageBox.Show(excepcion.ToString(), "Validando campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return estado;
            }
        }

        private Boolean validarCodigo(string codigo)
        {
            Boolean existe = true;
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            
            try
            {
                existe = articuloNegocio.encontrarCodigo(codigo);
                return existe;
            }
            catch (Exception excepcion)
            {
                MessageBox.Show("Todabia no se que paso... Dios!!!!!", "Creando articulo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return existe;
            }
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception excepcion)
            {
                MessageBox.Show(excepcion.ToString(), "Cancelando creacion de articulo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
               if (!(tbxImagenUrl.Text == "" || tbxImagenUrl.Text == "Sin imagen"))
                {
                    pbxImagenUrl.Load(tbxImagenUrl.Text);
                }
            }
            catch (Exception excepcion)
            {
                pbxImagenUrl.Load("https://www.dotcom-monitor.com/blog/wp-content/uploads/sites/3/2019/09/404-error.jpg");
                tbxImagenUrl.Text = "";
                MessageBox.Show("Verifique la URL de la imagen o pruebe con otra.", "Cargando imagen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tbxCodigo_TextChanged(object sender, EventArgs e)
        {
            if (tbxCodigo.Text.Length != 0)
            {
                tbxCodigo.BackColor = Color.Teal;
            }
        }

        private void tbxNombre_TextChanged(object sender, EventArgs e)
        {
            if (tbxNombre.Text.Length != 0)
            {
                tbxNombre.BackColor = Color.Teal;
            }
        }

        private void tbxDescripcion_TextChanged(object sender, EventArgs e)
        {
            if (tbxDescripcion.Text.Length != 0)
            {
                tbxDescripcion.BackColor = Color.Teal;
            }
        }

        private void tbxImagenUrl_TextChanged(object sender, EventArgs e)
        {
            if (tbxImagenUrl.Text.Length != 0)
            {
                tbxImagenUrl.BackColor = Color.Teal;
            }
        }

        private void tbxPrecio_TextChanged(object sender, EventArgs e)
        {
            if (tbxPrecio.Text.Length != 0)
            {
                tbxPrecio.BackColor = Color.Teal;
            }
        }
    }
}
