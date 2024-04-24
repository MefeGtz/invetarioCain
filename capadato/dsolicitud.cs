using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;
namespace capadato
{
    public class dsolicitud
    {

      private int _idsolicitudp;

      private int total;

      public int Total
      {
          get { return total; }
          set { total = value; }
      }

        public int Idsolicitudp
        {
            get { return _idsolicitudp; }
            set { _idsolicitudp = value; }
        }
        private int _idorden;

        public int Idorden
        {
            get { return _idorden; }
            set { _idorden = value; }
        }
        private string _nombresol;

        public string Nombresol
        {
            get { return _nombresol; }
            set { _nombresol = value; }
        }
        private DateTime fecha;

        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
        
        public dsolicitud() { }

        public int totalpend = 1;

       public dsolicitud(int idsolicitudp,int idorden, string nombresol, DateTime fecha) {
            this.Idsolicitudp = idsolicitudp;
            this.Idorden = idorden;
            this.Nombresol = nombresol;
            this.Fecha = fecha;
        }

       //Métodos
       public string Disminuircantp(int idproductooc, int cantidad)
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
               SqlCmd.CommandText = "spdisminuir_pendientesol";
               SqlCmd.CommandType = CommandType.StoredProcedure;

               SqlParameter ParIdproductooc = new SqlParameter();
               ParIdproductooc.ParameterName = "@idproductooc";
               ParIdproductooc.SqlDbType = SqlDbType.Int;
               ParIdproductooc.Value = idproductooc;
               SqlCmd.Parameters.Add(ParIdproductooc);

               SqlParameter ParCantidad = new SqlParameter();
               ParCantidad.ParameterName = "@cantidad";
               ParCantidad.SqlDbType = SqlDbType.Int;
               ParCantidad.Value = cantidad;
               SqlCmd.Parameters.Add(ParCantidad);
               //Ejecutamos nuestro comando
               rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Actualizó el stock";
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
        
        public string Insertar(dsolicitud dsolicitud, List<dprodsol> Productossol)
        {
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
                SqlCmd.CommandText = "spinsertarsol";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdsolicitudp = new SqlParameter();
                ParIdsolicitudp.ParameterName = "@idsolicitudp";
                ParIdsolicitudp.SqlDbType = SqlDbType.Int;
                ParIdsolicitudp.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParIdsolicitudp);

                SqlParameter Paridorden = new SqlParameter();
                Paridorden.ParameterName = "@idorden";
                Paridorden.SqlDbType = SqlDbType.Int;
                Paridorden.Value = dsolicitud.Idorden;
                SqlCmd.Parameters.Add(Paridorden);
               
                SqlParameter Parnombresol = new SqlParameter();
                Parnombresol.ParameterName = "@nombresol";
                Parnombresol.SqlDbType = SqlDbType.VarChar;
                Parnombresol.Size = 256;
                Parnombresol.Value = dsolicitud.Nombresol;
                SqlCmd.Parameters.Add(Parnombresol);

                SqlParameter Parfecha = new SqlParameter();
                Parfecha.ParameterName = "@fecha";
                Parfecha.SqlDbType = SqlDbType.Date;
                Parfecha.Size = 256;
                Parfecha.Value = dsolicitud.Fecha;
                SqlCmd.Parameters.Add(Parfecha);

                //Ejecutamos nuestro comando
                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";
                // si se guardan en orden se procede a guardar los datos para los productos pertenceientes a la orden

