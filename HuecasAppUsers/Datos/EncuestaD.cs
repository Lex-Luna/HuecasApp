using Firebase.Database.Query;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Modelo;
using System;
using System.Collections.Generic;
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
            var data =await Constantes.firebase.Child("Encuesta")
                .PostAsync(new EncuestaM()
                {

                    Estado = parametros.Estado,
                    FechaEncuesta = parametros.FechaEncuesta,
                    IdCalificacion = parametros.IdCalificacion,
                    IdPlatoLocal = parametros.IdPlatoLocal,
                    IdUsuario = parametros.IdUsuario,
                    NomUsuario = parametros.NomUsuario,
                    ApellUsuario = parametros.ApellUsuario
                });
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
                    FechaEncuesta = item.Object.FechaEncuesta,
                    IdCalificacion = item.Object.IdCalificacion,
                    IdPlatoLocal = item.Object.IdPlatoLocal,
                    IdUsuario = item.Object.IdUsuario

                }).ToList();
        }
    }
}
