using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;

namespace capadato
{
   public  class Dbodega
    {
       
  private int _iddetalledesolicitud;
  
  private int _preciounitario;
  private int _cantidadinicial;
  private int _cantidadactual;
  private string _textobuscar;

  public string Textobuscar
  {
      get { return _textobuscar; }
      set { _textobuscar = value; }
  }

  

       private string descripcion;

public string Descripcion
{
  get { return descripcion; }
  set { descripcion = value; }
}

private int _idproducto;
        public int Idproducto
        {
            get { return _idproducto; }
            set { _idproducto= value; }
        }
       
        public int iddetalledesolicitud
        {
            get { return _iddetalledesolicitud; }
            set { _iddetalledesolicitud = value; }
        }


        private string _producto;
        public string producto
        {
            get { return _producto; }
            set { _producto = value; }
        }
       
        public int Preciounitario
        {
            get { return _preciounitario; }
            set { _preciounitario = value; }
        }
        
        public int Cantidadinicial
        {
            get { return _cantidadinicial; }
            set { _cantidadinicial = value; }
        }
       
        public int cantidadactual
        {
            get { return _cantidadactual; }
            set { _cantidadactual = value; }
        }

        private DateTime _fechadevencimiento; 

        public DateTime fechadevencimiento
        {
            get { return _fechadevencimiento; }
            set { _fechadevencimiento = value; }
        }

public Dbodega()
{

}
public Dbodega(int idproducto,int iddetalledesolicitud,string producto, int preciounitario, int cantidadinicial,int cantidadactual,DateTime fechadevencimiento, string descripcion)
{
    this.Idproducto=_idproducto;
    this.iddetalledesolicitud=iddetalledesolicitud;
    this.producto=producto;
    this.Preciounitario=preciounitario;
    this.Cantidadinicial=cantidadinicial;
    this.cantidadactual=cantidadactual;
    this.fechadevencimiento=fechadevencimiento;
    this.Descripcion=descripcion;
}
         public string Insertar(Dbodega Dbodega , ref SqlConnection SqlCon, ref SqlTransaction SqlTra)
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
                Pariddsolicitud.Value = Dbodega.iddetalledesolicitud;
                SqlCmd.Parameters.Add(Pariddsolicitud);

                 SqlParameter Parproducto = new SqlParameter();
                Parproducto.ParameterName = "@producto";
                Parproducto.SqlDbType = SqlDbType.VarChar;
                Parproducto.Size = 50;
                Parproducto.Value = Dbodega.producto;
                SqlCmd.Parameters.Add(Parproducto);

                SqlParameter Parpunitario = new SqlParameter();
                Parpunitario.ParameterName = "@punitario";
                Parpunitario.SqlDbType = SqlDbType.Int;
                Parpunitario.Value = Dbodega.Preciounitario;
                SqlCmd.Parameters.Add(Parpunitario);

                SqlParameter Parcantidadr = new SqlParameter();
                Parcantidadr.ParameterName = "@cantidad_inicial";
                Parcantidadr.SqlDbType = SqlDbType.Int;
                Parcantidadr.Value = Dbodega.Cantidadinicial;
                SqlCmd.Parameters.Add(Parcantidadr);

                SqlParameter Parcantidadp = new SqlParameter();
                Parcantidadp.ParameterName = "@cantidad_actual";
                Parcantidadp.SqlDbType = SqlDbType.Int;
                Parcantidadp.Value = Dbodega.cantidadactual;
                SqlCmd.Parameters.Add(Parcantidadp);

                //SqlParameter Partotal = new SqlParameter();
                //Partotal.ParameterName = "@total";
                //Partotal.SqlDbType = SqlDbType.Int;
                //Partotal.Value = dprodsol.Total;
                //SqlCmd.Parameters.Add(Partotal);

