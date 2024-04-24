using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using capanegocio;
namespace capapresentacion
{
    public partial class Productos_a_solicitar : Form
    {
        public Productos_a_solicitar()
        {
            InitializeComponent();
        }
       private void  mostrar(){
        this.dataListadoproductooc.DataSource = Norden.Mostrarp0();
       }

        private void Productos_a_solicitar_Load(object sender, EventArgs e)
        {
            mostrar();
        }
    }
}
