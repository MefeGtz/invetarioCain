using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using capanegocio;
using System.Threading.Tasks;

namespace capapresentacion
{
    public partial class Solicitudes : Form
    {
        private DataTable dtproductosol;
        private bool IsNuevo = false;
        private bool IsEditar = false;
        private int totalPagado = 0;

        private void Limpiar()
        {
            //this.txtid.Text = string.Empty;
            this.txtnombresoli.Text = string.Empty;
            //this.txtproveedor.Text = string.Empty;
            this.textboxprod.Text = string.Empty;
            this.textboxdescri.Text = string.Empty;
            this.textBoxcantr.Text = string.Empty;
            this.textboxpendiente.Text = string.Empty;
            //this.textBoxfech.Text = string.Empty;
            //this.textBoxoc.Text = string.Empty;
            this.textBoxpunitario.Text = string.Empty;
            this.textBoxobser.Text = string.Empty;

        }
        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema CAINE", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema CAINE ", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private string idorden;
        private string proveedor;
        private string ordenc;
        private string idproductooc;
        private string cantidad_inicial;
        private string pendiente_sol;

        //Habilitar los controles del formulario
        private void Habilitar(bool valor)
        {
            this.textBoxcantr.ReadOnly = !valor;
            this.txtnombresoli.ReadOnly = !valor;
            this.textBoxobser.ReadOnly = !valor;
            this.txtnombresoli.Focus();

        }

        private void siempreinactivos()
        {
            this.textboxpendiente.ReadOnly = true;
            this.textBoxidsol.ReadOnly = true;
            this.txtproveedor.ReadOnly = true;
            this.textboxprod.ReadOnly = true;
            this.textboxdescri.ReadOnly = true;
            this.textBoxpunitario.ReadOnly = true;
            this.textBoxoc.ReadOnly = true;
        }

        //Habilitar los botones
        private void Botones()
        {
            if (this.IsNuevo || this.IsEditar) //Alt + 124
            {
                this.Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;
                this.btnAgregar.Enabled = true;
                this.btnQuitar.Enabled = true;
            }
            else
            {
                this.Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
                this.btnCancelar.Enabled = false;
                this.btnAgregar.Enabled = false;
                this.btnQuitar.Enabled = false;
            }
        }

        //Método para ocultar columnas
        private void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;
            this.dataListado.Columns[2].Visible = false;

        }

        private static Solicitudes _Instancia;
        public static Solicitudes GetInstancia()
        {
            if (_Instancia == null)
            {
                _Instancia = new Solicitudes();
            }
            return _Instancia;
        }
        public void setorden(string idorden, string proveedor,string ordenc)
        {
            this.idorden = idorden;
            this.proveedor = proveedor;
            this.ordenc = ordenc;
            textBoxoc.Text = ordenc;
            txtproveedor.Text = proveedor;
            this.labelidorden.Text = idorden;
            this.labelorden.Text = ordenc;
       
        }
        
        public void setproducto(string idproductooc, string producto, string descripcion, DateTime fvencimiento, string cant, string punitario, string pendiente)
        {

            this.textboxprod.Text = producto;
            this.textboxdescri.Text = descripcion;
            this.textBoxpunitario.Text = punitario;
            this.dtFecha_Vencimiento.Text =Convert.ToString(fvencimiento);
            this.idproductooc = idproductooc;
            this.cantidad_inicial = cant;
            this.pendiente_sol = pendiente;
            this.textboxpendiente.Text = pendiente;
        }

