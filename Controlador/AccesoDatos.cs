﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Controlador
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;

        public AccesoDatos()
        {
            conexion = new SqlConnection("Data Source=.; Initial Catalog=CATALOGO_DB;Integrated Security=True;");
            comando = new SqlCommand();
        }

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public void conectar()
        {
            try
            {
                conexion.Open();
            }
            catch (Exception excepcion)
            {
                throw excepcion;
            }
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }

        public void ejecutarLectura()
        {
            comando.Connection = conexion;

            try
            {
                lector = comando.ExecuteReader();
            }
            catch (Exception excepcion)
            {
                throw excepcion;
            }
        }

        public void cerrar()
        {
            if (lector != null)
            {
                lector.Close();
            }
            conexion.Close();
        }
    }
}
