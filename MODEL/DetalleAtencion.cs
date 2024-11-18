using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMedico.MODEL
{
    public class DetalleAtencion
    {
        private int id;
        private DateTime fechaIngreso;
        private string paciente;
        private string medico;
        private string especialidad;
        private string diagnostico;
        private DateTime? fechaAlta;

        [DisplayName("ID")]
        public int Id { get { return id; } set { id = value; } }
        [DisplayName("Fecha Ingreso")]
        [DisplayFormat(DataFormatString = "dd-MM-yyyy")]
        public DateTime FechaIngreso { get { return fechaIngreso; } set { fechaIngreso = value; } }
        [DisplayName("Paciente")]
        public string Paciente { get { return paciente; } set { paciente = value; } }
        [DisplayName("Medico")]
        public string Medico { get { return medico; } set { medico = value; } }
        [DisplayName("Especialidad")]
        public string Especialidad { get { return especialidad; } set { especialidad = value; } }
        [DisplayName("Diagnostico")]
        public string Diagnostico { get { return diagnostico; } set { diagnostico = value; } }
        [DisplayName("Fecha Alta")]
        [DisplayFormat(DataFormatString = "dd-MM-yyyy")]
        public DateTime? FechaAlta { get { return fechaAlta; } set { fechaAlta = value; } }
    }
}
