using SistemaMedico.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaMedico.VISTA
{
    public interface IAteVista
    {
        int Id { get; set; }
        int MedicoId { get; set; }
        int PacienteId { get; set; }
        string Especialidad { get; set; }
        string Diagnostico { get; set; }
        DateTime? FechaAlta { get; set; }
        string Mensaje { get; set; }
        bool EsEdicion { get; set; }
        bool Finalizado { get; set; }

        event EventHandler EventoAgregar;
        event EventHandler EventoEditar;
        event EventHandler EventoEliminar;
        event EventHandler EventoGuardar;
        event EventHandler EventoSeleccionMedico;
        event EventHandler EventoCancelar;
        void ListarMedicos(DataTable medicos);
        void ListarPacientes(DataTable pacientes);
        void ListarAtencionesBindingSource(BindingSource atenciones);
        void CargarAtencionSeleccionada();
        void CargarEspecialidad();
        void Show();
    }
}
