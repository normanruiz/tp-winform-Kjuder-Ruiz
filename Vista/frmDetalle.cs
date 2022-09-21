using Controlador;
using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vista
{
    public partial class Detalles : Form
    {
        private Articulo articulo = null;
        public Detalles()
        {
            InitializeComponent();
        }
        public Detalles(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
        }

        private void frmDetalle_Load(object sender, EventArgs e)
        {
            try
            {
                if (articulo != null)
                {
                    lblCodigo.Text = articulo.Id.ToString();
                    lblCodigo.Text = articulo.Codigo;
                    lblNombre.Text = articulo.Nombre;
                    lblDescripcion.Text = articulo.Descripcion;
                    lblMarca.Text = articulo.marca.Descripcion;
                    lblCategoria.Text = articulo.categoria.Descripcion;
                    lblPrecio.Text = articulo.Precio.ToString();
                    try
                    {
                        if (!(articulo.ImagenUrl == "" || articulo.ImagenUrl == "Sin imagen"))
                        {
                            imgImagen.Load(articulo.ImagenUrl);
                        }
                    }
                    catch (Exception excepcion)
                    {
                        imgImagen.Load("https://www.dotcom-monitor.com/blog/wp-content/uploads/sites/3/2019/09/404-error.jpg");
                        MessageBox.Show("Verifique la URL de la imagen o pruebe con otra.", "Cargando imagen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception excepcion)
            {
               MessageBox.Show(excepcion.ToString(), "Cargando ventana detalle articulo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
