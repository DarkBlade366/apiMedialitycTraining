# 📘 Proyecto Capacitación Backend .NET – API REST

## 📌 Descripción General

Este proyecto corresponde al **Desafío Backend .NET** de capacitación, cuyo objetivo es desarrollar una **API REST profesional** utilizando **.NET 9** y **FastEndpoints**, aplicando buenas prácticas de arquitectura, diseño y seguridad.

La API permite la gestión de:

- Usuarios
- Recursos (Vehículos)
- Reservas (Ventas)
- Reportes (1 en vehículo y otro en venta)

El foco del proyecto no es únicamente que la solución funcione, sino demostrar **criterio técnico**, **estructura limpia** y **separación clara de responsabilidades**.

---

## Tecnologías Utilizadas

- **.NET 9**
- **FastEndpoints**
- **Entity Framework Core**
- **Base de datos relacional:** <<PostgreSQL>>
- **JWT (JSON Web Tokens)** para autenticación
- **Swagger** para documentación de la API
- **BCrypt** para hasheo de contraseñas

---

## Arquitectura

### Patrón elegido: **CQRS (Command Query Responsibility Segregation)**

El proyecto utiliza el patrón **CQRS**, separando explícitamente:

- **Commands** → Operaciones que modifican el estado del sistema
- **Queries** → Operaciones de lectura

#### Justificación

- Permite una separación clara entre lectura y escritura
- Facilita el mantenimiento y la escalabilidad
- Evita mezclar lógica de negocio con lógica de consulta
- Se integra de forma natural con FastEndpoints y Handlers

Este patrón se aplica de forma **consistente en todo el proyecto**.

---
## Módulo de Auth

### Funcionalidades

- Registrarse como nuevo usuario
- Logearse como usuario activo (usuario inactivo tiene que ser activado antes de logearse)

---

## Módulo de Usuarios

### Funcionalidades

- Crear usuarios (solo admin)
- Obtener usario por Id (solo admin)
- Obtener usuario segun paginacion (solo admin)
- Cambiar estado de un usuario (solo admin)
- Añadir o eliminar correos y telefonos, si tiene solo 1 no se puede eliminar (admin y user)
- Revisar perfil (admin y user)
- Actualizar nombre (admin y user)

### Relaciones

- Usuario → Correos electrónicos (1:N)
- Usuario → Teléfonos (1:N)

📌 **No se permite borrado físico**, solo desactivación lógica.

---

## Módulo de Recursos (Vehículos)

### Funcionalidades

- Crear vehiculo (solo admin)
- Borrar vehiculo si no esta vendido o pediente (solo admin)
- Buscar vehiculo por id (solo admin)
- Actualizar vehiculo (solo admin)
- Lista de vehiculos segun paginacion (admin y user, el admin puede ve los `Pending` y `Sold`, el user solo `Available`)
- Consulta global del estado de los inventarios (solo admin)

### Inventario

Cada vehículo cuenta con un inventario asociado que agrupo los autos segun elementos comunes
El **estado del inventario** se calcula dinámicamente en base a:

- Cantidad disponible
- Ventas pendientes
- Ventas completadas

Estados posibles del vehiculo:

- `Available`
- `Pending`
- `Sold`

---

## Módulo de Ventas / Reservas

### Funcionalidades

- Crear venta con estado inicial `Pending` (admin y user)
- Confirmar venta pasar el estado a `Completed` (solo admin)
- Cancelar venta pasar venta a `Cancelled` (admin y user)
- Consultar ventas propias (admin y user)
- Consultar ventas globales (solo admin)
- Obtener venta por ID (admin y user, el user solo los disponibles y el admin todos)

### Reglas de Negocio

- No se pueden completar ventas ya canceladas
- No se pueden cancelar ventas completadas
- Al crear una venta:
  - Se valida la disponibilidad del vehículo
  - Se descuenta del inventario
- Al cancelar una venta:
  - Se devuelve la cantidad al inventario
- Usuarios inactivos no pueden realizar ventas

---

## Paginación y Filtros

Todos los endpoints de listado implementan **paginación obligatoria**.

### Request

- `Page`
- `PageSize`

### Response

- `Items`
- `Page`
- `PageSize`
- `TotalItems`
- `TotalPages`
- `HasNext`
- `HasPrevious`

📌 **La paginación se realiza a nivel de base de datos**, no en memoria.

Los filtros se aplican directamente en las queries (estado, fechas, tipo, usuario).

---

## 🔐 Seguridad

### Autenticación

- Autenticación mediante **JWT**
- Tokens firmados con clave segura
- Claims incluidos:
  - UserId
  - Role

### Autorización

Roles disponibles:

- `Admin`
- `User`

Cada endpoint define explícitamente los roles permitidos.

---

## Documentación

- Swagger habilitado
- Endpoints documentados automáticamente con FastEndpoints
- Seguridad JWT integrada en Swagger

---

## ▶️ Cómo ejecutar el proyecto

1. Clonar el repositorio:

```bash
git clone <<https://github.com/DarkBlade366/apiMedialitycTraining.git>>
