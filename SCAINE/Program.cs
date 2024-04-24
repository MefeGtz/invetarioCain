using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using capapresentacion;

namespace SCAINE
{
  public static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        /// 
        //Datos categoria
        public static int idcategoria;
        public static String nombrecat;

        [STAThread]
        static void Main()
        {
        
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MDIP());
        }
    }
}
