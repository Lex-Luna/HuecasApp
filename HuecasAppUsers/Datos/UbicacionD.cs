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
    public class UbicacionD
    {
        public async Task<List<UbicacionM>> MostPais()
        {
            return (await Constantes.firebase
                .Child("Pais")
                .OnceAsync<UbicacionM>()).Select(item => new UbicacionM
                {
                    IdPais = item.Key,
                    Pais = item.Object.Pais

                }).ToList();
        }

        public async Task<List<UbicacionM>> MostCiudad()
        {
            return (await Constantes.firebase
                .Child("Ciudad")
                .OnceAsync<UbicacionM>()).Select(item => new UbicacionM
                {
                    IdCiudad = item.Key,
                    Ciudad = item.Object.Ciudad
                }).ToList();
        }
        public async Task InserPais(UbicacionM parametros)
        {
            await Constantes.firebase.Child("Pais")
                .PostAsync(new UbicacionM()
                {
                    Pais = parametros.Pais
                });
        }

        public async Task InserCiu(UbicacionM parametros)
        {
            await Constantes.firebase.Child("Ciudad")
                .PostAsync(new UbicacionM()
                {
                    Ciudad = parametros.Ciudad
                });
        }

    }
}
