using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;

namespace capadato
{
   public class Dordenc
    {
        private int _idorden;

        public int Idorden
        {
            get { return _idorden; }
            set { _idorden = value; }
        }
        private string _provedor;

        public string Provedor
        {
            get { return _provedor; }
            set { _provedor = value; }
        }
        private string _Direccion;

        public string Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }
        private string _RTN;

        public string RTN
        {
            get { return _RTN; }
            set { _RTN = value; }
        }
        private DateTime _fecha;

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
        }
        private string _telefono;

        public string Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }
        private string _nrepresentante;

        public string Nrepresentante
        {
            get { return _nrepresentante; }
            set { _nrepresentante = value; }
        }
        private string _ordencompra;

        public string Ordencompra
        {
            get { return _ordencompra; }
            set { _ordencompra = value; }
        }
        private string _estado;

        public string Estado
        {
            get { return _estado; }
            set { _estado = value; }
        }

        public Dordenc() { 
        
        }

        //con parametros
        public Dordenc(int Idorden, string Proveedor, string direccion, string rtn, DateTime fecha, string Telefono, string Nrepresentante, string Ordencompra, string Estado)
        {
            this.Idorden = Idorden;
            this.Provedor = Proveedor;
            this.Direccion = direccion;
            this.RTN = rtn;
            this.Fecha = fecha;
            this.Telefono = Telefono;
            this.Nrepresentante = Nrepresentante;
            this.Ordencompra = Ordencompra;
            this.Estado = Estado;
        }


        public string Insertar(Dordenc Dordenc, List<dproductooc> Productooc)
        {
            //este metodo nos recibe los datos del objeto dorden y una lista que trae datos de los productos de orden de compra

            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Código
                SqlCon.ConnectionString = conexion.Cn;
                SqlCon.Open();
                //transaccion
                SqlTransaction SqlTra = SqlCon.BeginTransaction();
                //Establecer el Comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "insertaroc";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdorden = new SqlParameter();
                ParIdorden.ParameterName = "@idorden";
                ParIdorden.SqlDbType = SqlDbType.Int;
                ParIdorden.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParIdorden);

                SqlParameter Parproveedor = new SqlParameter();
                Parproveedor.ParameterName = "@proveedor";
                Parproveedor.SqlDbType = SqlDbType.VarChar;
                Parproveedor.Size = 50;
                Parproveedor.Value = Dordenc.Provedor;
                SqlCmd.Parameters.Add(Parproveedor);

                SqlParameter ParDireccion = new SqlParameter();
                ParDireccion.ParameterName = "@direccion";
                ParDireccion.SqlDbType = SqlDbType.VarChar;
                ParDireccion.Size = 256;
                ParDireccion.Value = Dordenc.Direccion;
                SqlCmd.Parameters.Add(ParDireccion);
                
                SqlParameter ParRTN = new SqlParameter();
                ParRTN.ParameterName = "@RTN";
                ParRTN.SqlDbType = SqlDbType.VarChar;
                ParRTN.Size = 18;
                ParRTN.Value = Dordenc.RTN;
                SqlCmd.Parameters.Add(ParRTN);

                SqlParameter Parfecha = new SqlParameter();
                Parfecha.ParameterName = "@fecha";
                Parfecha.SqlDbType = SqlDbType.Date;
                Parfecha.Size = 256;
                Parfecha.Value = Dordenc.Fecha;
                SqlCmd.Parameters.Add(Parfecha);

                SqlParameter Partelefono = new SqlParameter();
                Partelefono.ParameterName = "@telefono";
                Partelefono.SqlDbType = SqlDbType.VarChar;
                Partelefono.Size = 256;
                Partelefono.Value = Dordenc.Telefono;
                SqlCmd.Parameters.Add(Partelefono);

                SqlParameter Parnrepresentante = new SqlParameter();
                Parnrepresentante.ParameterName = "@nrepresentante";
                Parnrepresentante.SqlDbType = SqlDbType.VarChar;
                Parnrepresentante.Size = 256;
                Parnrepresentante.Value = Dordenc.Nrepresentante;
                SqlCmd.Parameters.Add(Parnrepresentante);

                SqlParameter Parordencompra = new SqlParameter();
                Parordencompra.ParameterName = "@ordencompra";
                Parordencompra.SqlDbType = SqlDbType.VarChar;
                Parordencompra.Size = 100;
                Parordencompra.Value = Dordenc.Ordencompra;
                SqlCmd.Parameters.Add(Parordencompra);

                SqlParameter Parestado = new SqlParameter();
                Parestado.ParameterName = "@estado";
                Parestado.SqlDbType = SqlDbType.VarChar;
                Parestado.Size = 100;
                Parestado.Value = Dordenc.Estado;
                SqlCmd.Parameters.Add(Parestado);

                //Ejecutamos nuestro comando

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";
                // si se guardan en orden se procede a guardar los datos para los productos pertenceientes a la orden

                if (rpta.Equals("OK"))
                {
                    //Obtener el código del ingreso generado
                    this.Idorden = Convert.ToInt32(SqlCmd.Parameters["@idorden"].Value);
                    // este foreach es para insertar datos en los prodctos de orden de compra
                    foreach (dproductooc det in Productooc)
                    {
                        det.Idorden = this.Idorden;
                        //Llamar al método insertar de la clase DDetalle_Ingreso
                        rpta = det.Insertar(det, ref SqlCon, ref SqlTra);
                        if (!rpta.Equals("OK"))
                        {
                            break;
                            ////si la respuesta es distinta de ok entonces el ciclo termina y se cancela la operacion
                        }
                    }

                }
                if (rpta.Equals("OK"))
                {
                    SqlTra.Commit();
                }
                else
                {
                    SqlTra.Rollback();
                }
                
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return rpta;

        }

        //Método Editar
        public string Editar(Dordenc Dordenc)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Código
                SqlCon.ConnectionString = conexion.Cn;
                SqlCon.Open();
                //Establecer el Comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "editaroc";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdorden = new SqlParameter();
                ParIdorden.ParameterName = "@idorden";
                ParIdorden.SqlDbType = SqlDbType.Int;
                ParIdorden.Value=Dordenc.Idorden;
                SqlCmd.Parameters.Add(ParIdorden);

                SqlParameter Parproveedor = new SqlParameter();
                Parproveedor.ParameterName = "@proveedor";
                Parproveedor.SqlDbType = SqlDbType.VarChar;
                Parproveedor.Size = 50;
                Parproveedor.Value = Dordenc.Provedor;
                SqlCmd.Parameters.Add(Parproveedor);

                SqlParameter ParDireccion = new SqlParameter();
                ParDireccion.ParameterName = "@direccion";
                ParDireccion.SqlDbType = SqlDbType.VarChar;
                ParDireccion.Size = 256;
                ParDireccion.Value = Dordenc.Direccion;
                SqlCmd.Parameters.Add(ParDireccion);

                SqlParameter ParRTN = new SqlParameter();
                ParRTN.ParameterName = "@RTN";
                ParRTN.SqlDbType = SqlDbType.VarChar;
                ParRTN.Size = 18;
                ParRTN.Value = Dordenc.RTN;
                SqlCmd.Parameters.Add(ParRTN);

                SqlParameter Parfecha = new SqlParameter();
                Parfecha.ParameterName = "@fecha";
                Parfecha.SqlDbType = SqlDbType.Date;
                Parfecha.Size = 256;
                Parfecha.Value = Dordenc.Fecha;
                SqlCmd.Parameters.Add(Parfecha);

                SqlParameter Partelefono = new SqlParameter();
                Partelefono.ParameterName = "@telefono";
                Partelefono.SqlDbType = SqlDbType.VarChar;
                Partelefono.Size = 256;
                Partelefono.Value = Dordenc.Telefono;
                SqlCmd.Parameters.Add(Partelefono);

                SqlParameter Parnrepresentante = new SqlParameter();
                Parnrepresentante.ParameterName = "@nrepresentante";
                Parnrepresentante.SqlDbType = SqlDbType.VarChar;
                Parnrepresentante.Size = 256;
                Parnrepresentante.Value = Dordenc.Nrepresentante;
                SqlCmd.Parameters.Add(Parnrepresentante);

                SqlParameter Parordencompra = new SqlParameter();
                Parordencompra.ParameterName = "@ordencompra";
                Parordencompra.SqlDbType = SqlDbType.VarChar;
                Parordencompra.Size = 256;
                Parordencompra.Value = Dordenc.Ordencompra;
                SqlCmd.Parameters.Add(Parordencompra);

                SqlParameter Parestado = new SqlParameter();
                Parestado.ParameterName = "@estado";
                Parestado.SqlDbType = SqlDbType.VarChar;
                Parestado.Size = 100;
                Parestado.Value = Dordenc.Estado;
                SqlCmd.Parameters.Add(Parestado);

              
                //Ejecutamos nuestro comando

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Actualizo el Registro";
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return rpta;
            
        }

        //Método Eliminar
        public string Eliminar(Dordenc Dordenc)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Código
                SqlCon.ConnectionString = conexion.Cn;
                SqlCon.Open();
                //Establecer el Comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "eliminarocomp";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdorden = new SqlParameter();
                ParIdorden.ParameterName = "@idorden";
                ParIdorden.SqlDbType = SqlDbType.Int;
                ParIdorden.Value = Dordenc.Idorden;
                SqlCmd.Parameters.Add(ParIdorden);
                //Ejecutamos nuestro comando

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Elimino el Registro";
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return rpta;
        }

        public string Eliminarsolicitudoc(Dordenc Dordenc)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Código
                SqlCon.ConnectionString = conexion.Cn;
                SqlCon.Open();
                //Establecer el Comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "speliminar_solicituddesdeoc";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdorden = new SqlParameter();
                ParIdorden.ParameterName = "@idorden";
                ParIdorden.SqlDbType = SqlDbType.Int;
                ParIdorden.Value = Dordenc.Idorden;
                SqlCmd.Parameters.Add(ParIdorden);
                //Ejecutamos nuestro comando

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Elimino el Registro";
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return rpta;
        }



        public string Anular(Dordenc Dordenc)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Código
                SqlCon.ConnectionString = conexion.Cn;
                SqlCon.Open();
                //Establecer el Comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spestado_orden";
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlParameter ParIdorden = new SqlParameter();
                ParIdorden.ParameterName = "@idorden";
                ParIdorden.SqlDbType = SqlDbType.Int;
                ParIdorden.Value = Dordenc.Idorden;
                SqlCmd.Parameters.Add(ParIdorden);
                //Ejecutamos nuestro comando
                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se liquido la orden";
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
            return rpta;
        }


        //para mostrar

        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("ordencompra");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "mostraroc";
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);

            }
            catch (Exception)
            {
                DtResultado = null;
            }
            return DtResultado;
        }
        public DataTable Mostrarordensinliquidar()
        {
            DataTable DtResultado = new DataTable("ordencompra");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "mostrartodaoc";
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);

            }
            catch (Exception)
            {
                DtResultado = null;
            }
            return DtResultado;
        }

        public DataTable Mostrarp0()
        {
            DataTable DtResultado = new DataTable("ordencompra");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spmostrar_productoocs>0";
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);

            }
            catch (Exception)
            {
                DtResultado = null;
            }
            return DtResultado;
        }


        public DataTable MostrarDetalle(String TextoBuscar)
        {
            DataTable DtResultado = new DataTable("productooc");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spmostrar_productooc2";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParTextoBuscar = new SqlParameter();
                ParTextoBuscar.ParameterName = "@textobuscar";
                ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar.Size = 50;
                ParTextoBuscar.Value = TextoBuscar;
                SqlCmd.Parameters.Add(ParTextoBuscar);

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);

            }
            catch (Exception ex)
            {
                DtResultado = null;
            }
            return DtResultado;
        }

        public DataTable MostrarDetalle1(String TextoBuscar)
        {
            DataTable DtResultado = new DataTable("productooc");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spmostrar_vistaproductooc";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParTextoBuscar = new SqlParameter();
                ParTextoBuscar.ParameterName = "@textobuscar";
                ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar.Size = 50;
                ParTextoBuscar.Value = TextoBuscar;
                SqlCmd.Parameters.Add(ParTextoBuscar);

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);

            }
            catch (Exception ex)
            {
                DtResultado = null;
            }
            return DtResultado;
        }
        public DataTable buscarproductonombre(String TextoBuscar, string producto)
        {
            DataTable DtResultado = new DataTable("productooc");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spbuscar_productoocnombre";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParTextoBuscar = new SqlParameter();
                ParTextoBuscar.ParameterName = "@textobuscar";
                ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar.Size = 50;
                ParTextoBuscar.Value = TextoBuscar;
                SqlCmd.Parameters.Add(ParTextoBuscar);

                SqlParameter ParTextoBuscar2 = new SqlParameter();
                ParTextoBuscar2.ParameterName = "@producto";
                ParTextoBuscar2.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar2.Size = 50;
                ParTextoBuscar2.Value = producto;
                SqlCmd.Parameters.Add(ParTextoBuscar2);

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);

            }
            catch (Exception ex)
            {
                DtResultado = null;
            }
            return DtResultado;
        }





        public DataTable Buscarorden(string TextoBuscar, string textobuscar2)
        {
            DataTable DtResultado = new DataTable("ordencompra");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "buscaroc";
                SqlCmd.CommandType = CommandType.StoredProcedure;
              
                SqlParameter ParTextoBuscar = new SqlParameter();
                ParTextoBuscar.ParameterName = "@textobuscar";
                ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar.Size = 50;
                ParTextoBuscar.Value = TextoBuscar;
                SqlCmd.Parameters.Add(ParTextoBuscar);

                SqlParameter ParTextoBuscar2 = new SqlParameter();
                ParTextoBuscar2.ParameterName = "@textobuscar2";
                ParTextoBuscar2.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar2.Size = 50;
                ParTextoBuscar2.Value = textobuscar2;
                SqlCmd.Parameters.Add(ParTextoBuscar2);

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);
            }
            catch (Exception)
            {
                DtResultado = null;
            }
            return DtResultado;
        }


    }

}
