using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace capapresentacion
{
    public partial class Productosoc : Form
    {
        public Productosoc()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            registrproductoc P = new registrproductoc();
            if (dataGridView1.SelectedRows.Count > 0) 
            {}
            // Program.Evento = 1;
            else
                // Program.Evento = 0;
                dataGridView1.ClearSelection();
            P.Show();
        }
    }
}
