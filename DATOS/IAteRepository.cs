using SistemaMedico.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMedico.DATOS
{
    public interface IAteRepository
    {
        DataTable ListarMedicos();
        DataTable ListarPacientes();
        IEnumerable<DetalleAtencion> ListarAtenciones();
        void AgregarAtencion(AtencionMedica atencion);
        void EliminarAtencion(int id);
        void EditarAtencion(AtencionMedica atencion);
        AtencionMedica TraerAtencion(int id);
    }
}
