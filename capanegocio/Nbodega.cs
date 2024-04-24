using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using capadato;
using System.Data;

namespace capanegocio
{
   public class Nbodega
    {
       public static DataTable Mostrar()
       {
           return new Dbodega().Mostrar();
       }

       public static DataTable Mostrar0()
       {
           return new Dbodega().Mostrar0();
       }

       public static DataTable Mostrarproductoagastarse()
       {
           return new Dbodega().Mostrarproductosagastarse();
       }

       public static DataTable buscarproductoagastarse(string textobuscar)
       {
           Dbodega Obj = new Dbodega();
           Obj.Textobuscar = textobuscar;
           return Obj.Buscarproductoagastarse(Obj);
       }

       public static DataTable buscarfechas(string textobuscar, string textobuscar2)
       {
           Dbodega Obj = new Dbodega();
           return Obj.BuscarFechas(textobuscar,textobuscar2);
       }


       public static DataTable buscarnombre(string textobuscar) 
       {
           Dbodega Obj = new Dbodega();
           Obj.Textobuscar = textobuscar;
           return Obj.BuscarNombre(Obj);
       }

       public static string Eliminar(int idproductob)
       {
           Dbodega Obj = new Dbodega();
           Obj.Idproducto = idproductob;
           return Obj.Eliminar(Obj);
       }

    }
}
