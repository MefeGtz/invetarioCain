using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using capadato;

namespace capanegocio
{
   public class Nsolicitud
    {
       public static string Insertar(int idorden, string nombresol, DateTime fecha,DataTable dtproductosol)
       {
           dsolicitud Obj = new dsolicitud ( );
           Obj.Idorden = idorden;
           Obj.Nombresol = nombresol;
           Obj.Fecha = fecha;
           List<dprodsol> productossol = new List<dprodsol>();
           foreach (DataRow row in dtproductosol.Rows)
           {
               dprodsol detalle = new dprodsol();
               detalle.Idsolicitud = Convert.ToInt32(row["idsolicitud"].ToString());
               detalle.Idproductooc = Convert.ToInt32(row["idproductooc"].ToString());
               detalle.Punitario = Convert.ToInt32(row["P_unitario"].ToString());
               detalle.Cantidad_retirar = Convert.ToInt32(row["Cantidad_Retirar"].ToString());
               detalle.Pendiente_retirar = Convert.ToInt32(row["Pendiente_r"].ToString());
               detalle.Total = Convert.ToInt32(row["Total"].ToString());
               detalle.producto = Convert.ToString(row["Producto"].ToString());
               detalle.Descripcion = Convert.ToString(row["Descripción"].ToString());
               detalle.fechadevencimiento = Convert.ToDateTime(row["fvencimiento"].ToString());
               detalle.Observacion = Convert.ToString(row["Observación"].ToString());
               //agregoo a mi lista de productooc los los productos nombrados como detalle
               productossol.Add(detalle);
           }
           
           return Obj.Insertar(Obj, productossol);
       }

       public static string Editar(int idsolicitudp,int idorden, string nombresol, DateTime fecha)
       {
           dsolicitud Obj = new dsolicitud();
           Obj.Idsolicitudp = idsolicitudp;
           Obj.Idorden = idorden;
           Obj.Nombresol = nombresol;
           Obj.Fecha = fecha;
           return Obj.Editar(Obj); 
       }

       //Método Mostrar que llama al método Mostrar de la capa datos
       //de la CapaDatos
       public static DataTable Mostrar(string textobuscar)
       {
           return new dsolicitud().Mostrar(textobuscar);
       }

       public static DataTable MostrarDetalle(string textobuscar)
       {
           dsolicitud Obj = new dsolicitud();
           return Obj.MostrarDetalle(textobuscar);
       }
       //Método Eliminar que llama al método Eliminar de la clase DCategoría
       //de la CapaDatos
       public static string Eliminar(int idsolicitud)
       {
           dsolicitud Obj = new dsolicitud();
           Obj.Idsolicitudp = idsolicitud;
           return Obj.Eliminar(Obj);
       }

    }
}
