# ApiLibreriaNeoris
Api de Libreria, Crud de autores, editorial, libros, visualizacion de usuarios.
Autenticacion por token Jwt.
Seguridad de los endpoints por authorizacion y nivel de los roles.
Documentacion con Swagger
Conexion con sql server

Arquitectura Hexagonal
Patrón de diseño Repository


Ya existe dos usuarios creados por data semilla y tienen asignados los roles y permisos

1. usuario administrador con todos los permisos { "userName": "admin@gmail.com", "password": "admin" }

2. usuario estandar con los permisos para consultar excepto los usuarios { "userName": "estandar@gmail.com", "password": "estandar" }

