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
    public partial class MDIP : Form
    {
        private int childFormNumber = 0;
        //inician los componentes
        public MDIP()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Ventana " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Archivos de texto (*.txt)|*.txt|Todos los archivos (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        //private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        //}

        //private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        //}

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void ordenDeCompraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formordenc frm = formordenc.GetInstancia();
            frm.MdiParent = this;
            frm.Show();
        }

        private void productosOrdenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Productosoc n = new Productosoc();
            n.MdiParent = this;
            n.Show();

        }

        private void categoriasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            categorias n = new categorias();
            n.MdiParent = this;
            n.Show();
        }

        private void solicitudesParcialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Solicitudes v = Solicitudes.GetInstancia(); 
            v.MdiParent = this;
            v.Show();
        }

        private void bodegaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pbodega pb = new Pbodega();
            pb.MdiParent = this;
            pb.Show();

        }

        private void registrarSalidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Kardex k = Kardex.GetInstancia();
            k.MdiParent = this;
            k.Show();
        }

        private void productosAVencerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            productosvencen vencen = new productosvencen();
            vencen.MdiParent = this;
            vencen.Show();
        }

        private void cantidadDeProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            productospcant gastan = new productospcant();
            gastan.MdiParent = this;
            gastan.Show();
        }

        private void productosASolicitarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Productos_a_solicitar n = new Productos_a_solicitar();
            n.MdiParent = this;
            n.Show();
        }
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema CAINE ", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void Mostrar()
        {
            DateTime hoy = DateTime.Today;
            DateTime fin = hoy.AddDays(7);
           this.dtFecha1.Value = hoy;
           this.dtFecha2.Value = fin;
            this.dataListado.DataSource = Nbodega.buscarfechas(this.dtFecha1.Value.ToString("dd/MM/yyyy"),
                this.dtFecha2.Value.ToString("dd/MM/yyyy"));
            this.dataListado.Columns["idproductosb"].Visible = false;

            this.datalistado2c.DataSource = Nbodega.Mostrarproductoagastarse();
            this.datalistado2c.Columns["idproductosb"].Visible = false;
           // this.OcultarColumnas();
            //lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
        }
        private void MDIP_Load(object sender, EventArgs e)
        {
            Mostrar();
            int elemento;
            elemento = dataListado.Rows.Count;
            string notificacion ="",notificacion2="";
            int prodppocos;
            prodppocos = datalistado2c.Rows.Count;

            if ((elemento > 0) || (prodppocos>0))
            {
                if (elemento >= 1) 
                {
                   if(elemento==1){ notificacion="Hay " + elemento + " " + "Producto que vence entre 7 dias"; }
                   else
                   {
                       notificacion = "Hay " + elemento + " " + "Productos que vencen entre 7 dias";
                   }
                }

                if (prodppocos >= 1)
                {
                    if (prodppocos == 1) { notificacion2 = "Hay " + prodppocos + " " + "Producto que esta apunto de gastarse"; }
                    else
                    {
                        notificacion2 = "Hay " + prodppocos + " " + "Productos que estan apunto de gastarse";
                    }
                }
                MensajeOk(notificacion + "\n" + notificacion2);
                notifyIcon1.Icon = SystemIcons.Information;
                notifyIcon1.BalloonTipText = notificacion+ "\n"+notificacion2;
                notifyIcon1.ShowBalloonTip(1000);
           } 




        }
    }
}
