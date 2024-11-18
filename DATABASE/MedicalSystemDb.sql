CREATE DATABASE MedicalSystemDb;
GO
USE MedicalSystemDb;
GO
CREATE TABLE Specialties (
    Specialty_Id INT PRIMARY KEY IDENTITY(1,1),
    Specialty_Name VARCHAR(50) NOT NULL,
    Update_At DATETIME NOT NULL DEFAULT GETDATE()
);
GO

CREATE TABLE Patients (
    Patient_Id INT PRIMARY KEY IDENTITY(1,1),
    Patient_Name VARCHAR(50) NOT NULL,
    Patient_Dni VARCHAR(10) NOT NULL UNIQUE, 
    Patient_Insurance VARCHAR(50) NOT NULL,
    Update_At DATETIME NOT NULL DEFAULT GETDATE(),
);
GO

CREATE TABLE Doctors (
    Doctor_Id INT PRIMARY KEY IDENTITY(1,1),
    Doctor_Name VARCHAR(50) NOT NULL,
    Doctor_Dni VARCHAR(10) NOT NULL UNIQUE,
    Specialty_Id INT NOT NULL,
    Update_At DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (Specialty_Id) REFERENCES Specialties(Specialty_Id) ON DELETE CASCADE
);
GO

CREATE TABLE Medical_Attention (
    Attention_Id INT PRIMARY KEY IDENTITY(1,1),
    Patient_Id INT NOT NULL,
    Doctor_Id INT NOT NULL,
    Diagnosis TEXT NOT NULL,
    Admission_Date DATETIME NOT NULL DEFAULT GETDATE(),
    Discharge_Date DATETIME NULL,
    Update_At DATETIME NOT NULL DEFAULT GETDATE(),
    FOREIGN KEY (Patient_Id) REFERENCES Patients(Patient_Id) ON DELETE CASCADE,
    FOREIGN KEY (Doctor_Id) REFERENCES Doctors(Doctor_Id) ON DELETE CASCADE
);

GO
INSERT INTO Specialties (Specialty_Name)
VALUES 
    ('Cardiología'),
    ('Pediatría'),
    ('Traumatología'),
    ('Dermatología'),
    ('Neurología');
GO
INSERT INTO Patients (Patient_Name, Patient_Dni, Patient_Insurance)
VALUES 
    ('Ana María González', '15678432-1', 'Fonasa'),
    ('Carlos Alberto Muñoz', '17234567-8', 'Isapre Más Vida'),
    ('Patricia Andrea Soto', '14987654-2', 'Consalud'),
    ('Juan Pablo Martínez', '16543219-K', 'Fonasa'),
    ('María José Hernández', '18765432-9', 'Isapre Cruz Blanca');
GO
INSERT INTO Doctors (Doctor_Name, Doctor_Dni, Specialty_Id)
VALUES 
    ('Dr. Juan García Pérez', '12345678-9', 1), 
    ('Dra. Carmen Silva Rojas', '13456789-0', 2),
    ('Dr. Roberto Vega Mora', '14567890-1', 3),  
    ('Dra. Laura Pinto Sáez', '15678901-2', 4), 
    ('Dr. Miguel Ángel Ruiz', '16789012-3', 5);
GO
INSERT INTO Medical_Attention (Patient_Id, Doctor_Id, Diagnosis, Admission_Date, Discharge_Date)
VALUES 
    (1, 1, 'Hipertensión arterial controlada', '2024-03-15 09:00:00', '2024-03-15 09:45:00'),
    (2, 2, 'Faringitis aguda', '2024-03-16 10:30:00', '2024-03-16 11:00:00'),
    (3, 3, 'Tendinitis rotuliana', '2024-03-17 11:15:00', '2024-03-17 12:00:00'),
    (4, 4, 'Dermatitis atópica', '2024-03-18 14:30:00', '2024-03-18 15:00:00'),
    (5, 5, 'Migraña con aura', '2024-03-19 16:00:00', '2024-03-19 16:45:00'),
    (1, 3, 'Lumbalgia aguda', '2024-03-20 08:30:00', '2024-03-20 09:15:00'),
    (2, 4, 'Acné vulgar moderado', '2024-03-21 13:00:00', '2024-03-21 13:30:00');   
