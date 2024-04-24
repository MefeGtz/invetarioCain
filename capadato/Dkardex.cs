using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;


namespace capadato
{
   public class Dkardex
    {
        private int _idkardex;

        public int Idkardex
        {
            get { return _idkardex; }
            set { _idkardex = value; }
        }
        private int _idproductosb;

        public int Idproductosb
        {
            get { return _idproductosb; }
            set { _idproductosb = value; }
        }
        private DateTime _fecha;

        public DateTime Fecha
        {
            get { return _fecha; }
            set { _fecha = value; }
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
        private int _cantidad_restante;

        public int Cantidad_restante
        {
            get { return _cantidad_restante; }
            set { _cantidad_restante = value; }
        }
        private string _entrega;

        public string Entrega
        {
            get { return _entrega; }
            set { _entrega = value; }
        }
        private string _recibe;

        public string Recibe
        {
            get { return _recibe; }
            set { _recibe = value; }
        }

        public Dkardex() { }

        public Dkardex(int idkardex, int idproductobod, DateTime fecha, string producto, string descripcion, int cantidad, int punitario, int total, int cant_restante,
            string entrega, string recibe) {
                this.Idkardex = idkardex;
                this.Idproductosb = idproductobod;
                this.Fecha = fecha;
                this.Producto = producto;
                this.Descripcion = descripcion;
                this.Cantidad = cantidad;
                this.Punitario = punitario;
                this.Total = total;
                this.Cantidad_restante = cant_restante;
                this.Entrega = entrega;
                this.Recibe = recibe;
        }

        public string Insertar(Dkardex Dkardex)
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
                SqlCmd.CommandText = "spinsertar_kardex";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdkardex = new SqlParameter();
                ParIdkardex.ParameterName = "@idkardex";
                ParIdkardex.SqlDbType = SqlDbType.Int;
                ParIdkardex.Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add(ParIdkardex);

                SqlParameter Paridproductobod = new SqlParameter();
                Paridproductobod.ParameterName = "@idproductosb";
                Paridproductobod.SqlDbType = SqlDbType.Int;
                Paridproductobod.Value = Dkardex.Idproductosb;
                SqlCmd.Parameters.Add(Paridproductobod);

                SqlParameter Parfecha = new SqlParameter();
                Parfecha.ParameterName = "@fecha";
                Parfecha.SqlDbType = SqlDbType.Date;
                Parfecha.Size = 256;
                Parfecha.Value = Dkardex.Fecha;
                SqlCmd.Parameters.Add(Parfecha);

                SqlParameter ParProducto = new SqlParameter();
                ParProducto.ParameterName = "@producto";
                ParProducto.SqlDbType = SqlDbType.VarChar;
                ParProducto.Size = 50;
                ParProducto.Value = Dkardex.Producto;
                SqlCmd.Parameters.Add(ParProducto);

                SqlParameter ParDescripcion = new SqlParameter();
                ParDescripcion.ParameterName = "@descripcion";
                ParDescripcion.SqlDbType = SqlDbType.VarChar;
                ParDescripcion.Size = 256;
                ParDescripcion.Value = Dkardex.Descripcion;
                SqlCmd.Parameters.Add(ParDescripcion);

                SqlParameter Parcantidad = new SqlParameter();
                Parcantidad.ParameterName = "@cantidad";
                Parcantidad.SqlDbType = SqlDbType.Int;
                Parcantidad.Value = Dkardex.Cantidad;
                SqlCmd.Parameters.Add(Parcantidad);

                SqlParameter Parpunitario = new SqlParameter();
                Parpunitario.ParameterName = "@punitario";
                Parpunitario.SqlDbType = SqlDbType.Int;
                Parpunitario.Value = Dkardex.Punitario;
                SqlCmd.Parameters.Add(Parpunitario);

                SqlParameter Partotal = new SqlParameter();
                Partotal.ParameterName = "@total";
                Partotal.SqlDbType = SqlDbType.Int;
                Partotal.Value = Dkardex.Total;
                SqlCmd.Parameters.Add(Partotal);

                SqlParameter Parcantp = new SqlParameter();
                Parcantp.ParameterName = "@cantidad_restante";
                Parcantp.SqlDbType = SqlDbType.Int;
                Parcantp.Value = Dkardex.Cantidad_restante;
                SqlCmd.Parameters.Add(Parcantp);

                SqlParameter Parentrega = new SqlParameter();
                Parentrega.ParameterName = "@entrega";
                Parentrega.SqlDbType = SqlDbType.VarChar;
                Parentrega.Size = 256;
                Parentrega.Value = Dkardex.Entrega;
                SqlCmd.Parameters.Add(Parentrega);

                SqlParameter Parrecibe = new SqlParameter();
                Parrecibe.ParameterName = "@recibe";
                Parrecibe.SqlDbType = SqlDbType.VarChar;
                Parrecibe.Size = 256;
                Parrecibe.Value = Dkardex.Recibe;
                SqlCmd.Parameters.Add(Parrecibe);

                //Ejecutamos nuestro comando

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";
                if (rpta.Equals("OK"))
                {
                    rpta = Disminuircantidadb(Idproductosb,Cantidad);
                }
                //Actualizamos el stock o la cantidad pendiente a retirar
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


        //Métodos
        public string Disminuircantidadb(int idproductob, int cantidad)
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
                SqlCmd.CommandText = "spdisminuir_cantidadbodega";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdproductooc = new SqlParameter();
                ParIdproductooc.ParameterName = "@idproductosb";
                ParIdproductooc.SqlDbType = SqlDbType.Int;
                ParIdproductooc.Value = idproductob;
                SqlCmd.Parameters.Add(ParIdproductooc);

                SqlParameter ParCantidad = new SqlParameter();
                ParCantidad.ParameterName = "@cantidad";
                ParCantidad.SqlDbType = SqlDbType.Int;
                ParCantidad.Value = cantidad;
                SqlCmd.Parameters.Add(ParCantidad);
                //Ejecutamos nuestro comando
                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "No se Actualizó el stock en bodega";
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

        public DataTable Mostrar()
        {
            DataTable DtResultado = new DataTable("Kardex");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spmostrar_Kardex";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter SqlDat = new SqlDataAdapter(SqlCmd);
                SqlDat.Fill(DtResultado);
            }
            catch (Exception ex)
            {
                DtResultado = null;
            }
            return DtResultado;
        }
        //Método Buscarfechas
        public DataTable BuscarFechas(String TextoBuscar, String TextoBuscar2)
        {
            DataTable DtResultado = new DataTable("kardx");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spimprimir_kardex";
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
                ParTextoBuscar2.Value = TextoBuscar2;
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

       //este es el metodo que vamos a ver y el  de arriba es el que vamos a usar para imprimir
        public DataTable vistaBuscarFechas(String TextoBuscar, String TextoBuscar2)
        {
            DataTable DtResultado = new DataTable("kardex");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spbuscarentrefechas_kardex";
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
                ParTextoBuscar2.Value = TextoBuscar2;
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

        public DataTable Buscarproducto(string textoproducto)
        {
            DataTable DtResultado = new DataTable("Kardexprod");
            SqlConnection SqlCon = new SqlConnection();
            try
            {
                SqlCon.ConnectionString = conexion.Cn;
                SqlCommand SqlCmd = new SqlCommand();
                SqlCmd.Connection = SqlCon;
                SqlCmd.CommandText = "spbuscar_kardexprod";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParTextoBuscar = new SqlParameter();
                ParTextoBuscar.ParameterName = "@textobuscar";
                ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                ParTextoBuscar.Size = 100;
                ParTextoBuscar.Value = textoproducto;
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

        //Método Eliminar
        public string Eliminar(Dkardex kardex)
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
                SqlCmd.CommandText = "speliminar_kardex";
                SqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter ParIdcategoria = new SqlParameter();
                ParIdcategoria.ParameterName = "@idkardex";
                ParIdcategoria.SqlDbType = SqlDbType.Int;
                ParIdcategoria.Value = kardex.Idkardex;
                SqlCmd.Parameters.Add(ParIdcategoria);
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

    }
}
