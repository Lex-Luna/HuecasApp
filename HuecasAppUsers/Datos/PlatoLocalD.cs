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
    
        public class PlatoLocalD
        {
            string _IdPlatoLocal;
            public async Task<string> InserPlatoLocal(PlatoLocalM parametros)
            {
                
            try
            {
                var data = await Constantes.firebase.Child("PlatoLocal")
                    .PostAsync(new PlatoLocalM()
                    {
                        IdLocal = parametros.IdLocal,
                        IdPLato = parametros.IdPLato
                    });
                _IdPlatoLocal = data.Key;
                return _IdPlatoLocal;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            }

            public async Task<List<PlatoLocalM>> MostrarPlatoLocal()
            {
                return (await Constantes.firebase
                    .Child("PlatoLocal")
                    .OnceAsync<PlatoLocalM>()).Select(item => new PlatoLocalM
                    {

                        IdPlatoLocal = item.Key,
                        IdLocal = item.Object.IdLocal,
                        IdPLato = item.Object.IdPLato,

                    }).ToList();
            }
        }

    
}
