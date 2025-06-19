# Proyecto Capas – Transformación de Datos y API REST

Este proyecto fue desarrollado con .NET Core y SQL Server, siguiendo principios de arquitectura por capas y buenas prácticas como SOLID, validaciones robustas y manejo adecuado de errores. Su objetivo es procesar archivos CSV, transformarlos y exponerlos mediante una API REST, además de integrar una funcionalidad de envío de correos usando SendGrid.

## 🧩 Funcionalidades

### 1. Transformación de Datos desde CSV
- Lectura de archivos CSV con información de:
  - Clientes: tipo y número de identificación, nombre, dirección, teléfono, fecha de nacimiento.
  - Productos: código, nombre, descripción, stock, fecha de vencimiento.
- Transformación de datos (formato de fecha a `yyyy-MM-dd`).
- Exportación del resultado en formato JSON.

### 2. API REST con .NET Core
- Endpoints para registrar, actualizar y listar clientes y productos.
- Validaciones:
  - Datos requeridos (ej. nombre no vacío, stock mayor a cero).
  - Respuestas claras en caso de error.
- Documentación automática generada con Swagger.

### 3. Reintentos y Logs
- Implementación de política de reintentos con backoff exponencial.
- Simulación de fallos para pruebas.
- Logs descriptivos para trazabilidad de errores.

### 4. Integración con SendGrid
- Envío de correos electrónicos con API Key configurada.
- Uso del SDK oficial de SendGrid.

## 🧪 Tecnologías y Herramientas
- .NET Core
- SQL Server
- CsvHelper
- Newtonsoft.Json
- SendGrid SDK
- Swagger / Swashbuckle
- Serilog (o similar para logs)

## 🗂️ Estructura del Proyecto
- `Domain`: Entidades y contratos
- `Application`: Lógica de negocio, DTOs y servicios
- `Infrastructure`: Acceso a datos y servicios externos
- `Presentation`: Controladores y configuración de la API

## 🚀 Configuración
1. Clona el repositorio:
   ```bash
   git clone https://github.com/carsepe/proyecto_ol_capas.git
