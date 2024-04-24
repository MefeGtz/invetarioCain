using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;

namespace capadato
{
   public class dproductooc
    {
        private int _idproductooc;

        public int Idproductooc
        {
            get { return _idproductooc; }
            set { _idproductooc = value; }
        }
        private int _idorden;

        public int Idorden
        {
            get { return _idorden; }
            set { _idorden = value; }
        }
        private int _idcategoria;

        public int Idcategoria
        {
            get { return _idcategoria; }
            set { _idcategoria = value; }
        }
        private string _producto;

        public string Producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
        private string _descripcion;

        public string Descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }
        private DateTime _fvencimiento;

        public DateTime Fvencimiento
        {
            get { return _fvencimiento; }
            set { _fvencimiento = value; }
        }
        private int _cantidad;

        public int Cantidad
        {
            get { return _cantidad; }
            set { _cantidad = value; }
        }
        private int _punitario;

        public int Punitario
        {
            get { return _punitario; }
            set { _punitario = value; }
        }
        private int _total;

        public int Total
        {
            get { return _total; }
            set { _total = value; }
        }
        // los bob constructores
        private int _pendiente_sol;
        public int Pendiente_sol
        {
            get { return _pendiente_sol; }
            set { _pendiente_sol = value; }
        }

        public dproductooc() { }

        public dproductooc(int idproductoc, int idorden, int idcategoria, string producto, string descripcion, DateTime fvencimiento, int cantidad, int punitario, int total, int pendiente_sol)
        {
            this.Idproductooc = idproductoc;
            this.Idorden = idorden;
            this.Idcategoria = idcategoria;
            this.Producto = producto;
            this.Descripcion = descripcion;
            this.Fvencimiento = fvencimiento;
            this.Cantidad = cantidad;
            this.Punitario = punitario;
            this.Total = total;
            this.Pendiente_sol = pendiente_sol;
        }

       
       //este metodo vamos a recibir tres parametros y lo vamos a ejecutar en el mismo proceso de la insercion de orden de compra
       //recibe tres parametros, la referencia de la clase y todos sus argumentos, la conexion y la transaccion que se esta efectuando.
        public string Insertar(dproductooc dproductooc, ref SqlConnection SqlCon, ref SqlTransaction SqlTra)
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
                SqlCmd.CommandText = "spinsertar_productooc";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdproductooc = new SqlParameter();
                ParIdproductooc.ParameterName = "@idproductooc";
                ParIdproductooc.SqlDbType = SqlDbType.Int;
                ParIdproductooc.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParIdproductooc);

                SqlParameter ParIdorden = new SqlParameter();
                ParIdorden.ParameterName = "@idorden";
                ParIdorden.SqlDbType = SqlDbType.Int;
                ParIdorden.Value = dproductooc.Idorden;
                SqlCmd.Parameters.Add(ParIdorden);

                SqlParameter ParIdcategoria = new SqlParameter();
                ParIdcategoria.ParameterName = "@idcategoria";
                ParIdcategoria.SqlDbType = SqlDbType.Int;
                ParIdcategoria.Value = dproductooc.Idcategoria;
                SqlCmd.Parameters.Add(ParIdcategoria);

                SqlParameter ParProducto = new SqlParameter();
                ParProducto.ParameterName = "@producto";
                ParProducto.SqlDbType = SqlDbType.VarChar;
                ParProducto.Size = 50;
                ParProducto.Value = dproductooc.Producto;
                SqlCmd.Parameters.Add(ParProducto);

                SqlParameter ParDescripcion = new SqlParameter();
                ParDescripcion.ParameterName = "@descripcion";
                ParDescripcion.SqlDbType = SqlDbType.VarChar;
                ParDescripcion.Size = 100;
                ParDescripcion.Value = dproductooc.Descripcion;
                SqlCmd.Parameters.Add(ParDescripcion);

                SqlParameter ParFvencimiento = new SqlParameter();
                ParFvencimiento.ParameterName = "@fvencimiento";
                ParFvencimiento.SqlDbType = SqlDbType.Date;
                ParFvencimiento.Value = dproductooc.Fvencimiento;
                SqlCmd.Parameters.Add(ParFvencimiento);

                SqlParameter Parcantidad = new SqlParameter();
                Parcantidad.ParameterName = "@cantidad";
                Parcantidad.SqlDbType = SqlDbType.Int;
                Parcantidad.Value = dproductooc.Cantidad;
                SqlCmd.Parameters.Add(Parcantidad);

                SqlParameter Parpunitario = new SqlParameter();
                Parpunitario.ParameterName = "@punitario";
                Parpunitario.SqlDbType = SqlDbType.Int;
                Parpunitario.Value = dproductooc.Punitario;
                SqlCmd.Parameters.Add(Parpunitario);

                SqlParameter Partotal = new SqlParameter();
                Partotal.ParameterName = "@total";
                Partotal.SqlDbType = SqlDbType.Int;
                Partotal.Value = dproductooc.Total;
                SqlCmd.Parameters.Add(Partotal);

                SqlParameter Parpendientesol = new SqlParameter();
                Parpendientesol.ParameterName = "@pendientesol";
                Parpendientesol.SqlDbType = SqlDbType.Int;
                Parpendientesol.Value = dproductooc.Pendiente_sol;
                SqlCmd.Parameters.Add(Parpendientesol);
                //Ejecutamos nuestro comando
                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }
    }
}
