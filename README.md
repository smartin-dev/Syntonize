# Servicio de Gestión de Usuarios Syntonize

## Requisitos Previos
- Visual Studio con soporte para .NET Framework 4.8
- Postman o cualquier otra herramienta de prueba de API

## Pasos de Instalación

1. **Clonar el Repositorio**: 
   ```bash
   git clone https://github.com/smartin-dev/Syntonize.git
   ```

2. **Abrir la Solución en Visual Studio**:
   - Navega hasta el directorio del repositorio clonado.
   - Abre el archivo de solución (`*.sln`) en Visual Studio.

3. **Configurar la Cadena de Conexión y Migrar la base de datos**:
   - Abre el archivo `Web.config`.
   - Localiza la sección de cadena de conexión.
   - Actualiza la cadena de conexión con los detalles de tu base de datos.
   - Ejecuta el comando Update-Database para aplicar las migraciones y crear la base de datos con las últimas modificaciones de esquema.

4. **Compilar y Ejecutar el Proyecto**:
   - Compila la solución en Visual Studio.
   - Ejecuta el proyecto presionando F5 o haciendo clic en el botón "Start".

5. **Endpoints de la API**:
   - Accede a los siguientes endpoints de la API para probar la funcionalidad:

## Endpoints de la API

### Autenticación

- **POST** `/api/auth/signup`: 
  - Descripción: Regístrate para crear una nueva cuenta de usuario.

- **POST** `/api/auth/login`: 
  - Descripción: Inicia sesión para obtener un token de autenticación.

### Usuarios
- **GET** `/api/users?language={language}`:
  - Descripción: Recupera todos los usuarios.

- **GET** `/api/user/{id}?language={language}`:
  - Descripción: Recupera un usuario por ID.

- **POST** `/api/user?language={language}`:
  - Descripción: Crea un nuevo usuario.

- **PUT** `/api/user/{id}?language={language}`:
  - Descripción: Actualiza un usuario existente.

- **DELETE** `/api/user/{id}?language={language}`:
  - Descripción: Elimina un usuario por ID.


## Pruebas de las APIs
- Utiliza Postman o cualquier otra herramienta de prueba de API para realizar solicitudes a los endpoints mencionados anteriormente.
- Sigue la documentación de la API proporcionada para entender la carga útil de la solicitud y el formato de respuesta para cada endpoint.

## Notas Adicionales
- Reemplazar `{language}` con el código de idioma deseado en las URL de las solicitudes. Los idiomas disponibles actualmente son español con código "es" e inglés con código "en"
- Para la autenticación basada en tokens, obtén un token haciendo una solicitud a `/api/auth/login` e incluye el token en el encabezado de Autorización para las solicitudes posteriores.