GO  

CREATE TRIGGER update_timestamp_specialties
ON Specialties
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Actualizar el campo Update_At en la tabla Specialties
    UPDATE Specialties
    SET Update_At = GETDATE()
    FROM Specialties s
    INNER JOIN inserted i ON s.Specialty_Id = i.Specialty_Id;
END;
GO

CREATE TRIGGER update_timestamp_patients
ON Patients
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

     -- Actualizar el campo Update_At en la tabla Patients
    UPDATE Patients
    SET Update_At = GETDATE()
    WHERE Patient_Id IN (SELECT Patient_Id FROM inserted);
END;
GO

CREATE TRIGGER update_timestamp_doctors
ON Doctors
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Actualizar el campo Update_At en la tabla Doctors
    UPDATE Doctors
    SET Update_At = GETDATE()
    WHERE Doctor_Id IN (SELECT Doctor_Id FROM inserted);
END;

GO
CREATE TRIGGER update_timestamp_medical_attention
ON Medical_Attention
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Actualizar el campo Update_At en la tabla Medical_Visits
    UPDATE Medical_Attention
    SET Update_At = GETDATE()
    WHERE Attention_Id IN (SELECT Attention_Id FROM inserted);
END
GO
-- Procedimiento para insertar datos en la tabla Medical_Atention 
    
CREATE PROCEDURE AddMedicalAtention
    @Patient_Id INT,
    @Doctor_Id INT,
    @Diagnosis TEXT,
    @Discharge_Date DATETIME = NULL -- Opcional, puede ser nulo
AS
BEGIN
-- Insertar los datos en la tabla Medical_Attention
    INSERT INTO Medical_Attention 
    (
        Patient_Id, 
        Doctor_Id, 
        Diagnosis, 
        Admission_Date, 
        Discharge_Date, 
        Update_At
    )
    VALUES 
    (
        @Patient_Id, 
        @Doctor_Id, 
        @Diagnosis, 
        GETDATE(), -- Fecha de admisión predeterminada
        @Discharge_Date, 
        GETDATE()  -- Fecha de actualización predeterminada
    );
END;
GO

-- Vista detalle de atenciones médicas
CREATE VIEW MedicalAttentionView AS
SELECT 
    ma.Attention_Id,
    ma.Admission_Date,
    p.Patient_Name,
    d.Doctor_Name,
    s.Specialty_Name,
    ma.Diagnosis,
    ma.Discharge_Date
FROM 
    Medical_Attention ma
INNER JOIN Patients p ON ma.Patient_Id = p.Patient_Id
INNER JOIN Doctors d ON ma.Doctor_Id = d.Doctor_Id
INNER JOIN Specialties s ON d.Specialty_Id = s.Specialty_Id;
GO

CREATE VIEW DoctorsView AS
SELECT
	d.Doctor_Id,
	d.Doctor_Name,
	s.Specialty_Name
FROM
	Doctors d 
INNER JOIN Specialties s ON d.Specialty_Id = s.Specialty_Id;
GO

-- Procedimiento para actualizar
CREATE PROCEDURE UpdateMedicalAttention
    @AttentionId INT,
    @PatientId INT,
    @DoctorId INT,
    @Diagnosis TEXT,
    @DischargeDate DATETIME NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    UPDATE Medical_Attention
    SET 
        Patient_Id = @PatientId, 
        Doctor_Id = @DoctorId, 
        Diagnosis = @Diagnosis, 
        Discharge_Date = @DischargeDate, 
        Update_At = GETDATE()
    WHERE Attention_Id = @AttentionId;
END;
GO