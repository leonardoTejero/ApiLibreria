using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Utils.Enums
{
    public class Enums
    {
        public enum TypeState
        {
            EstadoUsuario = 1,
            EstadoLibro = 2,
        }

        public enum State
        {
            //Usuario
            UsuarioActivo = 1,
            UsuarioInactivo = 2,
            UsuarioSuspendido = 3,
        }

        public enum TypePermission
        {
            Usuarios = 1,
            Roles = 2,
            Permisos = 3,
            Libro = 4,
            Estados = 5,
            Editorial = 6,
            Autor = 7,
        }

        public enum Permission
        {
            //Usuarios
            CrearUsuarios = 1,
            ActualizarUsuarios = 2,
            EliminarUsuarios = 3,
            ConsultarUsuarios = 4,

            //Roles
            ActualizarRoles = 5,
            ConsultarRoles = 6,

            //Permisos
            ActualizarPermisos = 7,
            ConsultarPermisos = 8,
            DenegarPermisos = 9,

            //Editorial
            CrearEditorial = 10,
            ActualizarEditorial = 11,
            EliminarEditorial = 12,
            ConsultarEditorial = 13,

            //Libro
            CrearLibro=14,
            ConsultarLibros=15,
            ActualizarLibros=16,
            EliminarLibro=21,

            //Estados
            ConsultarEstados = 22,
            ActualizarEstado = 23,

            //Autor
            CrearAutor = 24,
            ActualizarAutor = 25,
            EliminarAutor = 26,
            ConsultarAutor = 27,
        }
       
        public enum RolUser
        {
            Administrador = 1,
            Estandar = 2
        }

    }
}
