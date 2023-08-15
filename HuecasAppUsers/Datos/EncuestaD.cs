using Firebase.Database.Query;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Modelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms.BehaviorsPack;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Shapes;

namespace HuecasAppUsers.Datos
{
    public class EncuestaD
    {

        public string IdEncuesta;

        #region Insertar
        public async Task<string> InsertarEncuesta(EncuestaM parametros)
        {

            var data = await Constantes.firebase.Child("Encuesta")
                .PostAsync(new EncuestaM()
                {

                    Estado = parametros.Estado,
                    FechaData = parametros.FechaData,
                    IdCalificacion = parametros.IdCalificacion,
                    IdPlatoLocal = parametros.IdPlatoLocal,
                    IdPlato = parametros.IdPlato,
                    IdLocal = parametros.IdLocal,
                    IdUsuario = parametros.IdUsuario,
                    NomUsuario = parametros.NomUsuario,
                    NomLocal = parametros.NomLocal,
                    NomPlato = parametros.NomPlato,
                    PromCalificacion = parametros.PromCalificacion,
                    TotalEncuesta = parametros.TotalEncuesta,
                    Recomendado = parametros.Recomendado,
                    Categorias = parametros.Categorias,
                    Barrio = parametros.Barrio,
                    Precio = parametros.Precio
                });
            IdEncuesta = data.Key;
            return IdEncuesta;
        }
        #endregion
        #region Actualizar
        public async Task EncuestaVaneada(EncuestaM p)
        {
            try
            {
                var obtenerData = (await Conexiones.Constantes.firebase
                    .Child("Encuesta")
                    .OnceAsync<EncuestaM>()).Where(a => a.Key == p.IdEncuesta)
                    .FirstOrDefault();

                if (obtenerData != null)
                {
                    var encuesta = obtenerData.Object;
                    encuesta.Estado = false;

                    await Conexiones.Constantes.firebase
                       .Child("Encuesta")
                       .Child(obtenerData.Key)
                       .PutAsync(encuesta);
                }

            }
            catch (Exception e)
            {

                throw e;
            }

        }

        #endregion
        #region MostrarEncuesta

        public async Task<List<EncuestaM>> MostEncuesta()
        {
            return (await Constantes.firebase
                .Child("Encuesta")
                .OnceAsync<EncuestaM>())
                .Where(item => item.Object.Estado == true)
                .Select(item => new EncuestaM
                {
                    IdEncuesta = item.Key,
                    Estado = item.Object.Estado,
                    FechaData = item.Object.FechaData,
                    IdCalificacion = item.Object.IdCalificacion,
                    IdPlatoLocal = item.Object.IdPlatoLocal,
                    IdPlato = item.Object.IdPlato,
                    IdLocal = item.Object.IdLocal,
                    IdUsuario = item.Object.IdUsuario,
                    NomUsuario = item.Object.NomUsuario,
                    NomLocal = item.Object.NomLocal,
                    NomPlato = item.Object.NomPlato,
                    PromCalificacion = item.Object.PromCalificacion
                }).ToList();
        }



        public async Task<List<EncuestaM>> MostEncuestaRecomendada()
        {
            var result = await Constantes.firebase
            .Child("Encuesta")
            .OnceAsync<EncuestaM>();
            return result
                .Where(item => item.Object.Recomendado == true && item.Object.Estado == true)
                .Select(item => new EncuestaM
                {
                    IdEncuesta = item.Key,
                    Estado = item.Object.Estado,
                    FechaData = item.Object.FechaData,
                    IdCalificacion = item.Object.IdCalificacion,
                    IdPlatoLocal = item.Object.IdPlatoLocal,
                    IdPlato = item.Object.IdPlato,
                    IdLocal = item.Object.IdLocal,
                    IdUsuario = item.Object.IdUsuario,
                    NomUsuario = item.Object.NomUsuario,
                    NomLocal = item.Object.NomLocal,
                    NomPlato = item.Object.NomPlato,
                    PromCalificacion = item.Object.PromCalificacion,
                    Recomendado = item.Object.Recomendado,
                    Categorias = item.Object.Categorias,
                    Barrio = item.Object.Barrio,
                    Precio = item.Object.Precio
                })
                .OrderByDescending(x => x.FechaData)
                .ToList();

        }


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
                    IdPlato = item.Object.IdPlato,
                    IdLocal = item.Object.IdLocal,
                    IdUsuario = item.Object.IdUsuario,
                    NomUsuario = item.Object.NomUsuario,
                    NomLocal = item.Object.NomLocal,
                    NomPlato = item.Object.NomPlato,
                    PromCalificacion = item.Object.PromCalificacion,
                    Recomendado = item.Object.Recomendado,
                    Categorias = item.Object.Categorias,
                    Barrio = item.Object.Barrio,
                    Precio = item.Object.Precio
                })
                .ToList();
        }

        #endregion

    }
}
