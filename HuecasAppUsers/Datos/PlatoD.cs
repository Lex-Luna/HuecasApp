using Firebase.Database.Query;
using Firebase.Storage;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuecasAppUsers.Datos
{
    public class PlatoD
    {
        #region Variables
        public string _IdPLato;
        public string rutafoto;
        #endregion
        #region Insertar
        public async Task<string> InserPlato(PlatoM parametros)
        {
            try
            {
                var data = await Constantes.firebase.Child("Plato")
                .PostAsync(new PlatoM()
                {
                    Comentario = parametros.Comentario,
                    Descripcion = parametros.Descripcion,
                    FotoPlato = parametros.FotoPlato,
                    Nombre = parametros.Nombre,
                    Precio = parametros.Precio,
                });
                _IdPLato = data.Key;
                return _IdPLato;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
        #endregion
        #region Mostrar


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
                    Precio = item.Object.Precio,
                    FotoPlato = item.Object.FotoPlato

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

        #endregion

        #region Multimedia
        public async Task<string> SubirFotoPlato(Stream imagen, string nombreplato)
        {

            var storageImagen = await new FirebaseStorage("huecasapp-d8da1.appspot.com")
                .Child("Plato")
                .Child(nombreplato + ".jpg")
                .PutAsync(imagen);
            rutafoto = storageImagen;
            return rutafoto;
        }
        #endregion
    }
}