                if (rpta.Equals("OK"))
                {
                    //Obtener el código del ingreso generado para la solicitud
                    this.Idsolicitudp = Convert.ToInt32(SqlCmd.Parameters["@idsolicitudp"].Value);
                    foreach (dprodsol det in Productossol)
                    {
                        det.Idsolicitud = this.Idsolicitudp;
                        //Llamar al método insertar de la clase DDetalle_Ingreso
                        rpta = det.Insertar(det, ref SqlCon, ref SqlTra);
                        if (!rpta.Equals("OK"))
                        {
                            break;
                        }
                        else
                        {
                            //obtenemos el codigo del detalle de solicitud que se guardo, para guardar seguidamente el producto en bodega.se realizara en la otra clase mejor
                           // det.Iddetallesolicitud = Convert.ToInt32(SqlCmd.Parameters["@iddetallesolicitud"].Value);
                            rpta=det.Insertarpbodega(det, ref SqlCon, ref SqlTra);
                            if (!rpta.Equals("OK"))
                            {
                                break;
                            }
                            //Actualizamos el stock o la cantidad pendiente a retirar
                            rpta = Disminuircantp(det.Idproductooc, det.Cantidad_retirar);
                            if (!rpta.Equals("OK"))
                            {
                                break;
                            }
                          //  det.Iddetallesolicitud=Convert.ToInt32(SqlCmd.Parameters["@iddetallesolicitud"].Value);    
                        }
                    }
                    
                }
                if (rpta.Equals("OK"))
                {
                    SqlTra.Commit();

                    string rpta2 = cambiardeestado(dsolicitud.Idorden);
                    if (rpta2.Equals("OK"))
                    {
                        //si el resultado de nuestro parametro fue cero entonces que nos cambie el estado de la orden de compra
                        if (this.totalpend <= 0)
                        {
                            rpta2 = Anular(dsolicitud.Idorden);
                        }
                    }

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
        public string Editar(dsolicitud dsolicitud)
        {
            string rpta = "";
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Código
                SqlCon.ConnectionString = conexion.Cn;
                SqlCon.Open();
                //transaccion
               // SqlTransaction SqlTra = SqlCon.BeginTransaction();
                //Establecer el Comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
               // SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "speditar_solicitud";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdsolicitudp = new SqlParameter();
                ParIdsolicitudp.ParameterName = "@idsolicitudp";
                ParIdsolicitudp.SqlDbType = SqlDbType.Int;
                ParIdsolicitudp.Value = dsolicitud.Idsolicitudp;
                SqlCmd.Parameters.Add(ParIdsolicitudp);

                SqlParameter Paridorden = new SqlParameter();
                Paridorden.ParameterName = "@idorden";
                Paridorden.SqlDbType = SqlDbType.Int;
                Paridorden.Value = dsolicitud.Idorden;
                SqlCmd.Parameters.Add(Paridorden);

                SqlParameter Parnombresol = new SqlParameter();
                Parnombresol.ParameterName = "@nombresol";
                Parnombresol.SqlDbType = SqlDbType.VarChar;
                Parnombresol.Size = 256;
                Parnombresol.Value = dsolicitud.Nombresol;
                SqlCmd.Parameters.Add(Parnombresol);

                SqlParameter Parfecha = new SqlParameter();
                Parfecha.ParameterName = "@fecha";
                Parfecha.SqlDbType = SqlDbType.Date;
                Parfecha.Size = 256;
                Parfecha.Value = dsolicitud.Fecha;
                SqlCmd.Parameters.Add(Parfecha);
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

        public DataTable Mostrar(String TextoBuscar)
        {
            DataTable DtResultado = new DataTable("Solicitud");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spmostrar_solicitud";
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
            catch (Exception)
            {
                DtResultado = null;
            }
            return DtResultado;
        }

        public DataTable MostrarDetalle(String TextoBuscar)
        {
            DataTable DtResultado = new DataTable("productossol");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spmostrar_detallesol";
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

        public string cambiardeestado(int idord)
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
                SqlCmd.CommandText = "spcambiardeestado";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdorden = new SqlParameter();
                ParIdorden.ParameterName = "@textobuscar";
                ParIdorden.SqlDbType = SqlDbType.Int;
                ParIdorden.Value = idord;
                SqlCmd.Parameters.Add(ParIdorden);

                SqlParameter Partotal = new SqlParameter();
                Partotal.ParameterName = "@totals";
                Partotal.SqlDbType = SqlDbType.Int;
                Partotal.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(Partotal);
                //Ejecutamos nuestro comando
                SqlCmd.ExecuteNonQuery();
                this.totalpend = Convert.ToInt32(SqlCmd.Parameters["@totals"].Value.ToString());
                rpta = "OK";
            }
            catch (Exception ex)
            {
                rpta = "No";
            }
            finally
            {
                if (SqlCon.State == ConnectionState.Open) SqlCon.Close();
            }
           return rpta;
        }

        public string Eliminar(dsolicitud solicitud)
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
                SqlCmd.CommandText = "speliminar_solicitud";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdsolicitud = new SqlParameter();
                ParIdsolicitud.ParameterName = "@idsolicitud";
                ParIdsolicitud.SqlDbType = SqlDbType.Int;
                ParIdsolicitud.Value = solicitud.Idsolicitudp;
                SqlCmd.Parameters.Add(ParIdsolicitud);
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

        public string Anular(int idord)
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
                ParIdorden.Value = idord;
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


    }
}
