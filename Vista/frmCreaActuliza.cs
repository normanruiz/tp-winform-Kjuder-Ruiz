﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo;

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

        }
    }
}