using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaMedico.PRESENTACION
{
    using SistemaMedico.MODEL;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ValidacionModeloDatos
    {
        public void Validate(object model)
        {
            string errorMessage = "";
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(model);

            // Validación para campos id.
            if (model is AtencionMedica atencion)
            {
                if (atencion.MedicoId <= 0)
                    errorMessage += "- Se requiere seleccionar un médico.\n";

                if (atencion.PacienteId <= 0)
                    errorMessage += "- Se requiere seleccionar un paciente.\n";
            }

            // Validación de atributos.
            bool isValid = Validator.TryValidateObject(model, context, results, true);
            if (!isValid)
            {
                foreach (var item in results)
                    errorMessage += "- " + item.ErrorMessage + "\n";
            }

            // Si hay errores, se lanza la excepción.
            if (!string.IsNullOrEmpty(errorMessage))
                throw new Exception(errorMessage);
        }
    }

}
