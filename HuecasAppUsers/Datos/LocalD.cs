﻿using Firebase.Database.Query;
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
        public string _IdLocal;
        public async Task <string> InsertarLocal(LocalM parametros)
        {
            var data = await Constantes.firebase.Child("Local")
                .PostAsync(new LocalM()
                {
                    Barrio = parametros.Barrio,
                    Direccion = parametros.Direccion,
                    FotoFachada = parametros.FotoFachada,
                    //Geolocalizacion = parametros.Geolocalizacion,
                    NombreLocal = parametros.NombreLocal,
                    IdPais = parametros.IdPais,
                    IdCiudad= parametros.IdCiudad,
                    IdLocal = parametros.IdLocal
                });
            _IdLocal = data.Key;
            return _IdLocal;
        }

        public async Task<string> SubirFotoFachada(Stream image, string identificacion)
        {
            string rutafoto;
            var storageImagen = await new FirebaseStorage("huecasapp-d8da1.appspot.com")
                .Child("Local")
                .Child(identificacion + ".jpg")
                .PutAsync(image);
            rutafoto = storageImagen;
            return rutafoto;
        }


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
                    NombreLocal = item.Object.NombreLocal,
                    IdPais = item.Object.IdPais


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

    }
}
