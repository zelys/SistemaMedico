# Sistema de Gestión Médica

## Introducción

El **Sistema de Gestión Médica** es una aplicación desarrollada en **C#** utilizando Windows Forms y el framework .NET en su versión 4.7.2. Este sistema permite gestionar la información de atenciones médicas a través de una interfaz gráfica amigable e intuitiva. Entre sus principales características, permite visualizar, agregar, editar y eliminar atenciones médicas, integrándose con una base de datos SQL Server para el almacenamiento de información.

La base de datos incluye las siguientes tablas principales:
- **Pacientes**
- **Médicos**
- **Especialidades**
- **Atenciones Médicas**

El sistema también cuenta con controles avanzados como **DataGridView** y **ComboBox** para interactuar de forma eficiente con los datos.

## Tecnologías Utilizadas

- **Lenguaje de programación:** C#
- **Framework:** .NET 4.7.2
- **IDE:** Visual Studio 2022
- **Base de datos:** SQL Server
- **Interfaz gráfica:** Windows Forms

## Dependencias principales

- **System.Data.SqlClient**: Para la conexión y manejo de datos con SQL Server.
- **Windows Forms**: Framework de desarrollo para la interfaz gráfica.

## Funcionalidades principales

1. **Gestión de Atenciones Médicas**

- Visualizar la lista de atenciones médicas en un **DataGridView**.
- Agregar nuevas atenciones médicas.
- Editar registros existentes.
- Eliminar registros de atenciones médicas.

2. **Carga dinámica de datos**

- Mostrar la lista de médicos y pacientes registrados en la base de datos mediante **ComboBox**.
- Integración con SQL Server para cargar datos automáticamente.

3. **Interfaz intuitiva**

- Navegación simplificada para gestionar información.
- Validaciones para evitar datos incompletos o incorrectos.

## Guía de instalación

### Requisitos previos

1. **Herramientas de desarrollo**:

- Visual Studio 2022 o superior.
- Framework .NET 4.7.2 instalado.

2. **Base de datos**:

- SQL Server configurado con las tablas `Patients`, `Doctors`, `Specialties` y `Medical_Attention`.

3. **Dependencias**:

- Asegúrate de tener instalados los paquetes necesarios mediante el administrador de paquetes NuGet en Visual Studio.

### Pasos de instalación

1. **Clonar o descargar el proyecto**:

- Descarga o clona este repositorio en tu máquina local.

2. **Configurar la base de datos**:
   
- Restaura la base de datos desde el script proporcionado en la carpeta `Database` o configura manualmente las tablas mencionadas.

3. **Actualizar cadena de conexión**:

- En el archivo `ConexionBD.cs`, actualiza la cadena de conexión para que apunte a tu instancia local de SQL Server.

### Contribuciones

Las contribuciones son bienvenidas. Por favor, abre un issue para discutir los cambios propuestos antes de realizar un pull request.

### Contacto

- LinkedIn: Elias Celis

- Correo electrónico: zelys.dev@gmail.com