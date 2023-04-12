using Firebase.Database.Query;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Modelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuecasAppUsers.Datos
{
    public class EncuestaD
    {
        public string IdEncuesta;
        public async Task <string> InsertarEncuesta(EncuestaM parametros)
        {
            var data = await Constantes.firebase.Child("Encuesta")
                .PostAsync(new EncuestaM()
                {

                    Estado = parametros.Estado,
                    FechaData = parametros.FechaData,
                    IdCalificacion = parametros.IdCalificacion,
                    IdPlatoLocal = parametros.IdPlatoLocal,
                    IdUsuario = parametros.IdUsuario,
                    NomUsuario = parametros.NomUsuario,
                    NomLocal = parametros.NomLocal,
                    NomPlato = parametros.NomPlato,
                    PromCalificacion = parametros.PromCalificacion
                }) ;
            IdEncuesta = data.Key;
            return IdEncuesta;
        }

        public async Task<List<EncuestaM>> MostEncuesta()
        {
            return (await Constantes.firebase
                .Child("Encuesta")
                .OnceAsync<EncuestaM>()).Select(item => new EncuestaM
                {


                    IdEncuesta = item.Key,
                    Estado = item.Object.Estado,
                    FechaData = item.Object.FechaData,
                    IdCalificacion = item.Object.IdCalificacion,
                    IdPlatoLocal = item.Object.IdPlatoLocal,
                    IdUsuario = item.Object.IdUsuario,
                    NomUsuario = item.Object.NomUsuario,
                    NomLocal = item.Object.NomLocal,
                    NomPlato = item.Object.NomPlato,
                    PromCalificacion = item.Object.PromCalificacion
                }).ToList();
        }

        /*public async Task<List<UsuarioM>> MostUsuarioXcorreo(UsuarioM p)
        {
            return (await Constantes.firebase
                .Child("Usuario")
                .OnceAsync<UsuarioM>()).Where(a => a.Object.Correo == p.Correo).Select(item => new UsuarioM
                {
                    IdUsuario = item.Key,
                    Apellido = item.Object.Apellido,
                    Contrasenia = item.Object.Contrasenia,
                    Correo = item.Object.Correo,
                    Estado = item.Object.Estado,
                    IdAdministrador = item.Object.IdAdministrador,
                    Nombre = item.Object.Nombre
                }).ToList();
        }*/

        public async Task<List<EncuestaM>> MostEncuestaIdUser(string p)
        {
            return (await Constantes.firebase
                .Child("Encuesta")
                .OnceAsync<EncuestaM>())
                .Where(a => a.Object.IdUsuario == p)
                .OrderByDescending(a => a.Object.FechaData)
                .Select(item => new EncuestaM
                {
                    IdEncuesta = item.Key,
                    Estado = item.Object.Estado,
                    FechaData = item.Object.FechaData,
                    IdCalificacion = item.Object.IdCalificacion,
                    IdPlatoLocal = item.Object.IdPlatoLocal,
                    IdUsuario = item.Object.IdUsuario,
                    NomUsuario = item.Object.NomUsuario,
                    NomLocal = item.Object.NomLocal,
                    NomPlato = item.Object.NomPlato,
                    PromCalificacion = item.Object.PromCalificacion
                })
                .ToList();
        }



    }
}