               SqlParameter ParFvencimiento = new SqlParameter();
                ParFvencimiento.ParameterName = "@fvencimiento";
                ParFvencimiento.SqlDbType = SqlDbType.Date;
                ParFvencimiento.Value = Dbodega.fechadevencimiento;
                SqlCmd.Parameters.Add(ParFvencimiento);

                SqlParameter ParDescripcion = new SqlParameter();
                ParDescripcion.ParameterName = "@descripcion";
                ParDescripcion.SqlDbType = SqlDbType.VarChar;
                ParDescripcion.Size = 100;
                ParDescripcion.Value = Dbodega.Descripcion;
                SqlCmd.Parameters.Add(ParDescripcion);
                //Ejecutamos nuestro comando

                rpta = SqlCmd.ExecuteNonQuery() == 1 ? "OK" : "NO se Ingreso el Registro";
            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            return rpta;
        }


         public DataTable Mostrar()
         {
             DataTable DtResultado = new DataTable("Productosb");
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = conexion.Cn;
                 SqlCommand SqlCmd = new SqlCommand();
                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "spmostrar_productosb";
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

       //mostrar productos que tengana una cantidad mayor a 1
         public DataTable Mostrar0()
         {
             DataTable DtResultado = new DataTable("Productosb");
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = conexion.Cn;
                 SqlCommand SqlCmd = new SqlCommand();
                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "spmostrar_productosb>0";
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

       //este metodo es para mostrar los productos con una cantidad menor a tres
         public DataTable Mostrarproductosagastarse()
         {
             DataTable DtResultado = new DataTable("Productosb");
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = conexion.Cn;
                 SqlCommand SqlCmd = new SqlCommand();
                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "spmostrar_productosbagastarse";
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

         public DataTable Buscarproductoagastarse(Dbodega Dbodega)
         {
             DataTable DtResultado = new DataTable("Productosb");
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = conexion.Cn;
                 SqlCommand SqlCmd = new SqlCommand();
                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "spbuscar_productosbagastarse";
                 SqlCmd.CommandType = CommandType.StoredProcedure;

                 SqlParameter ParTextoBuscar = new SqlParameter();
                 ParTextoBuscar.ParameterName = "@producto";
                 ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                 ParTextoBuscar.Size = 50;
                 ParTextoBuscar.Value = Dbodega.Textobuscar;
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
         public DataTable BuscarFechas(String TextoBuscar, String TextoBuscar2)
         {
             DataTable DtResultado = new DataTable("Products");
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = conexion.Cn;
                 SqlCommand SqlCmd = new SqlCommand();
                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "spmostrar_productosavencer";
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




         //Método BuscarNombre
         public DataTable BuscarNombre(Dbodega Dbodega)
         {
             DataTable DtResultado = new DataTable("Productosb");
             SqlConnection SqlCon = new SqlConnection();
             try
             {
                 SqlCon.ConnectionString = conexion.Cn;
                 SqlCommand SqlCmd = new SqlCommand();
                 SqlCmd.Connection = SqlCon;
                 SqlCmd.CommandText = "spbuscar_productobnombre";
                 SqlCmd.CommandType = CommandType.StoredProcedure;

                 SqlParameter ParTextoBuscar = new SqlParameter();
                 ParTextoBuscar.ParameterName = "@producto";
                 ParTextoBuscar.SqlDbType = SqlDbType.VarChar;
                 ParTextoBuscar.Size = 50;
                 ParTextoBuscar.Value = Dbodega.Textobuscar;
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

         public string Eliminar(Dbodega bodega)
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
                 SqlCmd.CommandText = "speliminar_productosb";
                 SqlCmd.CommandType = CommandType.StoredProcedure;

                 SqlParameter ParIdproductosb= new SqlParameter();
                 ParIdproductosb.ParameterName = "@idproductosb";
                 ParIdproductosb.SqlDbType = SqlDbType.Int;
                 ParIdproductosb.Value = bodega.Idproducto;
                 SqlCmd.Parameters.Add(ParIdproductosb);
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

