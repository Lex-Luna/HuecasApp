using Firebase.Database.Query;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Modelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Shapes;

namespace HuecasAppUsers.Datos
{
    public class EncuestaD
    {
        
        public string IdEncuesta;
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
                    TotalEncuesta = parametros.TotalEncuesta

                });
            IdEncuesta = data.Key;
            return IdEncuesta;
        }

        public async Task<List<EncuestaM>> MostEncuesta()
        {
            try
            {
                var result = await Constantes.firebase.Child("Encuesta").OnceAsync<EncuestaM>();
                return result
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
                    })
                    .OrderByDescending(x => x.FechaData)
                    .ToList();
            }
            catch (Exception e)
            {

                throw e;
            }
            
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
                    PromCalificacion = item.Object.PromCalificacion
                })
                .ToList();
        }


    }
}
