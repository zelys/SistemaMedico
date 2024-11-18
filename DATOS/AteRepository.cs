using SistemaMedico.MODEL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SistemaMedico.DATOS
{
    public class AteRepository : IAteRepository
    {
        private ConexionBD Conexion;
        private SqlCommand Comando;
        private SqlDataReader LeerFilas;
        public AteRepository(ConexionBD Conexion) 
        { 
            this.Conexion = Conexion;
            this.Comando = new SqlCommand();
        }

        public DataTable ListarMedicos()
        {
            var lista = new DataTable();
            Comando.Connection = Conexion.AbrirConexion();
            Comando.CommandText = "SELECT * FROM DoctorsView";
            Comando.CommandType = CommandType.Text;
            LeerFilas = Comando.ExecuteReader();
            lista.Load(LeerFilas);
            LeerFilas.Close();
            Conexion.CerrarConexion();
            return lista;
        }

        public DataTable ListarPacientes()
        {
            var lista = new DataTable();
            Comando.Connection = Conexion.AbrirConexion();
            Comando.CommandText = "SELECT Patient_Id, Patient_Name FROM Patients";
            Comando.CommandType = CommandType.Text;
            LeerFilas = Comando.ExecuteReader();
            lista.Load(LeerFilas);
            LeerFilas.Close();
            Conexion.CerrarConexion();
            return lista;
        }

        public void AgregarAtencion(AtencionMedica atencion) 
        {
            // Configurar la conexión y el comando
            Comando.Connection = Conexion.AbrirConexion();
            Comando.CommandText = "AddMedicalAtention"; // Nombre del procedimiento
            Comando.CommandType = CommandType.StoredProcedure;

            // Agregar parámetros al comando
            Comando.Parameters.Clear(); // Limpia parámetros previos
            Comando.Parameters.AddWithValue("@Patient_Id", atencion.PacienteId);
            Comando.Parameters.AddWithValue("@Doctor_Id", atencion.MedicoId);
            Comando.Parameters.AddWithValue("@Diagnosis", atencion.Diagnostico);

            // Agregar fecha de alta si tiene valor; de lo contrario, usar DBNull
            if (atencion.FechaAlta.HasValue)
            {
                Comando.Parameters.AddWithValue("@Discharge_Date", atencion.FechaAlta.Value);
            }
            else
            {
                Comando.Parameters.AddWithValue("@Discharge_Date", DBNull.Value);
            }
            Comando.ExecuteNonQuery();
            Comando.Parameters.Clear();
            Conexion.CerrarConexion();
        }

        public IEnumerable<DetalleAtencion> ListarAtenciones()
        {
            var ateLista = new List<DetalleAtencion>();
            Comando.Connection = Conexion.AbrirConexion();
            Comando.CommandText = "SELECT * FROM MedicalAttentionView";
            Comando.CommandType = CommandType.Text;
            LeerFilas = Comando.ExecuteReader();
            while (LeerFilas.Read())
            {
                var atencion = new DetalleAtencion
                {
                    Id = (int)LeerFilas[0],
                    FechaIngreso = (DateTime)LeerFilas[1],
                    Paciente = LeerFilas[2].ToString(),
                    Medico = LeerFilas[3].ToString(),
                    Especialidad = LeerFilas[4].ToString(),
                    Diagnostico = LeerFilas[5].ToString(),
                    FechaAlta = LeerFilas.IsDBNull(6) ? (DateTime?)null : (DateTime)LeerFilas[6]
                };
                ateLista.Add(atencion);
            }
            LeerFilas.Close();
            Conexion.CerrarConexion();

            return ateLista;
        }

        public void EliminarAtencion(int id)
        {
            Comando.Connection = Conexion.AbrirConexion();
            Comando.CommandText = "DELETE FROM Medical_Attention WHERE Attention_Id=@id";
            Comando.Parameters.Add("@id", SqlDbType.Int).Value = id;
            Comando.ExecuteNonQuery();
            Comando.Parameters.Clear();
            Conexion.CerrarConexion();
        }

        public void EditarAtencion(AtencionMedica atencion)
        {
            Comando.Connection = Conexion.AbrirConexion();
            Comando.CommandText = "UpdateMedicalAttention"; // Nombre del procedimiento
            Comando.CommandType = CommandType.StoredProcedure;
            Comando.Parameters.Add("@AttentionId", SqlDbType.Int).Value = atencion.Id;
            Comando.Parameters.Add("@PatientId", SqlDbType.Int).Value = atencion.PacienteId;
            Comando.Parameters.Add("@DoctorId", SqlDbType.Int).Value = atencion.MedicoId;
            Comando.Parameters.Add("@Diagnosis", SqlDbType.Text).Value = atencion.Diagnostico;
            // Validamos y asignamos FechaAlta o DBNull
            if (atencion.FechaAlta.HasValue)
            {
                Comando.Parameters.Add("@DischargeDate", SqlDbType.DateTime).Value = atencion.FechaAlta.Value;
            }
            else
            {
                Comando.Parameters.Add("@DischargeDate", SqlDbType.DateTime).Value = DBNull.Value;
            }
            // Ejecutamos el procedimiento almacenado
            Comando.ExecuteNonQuery();
            Comando.Parameters.Clear();
            Conexion.CerrarConexion();
        }


        public AtencionMedica TraerAtencion(int id)
        {
            var atencion = new AtencionMedica();
            Comando.Connection = Conexion.AbrirConexion();
            Comando.CommandText = "SELECT * FROM Medical_Attention WHERE Attention_Id = @id";
            Comando.CommandType = CommandType.Text;
            Comando.Parameters.Add("@id", SqlDbType.Int).Value = id;
            var lector = Comando.ExecuteReader();
            if (lector.Read())
            {
                atencion.PacienteId = (int)lector["Patient_Id"];
                atencion.MedicoId = (int)lector["Doctor_Id"];
                atencion.Diagnostico = lector["Diagnosis"].ToString();
                atencion.FechaAlta = lector.IsDBNull(5) ? (DateTime?)null : (DateTime)lector["Discharge_Date"];
            }
            Comando.Parameters.Clear();
            Conexion.CerrarConexion();
            return atencion;
        }
    }
}
