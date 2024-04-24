using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;

namespace capadato
{
   public class dprodsol

    {
       // esta capa tambien sera utilizada para guardar los datos a bodega;
        private int iddetallesolicitud;
        public int Iddetallesolicitud
        {
            get { return iddetallesolicitud; }
            set { iddetallesolicitud = value; }
        }
        private int idsolicitud;

        public int Idsolicitud
        {
            get { return idsolicitud; }
            set { idsolicitud = value; }
        }
        private int idproductooc;

        public int Idproductooc
        {
            get { return idproductooc; }
            set { idproductooc = value; }
        }
        private int punitario;

        public int Punitario
        {
            get { return punitario; }
            set { punitario = value; }
        }
        private int cantidad_retirar;

        public int Cantidad_retirar
        {
            get { return cantidad_retirar; }
            set { cantidad_retirar = value; }
        }
        private int pendiente_retirar;

        public int Pendiente_retirar
        {
            get { return pendiente_retirar; }
            set { pendiente_retirar = value; }
        }
        private int total;

        public int Total
        {
            get { return total; }
            set { total = value; }
        }
        private string observacion;

        public string Observacion
        {
            get { return observacion; }
            set { observacion = value; }
        }

        private int _idproducto;
        public int Idproducto
        {
            get { return _idproducto; }
            set { _idproducto = value; }
        }

        private string _producto;
        public string producto
        {
            get { return _producto; }
            set { _producto = value; }
        }

       private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private DateTime _fechadevencimiento;

        public DateTime fechadevencimiento
        {
            get { return _fechadevencimiento; }
            set { _fechadevencimiento = value; }
        }


        public dprodsol() { }

       public dprodsol(int iddetallesolicitud, int idsolicitud, int idproductooc, int punitario, int cant_retirar, int cant_pendiente, int total, string observacion,
           int idproductob, string productob, string descripcion, DateTime fvencimiento)
        {
            this.Iddetallesolicitud = iddetallesolicitud;
            this.Idsolicitud = idsolicitud;
            this.Idproductooc = idproductooc;
            this.Punitario = punitario;
            this.Cantidad_retirar = cant_retirar;
            this.Pendiente_retirar = cant_pendiente;
            this.Total = total;
            this.Observacion = observacion;
            this.fechadevencimiento = fvencimiento;
            this.producto = productob;
            this.Idproducto = idproductob;
            this.Descripcion = descripcion;
        }

        public string Insertar(dprodsol dprodsol, ref SqlConnection SqlCon, ref SqlTransaction SqlTra)
        {
            string rpta = "";
            //  SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Código
                //  SqlCon.ConnectionString = Conexion.Cn;
                //Establecer el Comando
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.Transaction = SqlTra;
                SqlCmd.CommandText = "spinsertar_detsoli";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIddetallesolicitud = new SqlParameter();
                ParIddetallesolicitud.ParameterName = "@iddetallesolicitud";
                ParIddetallesolicitud.SqlDbType = SqlDbType.Int;
                ParIddetallesolicitud.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParIddetallesolicitud);

                SqlParameter Paridsolicitud = new SqlParameter();
                Paridsolicitud.ParameterName = "@idsolicitud";
                Paridsolicitud.SqlDbType = SqlDbType.Int;
                Paridsolicitud.Value = dprodsol.Idsolicitud;
                SqlCmd.Parameters.Add(Paridsolicitud);

                SqlParameter ParIdproductooc = new SqlParameter();
                ParIdproductooc.ParameterName = "@idproductooc";
                ParIdproductooc.SqlDbType = SqlDbType.Int;
                ParIdproductooc.Value = dprodsol.Idproductooc;
                SqlCmd.Parameters.Add(ParIdproductooc);

                SqlParameter Parpunitario = new SqlParameter();
                Parpunitario.ParameterName = "@punitario";
                Parpunitario.SqlDbType = SqlDbType.Int;
                Parpunitario.Value = dprodsol.Punitario;
                SqlCmd.Parameters.Add(Parpunitario);

                SqlParameter Parcantidadr = new SqlParameter();
                Parcantidadr.ParameterName = "@cantidad_retirar";
                Parcantidadr.SqlDbType = SqlDbType.Int;
                Parcantidadr.Value = dprodsol.Cantidad_retirar;
                SqlCmd.Parameters.Add(Parcantidadr);

                SqlParameter Parcantidadp = new SqlParameter();
                Parcantidadp.ParameterName = "@pendiente_retirar";
                Parcantidadp.SqlDbType = SqlDbType.Int;
                Parcantidadp.Value = dprodsol.Pendiente_retirar;
                SqlCmd.Parameters.Add(Parcantidadp);