        //Método Mostrar
        private void Mostrar()
        {
            this.dataListado.DataSource = Nsolicitud.Mostrar(labelidorden.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Solicitudes: " + Convert.ToString(dataListado.Rows.Count);
        }

        private void limpiarDetalle()
        {
            this.textboxprod.Text = string.Empty;
            this.textBoxcantr.Text = string.Empty;
            this.textboxdescri.Text = string.Empty;
            this.textBoxobser.Text = string.Empty;
            this.textBoxpunitario.Text = string.Empty;
            this.textboxpendiente.Text = string.Empty;
        }

        private void MostrarDetalle()
        {
            //ese id es de la orden de compra
            this.dataListadoproductosol.DataSource = Nsolicitud.MostrarDetalle(this.textBoxidsol.Text);
            this.dataListadoproductosol.Columns[0].Visible = false;

        }

        private void crearTabla()
        {
            this.dtproductosol = new DataTable("DetalleProductosol");
            this.dtproductosol.Columns.Add("idsolicitud", System.Type.GetType("System.Int32"));
            this.dtproductosol.Columns.Add("idproductooc", System.Type.GetType("System.Int32"));
            this.dtproductosol.Columns.Add("fvencimiento", System.Type.GetType("System.DateTime"));
            this.dtproductosol.Columns.Add("Producto", System.Type.GetType("System.String"));
            this.dtproductosol.Columns.Add("Descripción", System.Type.GetType("System.String"));
            this.dtproductosol.Columns.Add("Cantidad_Retirar", System.Type.GetType("System.Int32"));
            this.dtproductosol.Columns.Add("P_unitario", System.Type.GetType("System.Int32"));
            this.dtproductosol.Columns.Add("Total", System.Type.GetType("System.Int32"));
            this.dtproductosol.Columns.Add("Observación", System.Type.GetType("System.String"));
            this.dtproductosol.Columns.Add("Cantidad_inicial", System.Type.GetType("System.Int32"));
            this.dtproductosol.Columns.Add("Pendiente_r", System.Type.GetType("System.Int32"));   
            //Relacionar nuestro DataGRidView con nuestro DataTable
            this.dataListadoproductosol.DataSource = this.dtproductosol;
            this.dataListadoproductosol.Columns["idsolicitud"].Visible = false;
            this.dataListadoproductosol.Columns["idproductooc"].Visible = false;
            this.dataListadoproductosol.Columns["fvencimiento"].Visible = false;
        }

        public Solicitudes()
        {
            InitializeComponent();
            this.ttmensaje.SetToolTip(this.textboxpendiente, "Cantidad Pendiente a solicitar");
        }

        private void Solicitudes_Load(object sender, EventArgs e)
        {
            siempreinactivos();
            if (idorden == null)
            {
                this.Habilitar(false);
                this.Botones();
            }
            else {
                this.Mostrar();
                this.Habilitar(false);
                this.Botones();

            }
            //this.crearTabla();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
            this.txtnombresoli.Focus();
            this.crearTabla();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                //this.txtid.Text == string.Empty 
                string rpta = "";
                if (txtnombresoli.Text==string.Empty|| this.txtproveedor.Text == string.Empty || this.textBoxoc.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    erroricono.SetError(textBoxoc, "Ingrese un Valor");
                    erroricono.SetError(txtnombresoli, "Ingrese un Valor");
                }
                else
                {
                    int detallessol;
                    detallessol = dataListadoproductosol.Rows.Count;
                    if (detallessol <= 0) { MensajeError("No hay productos para la solicitud"); }
                    else
                    {
                        if (this.IsNuevo)
                        {
                            rpta = Nsolicitud.Insertar(Convert.ToInt32(this.idorden), this.txtnombresoli.Text, dtFechao.Value, dtproductosol);
                        }
                        else
                        {
                          
                            rpta = Nsolicitud.Editar(Convert.ToInt32(this.textBoxidsol.Text), Convert.ToInt32(this.idorden), this.txtnombresoli.Text, dtFechao.Value);
                        }
                    }
                    if (rpta.Equals("OK"))
                    {
                        if (this.IsNuevo)
                        {
                            this.MensajeOk("Se Insertó de forma correcta La Solicitud ");
                        }
                        else
                        {
                            this.MensajeOk("Se Actualizó de forma correcta La solicitud");
                        }
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }
                    this.IsNuevo = false;
                    this.IsEditar = false;
                    this.Botones();
                    this.Limpiar();
                    this.Mostrar();
                   tabControl1.SelectTab(0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                this.textBoxidsol.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idsolicitudp"].Value);
                //this.txtid.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idorden"].Value);
                this.txtnombresoli.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Solicitud"].Value);
                this.dtFechao.Value = Convert.ToDateTime(this.dataListado.CurrentRow.Cells["Fecha"].Value);
                //this.textBoxoc.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["ordencompra"].Value);
                this.MostrarDetalle();
                this.tabControl1.SelectedIndex = 1;
            }
            catch {
                MessageBox.Show("Error");
            }

            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(false);
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {
                this.dataListado.Columns[0].Visible = true;
            }
            else
            {
                this.dataListado.Columns[0].Visible = false;
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.textboxprod.Text == string.Empty || this.textboxdescri.Text == string.Empty
                    || this.textBoxpunitario.Text == string.Empty || this.textBoxcantr.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    erroricono.SetError(textboxprod, "Ingrese un Valor");
                    erroricono.SetError(textBoxpunitario, "Ingrese un Valor");
                    erroricono.SetError(textboxdescri, "Ingrese un Valor");
                    erroricono.SetError(textBoxcantr, "Ingrese un Valor");
                }
                else
                {
                    bool registrar = true;
                    foreach (DataRow row in dtproductosol.Rows)
                    {
                        if (Convert.ToString(row["producto"]) == this.textboxprod.Text)
                        {
                            registrar = false;
                            this.MensajeError("Ya se encuentra el producto en la orden de compra");
                        }
                    }
                    int cant_p;
                    cant_p = Convert.ToInt32(pendiente_sol) - Convert.ToInt32(textBoxcantr.Text);
                    if (registrar)
                    { 
                        if( cant_p>=0){
                        int subTotal = 0;
                        subTotal = Convert.ToInt32(this.textBoxcantr.Text) * Convert.ToInt32(this.textBoxpunitario.Text);
                        totalPagado = totalPagado + subTotal;
                        this.lblTotal_Pagado.Text = totalPagado.ToString("#0.00#");
                        //Agregar ese detalle al datalistadoDetalle
                        DataRow row = this.dtproductosol.NewRow();
                        //  row["idproductooc"] = Convert.ToInt32(this.txtIdarticulo.Text);
                        row["idsolicitud"] = 0;
                        row["idproductooc"] = this.idproductooc;
                        row["fvencimiento"] = dtFecha_Vencimiento.Value;
                        row["Producto"] = this.textboxprod.Text;
                        row["Descripción"] = textboxdescri.Text;
                        row["Cantidad_Retirar"] = Convert.ToInt32(this.textBoxcantr.Text);
                        row["P_unitario"] = Convert.ToInt32(this.textBoxpunitario.Text);
                        row["Total"] = subTotal;
                        row["Observación"] = textBoxobser.Text;
                        row["Cantidad_inicial"] = Convert.ToInt32(this.cantidad_inicial);
                        row["Pendiente_r"] = cant_p;
                        this.dtproductosol.Rows.Add(row);
                        this.limpiarDetalle();
                        }
                        else
                        {
                            MessageBox.Show("la cantidad a pedir no es correcta");
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                int indiceFila = this.dataListadoproductosol.CurrentCell.RowIndex;
                DataRow row = this.dtproductosol.Rows[indiceFila];
                //Disminuir el totalPAgado
                totalPagado = totalPagado - (Convert.ToInt32(row["total"].ToString()));
                this.lblTotal_Pagado.Text = totalPagado.ToString("#0.00#");
                //Removemos la fila
                this.dtproductosol.Rows.Remove(row);
            }
            catch (Exception ex)
            {
                MensajeError("No hay fila para remover");
            }
        }

        //para buscar los articulos de la orden de compra
        private void btnBuscarArticulo_Click(object sender, EventArgs e)
        {
            if (ordenc == null) {
                MensajeError("Debe estar seleccionada una Orden de compra");
            }
            else
            {
                vistaproductooc n = new vistaproductooc();
                Class1.ordenc = ordenc;
                Class1.idordenc = idorden;
                n.ShowDialog();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                imprimirsolicitud imp = new imprimirsolicitud();
                imp.Texto = Convert.ToString(this.dataListado.CurrentRow.Cells["idsolicitudp"].Value);
                imp.ShowDialog();
            }
            catch {
                this.MensajeError("Seleccione una solicitud");
            
            }
        }

        private void buttonbuscarorden_Click(object sender, EventArgs e)
        {
            Limpiar();
            limpiarDetalle();
            crearTabla();
            
            vistaordenc buscarorden = new vistaordenc();
            buscarorden.ShowDialog();
            if (idorden == null)
            {
                
            }
            else { Mostrar(); }
        }

        private void Solicitudes_Activated(object sender, EventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            //esta variable y ocndicional es para revisar que haya contenido en el datagriev y ver si esta seleccinado el chekbox eliminar
            int solicitudes;
            solicitudes = dataListado.Rows.Count;
            if (solicitudes <= 0 || (chkEliminar.Checked == false)) { MensajeError("No hay Solicitud para eliminar"); }
            else
            {
                try
                {
                    DialogResult Opcion;
                    Opcion = MessageBox.Show("Realmente ¿Desea Eliminar la solicitud y todos sus detalles?", "Sistema CAINE", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    if (Opcion == DialogResult.OK)
                    {
                        string Codigo;
                        string Rpta = "";

                        foreach (DataGridViewRow row in dataListado.Rows)
                        {
                            if (Convert.ToBoolean(row.Cells[0].Value))
                            {
                                Codigo = Convert.ToString(row.Cells[1].Value);
                                Rpta = Nsolicitud.Eliminar(Convert.ToInt32(Codigo));

                                if (Rpta.Equals("OK"))
                                {
                                    this.MensajeOk("Se Eliminó Correctamente el registro");
                                }
                                else
                                {
                                    this.MensajeError(Rpta);
                                }
                            }
                        }
                        this.Mostrar();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
        }

        private void Solicitudes_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Instancia = null;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            this.btnAgregar.Enabled = false;
            this.btnQuitar.Enabled = false;
            if (!this.textBoxidsol.Text.Equals(""))
            {
                this.IsEditar = true;
                this.Botones();
                this.Habilitar(true);
                btnAgregar.Enabled = false;
                btnQuitar.Enabled = false;
                MessageBox.Show("Solo se puede modificar el nombre de la solicitud y la fecha de la solicitud");
            }
            else
            {
                this.MensajeError("Debe de seleccionar primero una solicitud para Modificar");
            }
        }
    }
}
