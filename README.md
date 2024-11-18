# Sistema de Gesti�n M�dica

## Introducci�n

El **Sistema de Gesti�n M�dica** es una aplicaci�n desarrollada en **C#** utilizando Windows Forms y el framework .NET en su versi�n 4.7.2. Este sistema permite gestionar la informaci�n de atenciones m�dicas a trav�s de una interfaz gr�fica amigable e intuitiva. Entre sus principales caracter�sticas, permite visualizar, agregar, editar y eliminar atenciones m�dicas, integr�ndose con una base de datos SQL Server para el almacenamiento de informaci�n.

La base de datos incluye las siguientes tablas principales:
- **Pacientes**
- **M�dicos**
- **Especialidades**
- **Atenciones M�dicas**

El sistema tambi�n cuenta con controles avanzados como **DataGridView** y **ComboBox** para interactuar de forma eficiente con los datos.

## Tecnolog�as Utilizadas

- **Lenguaje de programaci�n:** C#
- **Framework:** .NET 4.7.2
- **IDE:** Visual Studio 2022
- **Base de datos:** SQL Server
- **Interfaz gr�fica:** Windows Forms

## Dependencias principales

- **System.Data.SqlClient**: Para la conexi�n y manejo de datos con SQL Server.
- **Windows Forms**: Framework de desarrollo para la interfaz gr�fica.

## Funcionalidades principales

1. **Gesti�n de Atenciones M�dicas**

- Visualizar la lista de atenciones m�dicas en un **DataGridView**.
- Agregar nuevas atenciones m�dicas.
- Editar registros existentes.
- Eliminar registros de atenciones m�dicas.

2. **Carga din�mica de datos**

- Mostrar la lista de m�dicos y pacientes registrados en la base de datos mediante **ComboBox**.
- Integraci�n con SQL Server para cargar datos autom�ticamente.

3. **Interfaz intuitiva**

- Navegaci�n simplificada para gestionar informaci�n.
- Validaciones para evitar datos incompletos o incorrectos.

## Gu�a de instalaci�n

### Requisitos previos

1. **Herramientas de desarrollo**:

- Visual Studio 2022 o superior.
- Framework .NET 4.7.2 instalado.

2. **Base de datos**:

- SQL Server configurado con las tablas `Patients`, `Doctors`, `Specialties` y `Medical_Attention`.

3. **Dependencias**:

- Aseg�rate de tener instalados los paquetes necesarios mediante el administrador de paquetes NuGet en Visual Studio.

### Pasos de instalaci�n

1. **Clonar o descargar el proyecto**:

- Descarga o clona este repositorio en tu m�quina local.

2. **Configurar la base de datos**:
   
- Restaura la base de datos desde el script proporcionado en la carpeta `Database` o configura manualmente las tablas mencionadas.

3. **Actualizar cadena de conexi�n**:

- En el archivo `ConexionBD.cs`, actualiza la cadena de conexi�n para que apunte a tu instancia local de SQL Server.

### Contribuciones

Las contribuciones son bienvenidas. Por favor, abre un issue para discutir los cambios propuestos antes de realizar un pull request.

### Contacto

- LinkedIn: Elias Celis

- Correo electr�nico: zelys.dev@gmail.com