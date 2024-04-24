using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//librerias para la conexion 
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Data;

namespace capadato
{
    class conexion
    {
        //public SqlConnection conexion = new SqlConnection("Server=.;DataBase=bd_caine;Integrated Security=SSPI");
       //en el server va nuestro servidor en este caso tiene un ounto por que es el local
        //la case de datos con su nombre, el tipo de seguridad, esta en una variable de tipo publica
        //para ser utilizada por mas clases en esta capa
        //public static string Cn = "Server=.;DataBase=bd_caine;Integrated Security=SSPI";
        public static string Cn = Properties.Settings.Default.cn;
    }
}
