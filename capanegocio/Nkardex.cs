using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using capadato;
using System.Data;

namespace capanegocio
{
   public class Nkardex
    {

       public static string insertar(int idproductob, DateTime fecha, string producto, string descripcion, int cantidad, int punitario, int total, int cant_restante,
            string entrega, string recib) {

                Dkardex obj = new Dkardex();
                obj.Idproductosb = idproductob;
                obj.Fecha = fecha;
                obj.Producto = producto;
                obj.Descripcion = descripcion;
                obj.Cantidad = cantidad;
                obj.Punitario = punitario;
                obj.Total = total;
                obj.Cantidad_restante = cant_restante;
                obj.Entrega = entrega;
                obj.Recibe = recib;
                return obj.Insertar(obj);
       }


        public static DataTable Mostrar()
        {
            return new Dkardex().Mostrar();
        }

        //Método BuscarNombre que llama al método BuscarNombre
        //de la clase DCategoría de la CapaDatos

        public static DataTable Buscarproducto(string textobuscar)
        {
            Dkardex Obj = new Dkardex();
            return Obj.Buscarproducto(textobuscar);
        }

        public static DataTable BuscarFechas(string textobuscar, string textobuscar2)
        {
            Dkardex Obj = new Dkardex();
            return Obj.BuscarFechas(textobuscar, textobuscar2);
        }

        public static DataTable vistaBuscarFechas(string textobuscar, string textobuscar2)
        {
            Dkardex Obj = new Dkardex();
            return Obj.vistaBuscarFechas(textobuscar, textobuscar2);
        }
        public static string Eliminar(int idkardex)
        {
            Dkardex Obj = new Dkardex();
            Obj.Idkardex = idkardex;
            return Obj.Eliminar(Obj);
        }

    }
}
