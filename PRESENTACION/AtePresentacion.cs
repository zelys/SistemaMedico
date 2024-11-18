using SistemaMedico.DATOS;
using SistemaMedico.MODEL;
using SistemaMedico.VISTA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SistemaMedico.PRESENTACION
{
    public class AtePresentacion
    {
        private IAteVista vista;
        private IAteRepository ateRepository;
        private IEnumerable<DetalleAtencion> detalleAtenciones;
        private BindingSource atencionesBindingSource;
        private DataTable medicos;
        private DataTable pacientes;
        public AtePresentacion(IAteVista vista, IAteRepository ateRepository)
        {
            this.vista = vista;
            this.ateRepository = ateRepository;
            this.atencionesBindingSource = new BindingSource();
            this.medicos = new DataTable();
            this.pacientes = new DataTable();
            // Eventos
            this.vista.EventoAgregar += AgregarNuevaAtencion;
            this.vista.EventoEliminar += EliminarAtencion;
            this.vista.EventoEditar += CargarSeleccion;
            this.vista.EventoGuardar += GuardarAtencion;
            this.vista.EventoSeleccionMedico += CargarEspecialidad;
            this.vista.EventoCancelar += AccionCancelar;
            // Cargar listas de datos.
            CargarListaAtenciones();
            CargarListaMedicos();
            CargarListaPacientes();
            vista.ListarMedicos(medicos);
            vista.ListarPacientes(pacientes);
            this.vista.ListarAtencionesBindingSource(atencionesBindingSource);

            this.vista.Show();
        }

        private void AccionCancelar(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void CargarEspecialidad(object sender, EventArgs e)
        {
            vista.CargarEspecialidad();
        }

        private void GuardarAtencion(object sender, EventArgs e)
        {
            var atte = (DetalleAtencion)atencionesBindingSource.Current;
            var atencion = new AtencionMedica
            {
                Id = atte.Id,
                PacienteId = vista.PacienteId,
                MedicoId = vista.MedicoId,
                Diagnostico = vista.Diagnostico,
                FechaAlta = vista.FechaAlta
            };

            try
            {
                new ValidacionModeloDatos().Validate(atencion);
                if (vista.EsEdicion)
                {
                    ateRepository.EditarAtencion(atencion);
                    vista.Mensaje = "Atención editada correctamente";
                }
                else
                {
                    ateRepository.AgregarAtencion(atencion);
                    vista.Mensaje = "Atención agregada correctamente";
                }
                vista.Finalizado = true;
                CargarListaAtenciones();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                vista.Finalizado = false;
                vista.Mensaje = ex.Message;
            }
        }

        private void CargarSeleccion(object sender, EventArgs e)
        {
            // Obtener la atención seleccionada.
            var atte = (DetalleAtencion)atencionesBindingSource.Current;
            var atencion = ateRepository.TraerAtencion(atte.Id);

            // Asignar valores.
            if (atencion != null)
            {
                vista.MedicoId = atencion.MedicoId;
                vista.PacienteId = atencion.PacienteId;
                vista.Diagnostico = atencion.Diagnostico;
                vista.Especialidad = atte.Especialidad;
                vista.FechaAlta = atencion.FechaAlta;
                vista.CargarAtencionSeleccionada();
                vista.EsEdicion = true;
            }
        }

        private void EliminarAtencion(object sender, EventArgs e)
        {
            try
            {
                var atte = (DetalleAtencion)atencionesBindingSource.Current;
                ateRepository.EliminarAtencion(atte.Id);
                vista.Finalizado = true;
                vista.Mensaje = "Registro Eliminado";
                CargarListaAtenciones();
            }
            catch (Exception)
            {
                vista.Finalizado = false;
                vista.Mensaje = "Ocurrió un error, no se pudo eliminar el registro";
            }
        }

        private void AgregarNuevaAtencion(object sender, EventArgs e)
        {
            LimpiarCampos();
            vista.EsEdicion = false;
        }

        private void CargarListaAtenciones()
        {
            atencionesBindingSource.DataSource = ateRepository.ListarAtenciones();
        }

        private void CargarListaMedicos()
        {
            medicos = ateRepository.ListarMedicos();
        }

        private void CargarListaPacientes()
        {
            pacientes = ateRepository.ListarPacientes();
        }

        private void LimpiarCampos()
        {
            vista.Especialidad = "";
            vista.Diagnostico = "";
            vista.FechaAlta = null;
        }
    }
}
