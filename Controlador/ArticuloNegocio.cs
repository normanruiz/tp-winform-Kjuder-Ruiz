using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo;

namespace Controlador
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> listaArticulos = new List<Articulo>();
            AccesoDatos Conexion = new AccesoDatos();
            Articulo articulo;

            try
            {
                Conexion.conectar();
                Conexion.setearConsulta("SELECT a.[Id], a.[Codigo], a.[Nombre], a.[Descripcion], a.[IdMarca], m.[Descripcion], a.[IdCategoria], c.[Descripcion], a.[ImagenUrl], a.[Precio] FROM [CATALOGO_DB].[dbo].[ARTICULOS] AS a WITH (NOLOCK) INNER JOIN [CATALOGO_DB].[dbo].[CATEGORIAS] AS c WITH (NOLOCK)ON c.[Id] = a.[IdCategoria] INNER JOIN [CATALOGO_DB].[dbo].[MARCAS] AS m WITH (NOLOCK)ON m.[Id] = a.[IdCategoria];");
                Conexion.ejecutarLectura();
                
                while(Conexion.Lector.Read())
                {
                    articulo = new Articulo();
                    articulo.Id = (Int32)Conexion.Lector["Id"];
                    articulo.Codigo = (string)Conexion.Lector["Codigo"];
                    articulo.Nombre = (string)Conexion.Lector["Nombre"];
                    articulo.Descripcion = (string)Conexion.Lector["Descripcion"];
                    articulo.marca = new Marca();
                    articulo.marca.Id = (Int32)Conexion.Lector["IdMarca"];
                    articulo.marca.Descripcion = (string)Conexion.Lector["Descripcion"];
                    articulo.categoria = new Categoria();
                    articulo.categoria.Id = (Int32)Conexion.Lector["IdCategoria"];
                    articulo.categoria.Descripcion = (string)Conexion.Lector["Descripcion"];
                    articulo.ImagenUrl = (string)Conexion.Lector["ImagenUrl"];
                    articulo.Precio = (Decimal)Conexion.Lector["Precio"];
                    listaArticulos.Add(articulo);
                }
                
                return listaArticulos;
            }
            catch (Exception excepcion)
            {
                throw excepcion;
            }
            finally
            {
                Conexion.cerrar();
            }

        }

        public void crear(Articulo articulo)
        {
            AccesoDatos conexion = new AccesoDatos();
            try
            {
                string consulta = "INSERT INTO[CATALOGO_DB].[dbo].[ARTICULOS]([Codigo], [Nombre], [Descripcion], [IdMarca], [IdCategoria], [ImagenUrl], [Precio]) VALUES(@codigo, @nombre, @descripcion, @idmarca, @idcategoria, @imagenurl, @precio);";
                conexion.setearParametro("@codigo", articulo.Codigo);
                conexion.setearParametro("@nombre", articulo.Nombre);
                conexion.setearParametro("@descripcion", articulo.Descripcion);
                conexion.setearParametro("@idmarca", articulo.marca.Id);
                conexion.setearParametro("@idcategoria", articulo.categoria.Id);
                conexion.setearParametro("@imagenurl", articulo.ImagenUrl);
                conexion.setearParametro("@precio", articulo.Precio);
                conexion.conectar();
                conexion.setearConsulta(consulta);
                conexion.ejecutarAccion();
            }
            catch (Exception excepcion)
            {
                throw excepcion;
            }
            finally
            {
                conexion.cerrar();
            }
        }
    }
}