                SqlParameter Partotal = new SqlParameter();
                Partotal.ParameterName = "@total";
                Partotal.SqlDbType = SqlDbType.Int;
                Partotal.Value = dprodsol.Total;
                SqlCmd.Parameters.Add(Partotal);

                SqlParameter Parobservacion = new SqlParameter();
                Parobservacion.ParameterName = "@observacion";
                Parobservacion.SqlDbType = SqlDbType.VarChar;
                Parobservacion.Size = 50;
                Parobservacion.Value = dprodsol.Observacion;
                SqlCmd.Parameters.Add(Parobservacion);
                //Ejecutamos nuestro comando
                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";

                if (rpta.Equals("OK")){
                    this.Iddetallesolicitud = Convert.ToInt32(SqlCmd.Parameters["@iddetallesolicitud"].Value);
                }

            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }

    public string Insertarpbodega(dprodsol dprodsol, ref SqlConnection SqlCon, ref SqlTransaction SqlTra)
        {
            string rpta = "";
            //  SqlConnection SqlCon = new SqlConnection();
            try
            {
                //Código
                //  SqlCon.ConnectionString = Conexion.Cn;
                //Establecer el Comando
                    //obtenemos el codigo del detalle de solicitud que se guardo, para guardar seguidamente el producto en bodega.
                    SqlCommand SqlCmd = new SqlCommand();
                    SqlCmd.Connection = SqlCon;
                    SqlCmd.Transaction = SqlTra;
                    SqlCmd.CommandText = "spinsertar_productobodega";
                    SqlCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter ParIdproductosb = new SqlParameter();
                    ParIdproductosb.ParameterName = "@idproductosb";
                    ParIdproductosb.SqlDbType = SqlDbType.Int;
                    ParIdproductosb.Direction = ParameterDirection.Output;
                    SqlCmd.Parameters.Add(ParIdproductosb);

                    SqlParameter Pariddsolicitud = new SqlParameter();
                    Pariddsolicitud.ParameterName = "@iddetallesolicitud";
                    Pariddsolicitud.SqlDbType = SqlDbType.Int;
                    Pariddsolicitud.Value = dprodsol.Iddetallesolicitud;
                    SqlCmd.Parameters.Add(Pariddsolicitud);

                    SqlParameter Parproducto = new SqlParameter();
                    Parproducto.ParameterName = "@producto";
                    Parproducto.SqlDbType = SqlDbType.VarChar;
                    Parproducto.Size = 50;
                    Parproducto.Value = dprodsol.producto;
                    SqlCmd.Parameters.Add(Parproducto);

                    SqlParameter Parpunitariob = new SqlParameter();
                    Parpunitariob.ParameterName = "@punitario";
                    Parpunitariob.SqlDbType = SqlDbType.Int;
                    Parpunitariob.Value = dprodsol.Punitario;
                    SqlCmd.Parameters.Add(Parpunitariob);

                    SqlParameter Parcantidadi = new SqlParameter();
                    Parcantidadi.ParameterName = "@cantidad_inicial";
                    Parcantidadi.SqlDbType = SqlDbType.Int;
                    Parcantidadi.Value = dprodsol.Cantidad_retirar;
                    SqlCmd.Parameters.Add(Parcantidadi);

                    SqlParameter ParcantidadAct = new SqlParameter();
                    ParcantidadAct.ParameterName = "@cantidad_actual";
                    ParcantidadAct.SqlDbType = SqlDbType.Int;
                    ParcantidadAct.Value = dprodsol.Cantidad_retirar;
                    SqlCmd.Parameters.Add(ParcantidadAct);

                    SqlParameter ParFvencimiento = new SqlParameter();
                    ParFvencimiento.ParameterName = "@fvencimiento";
                    ParFvencimiento.SqlDbType = SqlDbType.Date;
                    ParFvencimiento.Value = dprodsol.fechadevencimiento;
                    SqlCmd.Parameters.Add(ParFvencimiento);

                    SqlParameter ParDescripcion = new SqlParameter();
                    ParDescripcion.ParameterName = "@descripcion";
                    ParDescripcion.SqlDbType = SqlDbType.VarChar;
                    ParDescripcion.Size = 100;
                    ParDescripcion.Value = dprodsol.Descripcion;
                    SqlCmd.Parameters.Add(ParDescripcion);
                    //Ejecutamos nuestro comando
                    rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";
                }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;}
   }
}
      
