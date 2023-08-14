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
    public class CalificacionD
    {
        string _IdCalificacion;
        public async Task <string> InserCalificacion(CalificacionM parametros)
        {
            var data=await Constantes.firebase.Child("Calificacion")
                .PostAsync(new CalificacionM()
                {
                    CalificacionAtencion = parametros.CalificacionAtencion,
                    CalificacionComida = parametros.CalificacionComida,
                    CalificacionLugar = parametros.CalificacionLugar,
                    Recomendacion = parametros.Recomendacion,
                    IdPlatoLocal = parametros.IdPlatoLocal

                });
            _IdCalificacion = data.Key;
            return _IdCalificacion;
        }

        public async Task<List<CalificacionM>> MostCalificacion()
        {
            return (await Constantes.firebase
                .Child("Calificacion")
                .OnceAsync<CalificacionM>()).Select(item => new CalificacionM
                {

                    IdCalificacion = item.Key,
                    CalificacionAtencion = item.Object.CalificacionAtencion,
                    CalificacionComida = item.Object.CalificacionComida,
                    CalificacionLugar = item.Object.CalificacionLugar,
                    IdPlatoLocal = item.Object.IdPlatoLocal,
                    Recomendacion=item.Object.Recomendacion

                }).ToList();
        }


        
        public async Task<List<CalificacionM>> MostCalificacionXId(string p)
        {
            var calificacion = await Constantes.firebase
                .Child("Calificacion")
                .Child(p)
                .OnceSingleAsync<CalificacionM>();

            return new List<CalificacionM> { calificacion };
        }
    }
}

