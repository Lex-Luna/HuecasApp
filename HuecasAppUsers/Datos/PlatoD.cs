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
    public class PlatoD
    {
        public string _IdPLato;
        public async Task<string> InserPlato(PlatoM parametros)
        {
            var data =await Constantes.firebase.Child("Plato")
                .PostAsync(new PlatoM()
                {
                    Comentario = parametros.Comentario,
                    Descripcion = parametros.Descripcion,
                    Nombre = parametros.Nombre,
                    Precio = parametros.Precio,
                });
            _IdPLato = data.Key;
            return _IdPLato;
        }


        public async Task<List<PlatoM>> MostPlato()
        {
            return (await Constantes.firebase
                .Child("Plato")
                .OnceAsync<PlatoM>()).Select(item => new PlatoM
                {

                    IdPlato = item.Key,
                    Comentario = item.Object.Comentario,
                    Descripcion = item.Object.Descripcion,
                    Nombre = item.Object.Nombre,
                    Precio = item.Object.Precio

                }).ToList();
        }

        public async Task<List<PlatoM>> MostPlatoXId(string p)
        {
            var plato = await Constantes.firebase
                .Child("Plato")
                .Child(p)
                .OnceSingleAsync<PlatoM>();

            return new List<PlatoM> { plato };
        }
    }
}
