using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using SistemaMedico.DATOS;
using SistemaMedico.MODEL;

namespace SistemaMedico.VISTA
{
    public partial class AtencionesForm : Form, IAteVista
    {
        private string mensaje;
        private bool finalizado;
        private bool esEdicion;
        private int pIndex;
        private int mIndex;

        public AtencionesForm()
        {
            InitializeComponent();
            tabControl1.TabPages.Remove(tabPage2);

            // Botón Agregar
            btnAgregar.Click += delegate
            {
                EventoAgregar?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Add(tabPage2);
                tabPage2.Text = "Agregar";
                cmbMedicos.SelectedIndex = -1;
                cmbPacientes.SelectedIndex = -1;
            };

            // Botón Editar
            btnEditar.Click += delegate
            {
                EventoEditar?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPage1);
                tabControl1.TabPages.Add(tabPage2);
                tabPage2.Text = "Editar";
                cmbMedicos.SelectedIndex = mIndex-1;
                cmbPacientes.SelectedIndex = pIndex-1;
            };

            // Botón Eliminar
            btnEliminar.Click += delegate
            {
                if (dgvAtenciones.SelectedRows.Count > 0)
                {
                    var result = MessageBox.Show("¿Está seguro de que desea eliminar el registro seleccionado?", "Advertencia",
                      MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        EventoEliminar?.Invoke(this, EventArgs.Empty);
                        MessageBox.Show(Mensaje);
                    }
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un registro para eliminar.");
                }
            };
            // Botón Guardar
            btnGuardar.Click += delegate
            {
                EventoGuardar?.Invoke(this, EventArgs.Empty);
                if (Finalizado)
                {
                    tabControl1.TabPages.Remove(tabPage2);
                    tabControl1.TabPages.Add(tabPage1);
                }
                MessageBox.Show(Mensaje);
            };

            //Cancelar
            btnCancelar.Click += delegate
            {
                EventoCancelar?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Add(tabPage1);
                cmbMedicos.SelectedIndex = -1;
                cmbPacientes.SelectedIndex = -1;
            };

            cmbMedicos.SelectedIndexChanged += delegate 
            {
                EventoSeleccionMedico?.Invoke(this, EventArgs.Empty);
            };
        }

        public void ListarAtencionesBindingSource(BindingSource atenciones)
        {
            dgvAtenciones.DataSource = atenciones;
        }

        public void ListarMedicos(DataTable medicos)
        {
            cmbMedicos.DataSource = medicos;
            cmbMedicos.DisplayMember = "Doctor_Name";
            cmbMedicos.ValueMember = "Doctor_Id";
            cmbMedicos.SelectedIndex = -1;
        }

        public void ListarPacientes(DataTable pacientes)
        {
            cmbPacientes.DataSource = pacientes;
            cmbPacientes.DisplayMember = "Patient_Name";
            cmbPacientes.ValueMember = "Patient_Id";
            cmbPacientes.SelectedIndex = -1;
        }

        public void CargarAtencionSeleccionada()
        {
            if (dgvAtenciones.SelectedRows.Count > 0)
            {
                cmbPacientes.SelectedValue = PacienteId;
                cmbMedicos.SelectedValue = MedicoId;
                txtDiagnostico.Text = Diagnostico;
                if (FechaAlta.HasValue)
                    dtpFechaAlta.Value = FechaAlta.Value;

            }
            else
            {
                MessageBox.Show("Debes seleccionar una fila de la tabla");
            }
        }

        public void CargarEspecialidad()
        {
            if (cmbMedicos.SelectedItem is DataRowView rowView)
                txtEspecialidad.Text = rowView["Specialty_Name"].ToString();
            else
                txtEspecialidad.Text = string.Empty;

        }

        public int MedicoId
        {
            get { return Convert.ToInt32(cmbMedicos.SelectedValue); }
            set { mIndex = value; }
        }
        public int PacienteId
        {
            get { return Convert.ToInt32(cmbPacientes.SelectedValue); }
            set { pIndex = value; }
        }
        public string Especialidad 
        { 
            get { return txtEspecialidad.Text; } 
            set { txtEspecialidad.Text = value; } 
        }
        public string Diagnostico 
        { 
            get { return txtDiagnostico.Text; } 
            set { txtDiagnostico.Text = value; } 
        }
        public DateTime? FechaAlta { get; set; }
        public string Mensaje 
        { 
            get { return mensaje; } 
            set {  mensaje = value; }
        }
        public bool EsEdicion 
        {
            get { return esEdicion; }
            set { esEdicion = value; }
        }
        public bool Finalizado 
        {
            get { return finalizado; }
            set { finalizado = value; }
        }
        public int Id { get; set; }

        public event EventHandler EventoAgregar;
        public event EventHandler EventoEditar;
        public event EventHandler EventoEliminar;
        public event EventHandler EventoGuardar;
        public event EventHandler EventoSeleccionMedico;
        public event EventHandler EventoCancelar;
    }
}
