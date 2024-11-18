using SistemaMedico.DATOS;
using SistemaMedico.PRESENTACION;
using SistemaMedico.VISTA;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaMedico
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ConexionBD Conexion = new ConexionBD();
            IAteRepository repository = new AteRepository(Conexion);
            IAteVista vista = new AtencionesForm();
            new AtePresentacion(vista, repository);
            Application.Run((Form) vista);
        }
    }
}
