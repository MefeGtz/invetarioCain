using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using capadato;

namespace capanegocio
{
    public class Norden
    {
        //Método Insertar que llama al método Insertar de la clase Dorden
        //de la CapaDatos
        public static string Insertar(string proveedor, string direccion, string rtn, DateTime fecha, string telefono, string nrepresent,
            string ordenc,string estado, DataTable dtproductooc)
        {
            Dordenc Obj = new Dordenc();
            Obj.Provedor = proveedor;
            Obj.Direccion = direccion;
            Obj.RTN = rtn;
            Obj.Fecha = fecha;
            Obj.Telefono = telefono;
            Obj.Nrepresentante = nrepresent;
            Obj.Ordencompra = ordenc;
            Obj.Estado = estado;
            List<dproductooc> productooc = new List<dproductooc>();
            //del datable de la caa presentacion se cargan los datos a esta lista para ins¿gresar varios productos en una orden
            foreach (DataRow row in dtproductooc.Rows)
            {
                dproductooc detalle = new dproductooc();
                //detalle.Idorden = Convert.ToInt32(row["idorden"].ToString());
                detalle.Idcategoria = Convert.ToInt32(row["idcategoria"].ToString());
                detalle.Producto = Convert.ToString(row["Producto"].ToString());
                detalle.Descripcion = Convert.ToString(row["Descripción"].ToString());
                detalle.Fvencimiento = Convert.ToDateTime(row["fvencimiento"].ToString());
                detalle.Cantidad = Convert.ToInt32(row["Cantidad"].ToString());
                detalle.Punitario = Convert.ToInt32(row["P_unitario"].ToString());
                detalle.Total = Convert.ToInt32(row["Total"].ToString());
                detalle.Pendiente_sol = Convert.ToInt32(row["Pendiente_sol"].ToString());
                //agregoo a mi lista de productooc los los productos nombrados como detalle
                productooc.Add(detalle);
            }
            return Obj.Insertar(Obj,productooc);
        }

        //Método Editar que llama al método Editar de la clase Dorden
        //de la CapaDatos
        public static string Editar(int idorden,string proveedor, string direccion, string rtn, DateTime fecha, string telefono, string nrepresent, string ordenc, string estado)
        {
            Dordenc Obj = new Dordenc();
            Obj.Idorden = idorden;
            Obj.Provedor = proveedor;
            Obj.Direccion = direccion;
            Obj.RTN = rtn;
            Obj.Fecha = fecha;
            Obj.Telefono = telefono;
            Obj.Nrepresentante = nrepresent;
            Obj.Ordencompra = ordenc;
            Obj.Estado = estado;
            return Obj.Editar(Obj);
        }

        //Método Eliminar que llama al método Eliminar de la clase Datos
        //de la CapaDatos
        public static string Eliminar(int idorden)
        {
            Dordenc Obj = new Dordenc();
            Obj.Idorden = idorden;
            return Obj.Eliminar(Obj);
        }

        public static string Eliminarsolicitud(int idorden)
        {
            Dordenc Obj = new Dordenc();
            Obj.Idorden = idorden;
            return Obj.Eliminarsolicitudoc(Obj);
        }

        //Método Mostrar que llama al método Mostrar de la clase Dorden
        //de la CapaDatos
        public static DataTable Mostrar()
        {
            return new Dordenc().Mostrar();
        }

        public static DataTable Mostrarordensinliquidar()
        {
            return new Dordenc().Mostrarordensinliquidar();
        }

        public static DataTable Mostrarp0()
        {
            return new Dordenc().Mostrarp0();
        }

        /// <summary>
        /// para mostrar los productos de orden de compra
        /// </summary>
        /// <param name="textobuscar"></param>
        /// <returns></returns>
        public static DataTable MostrarDetalle(string textobuscar)
        {
            Dordenc Obj = new Dordenc();
            return Obj.MostrarDetalle(textobuscar);
        }
        //para que salgan solo losproductos con una cantidad pendiente a retirar mayor a 0
        public static DataTable MostrarDetalle1(string textobuscar)
        {
            Dordenc Obj = new Dordenc();
            return Obj.MostrarDetalle1(textobuscar);
        }

        public static DataTable buscarproductooc(string textobuscar, string producto)
        {
            Dordenc Obj = new Dordenc();
            return Obj.buscarproductonombre(textobuscar,producto);
        }
        public static DataTable Buscarorden(string textobuscar,string textobuscar2)
        {
            Dordenc Obj = new Dordenc();
            return Obj.Buscarorden(textobuscar,textobuscar2);
        }


    }

     

}
