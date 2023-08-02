using Firebase.Database.Query;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase;
using Firebase.Storage;
using System.IO;
using System.Linq;

using Firebase.Database;
using Firebase.Auth;
using Newtonsoft.Json;
using Xamarin.Essentials;
using HuecasAppUsers.VistaModelo;

namespace HuecasAppUsers.Datos
{
    public class LocalD
    {
        #region Variables
        string rutafoto;
        string rutaVideo;
        public string _IdLocal;

        #endregion

        #region Insertar
        public async Task<string> InsertarLocal(LocalM parametros)
        {
            try
            {
                var data = await Constantes.firebase.Child("Local")
              .PostAsync(new LocalM()
              {
                  Barrio = parametros.Barrio,
                  Direccion = parametros.Direccion,
                  FotoFachada = parametros.FotoFachada,
                  Geolocalizacion = parametros.Geolocalizacion,
                  NombreLocal = parametros.NombreLocal,
                  Video = parametros.Video,
                  IdPais = parametros.IdPais,
                  IdCiudad = parametros.IdCiudad,
                  Categorias = parametros.Categorias,
                  IdLocal = parametros.IdLocal
              });
                _IdLocal = data.Key;
                return _IdLocal;
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        #endregion
        #region Mostrar
        public async Task<List<LocalM>> MostLocal()
        {
            return (await Constantes.firebase
                .Child("Local")
                .OnceAsync<LocalM>()).Select(item => new LocalM
                {
                    IdLocal = item.Key,
                    IdCiudad = item.Object.IdCiudad,
                    Direccion = item.Object.Direccion,
                    FotoFachada = item.Object.FotoFachada,
                    Geolocalizacion = item.Object.Geolocalizacion,
                    Video = item.Object.Video,
                    NombreLocal = item.Object.NombreLocal,
                    IdPais = item.Object.IdPais,
                    Categorias = item.Object.Categorias
                }).ToList();
        }
        public async Task<List<LocalM>> MostLocalXId(string p)
        {
            var local = await Constantes.firebase
                .Child("Local")
                .Child(p)
                .OnceSingleAsync<LocalM>();
            return new List<LocalM> { local };
        }
        #endregion
        #region Multimedia
        public async Task<string> SubirFotoFachada(Stream imagen, string identificacion)
        {

            var storageImagen = await new FirebaseStorage("huecasapp-d8da1.appspot.com")
                .Child("Local")
                .Child(identificacion + ".jpg")
                .PutAsync(imagen);
            rutafoto = storageImagen;
            return rutafoto;
        }

        public async Task<string> SubirVideo(Stream video, string nombreLocal)
        {
            var storageVideo = await new FirebaseStorage("huecasapp-d8da1.appspot.com")
                .Child("Local")
                .Child(nombreLocal + ".mp4")
                .PutAsync(video);
            rutaVideo = storageVideo;
            return rutaVideo;
        }
        public async Task SeleccionarVideo(string nombreDelVideo)
        {
            var storage = new FirebaseStorage("huecasapp-d8da1.appspot.com");
            string rutaVideo = await storage
                .Child("Local")
                .Child(nombreDelVideo + ".mp4")
                .GetDownloadUrlAsync();
            string RutaVideoSeleccionado = rutaVideo;
        }

        #endregion





    }
}
