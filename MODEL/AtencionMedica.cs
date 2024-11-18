using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace SistemaMedico.MODEL
{ 
    public class AtencionMedica
    {
        private int id;
        private int pacienteId;
        private int medicoId;
        private string diagnostico;
        private DateTime? fechaAlta;

        public int Id { get { return id; } set { id = value; } }
        public int PacienteId { get { return pacienteId; } set { pacienteId = value; } }
        public int MedicoId { get { return medicoId; } set { medicoId = value; } }
        [Required(ErrorMessage = "Se requiere el diagnostico del paciente")]
        [StringLength(500, MinimumLength = 3, ErrorMessage = "El diagnostico puede tener entre 5 y 500 caracteres.")]
        public string Diagnostico { get { return diagnostico; } set { diagnostico = value; } }
        public DateTime? FechaAlta { get { return fechaAlta; } set { fechaAlta = value; } }
    }
}
