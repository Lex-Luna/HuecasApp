using Firebase.Auth;
using Firebase.Database.Query;
using Firebase.Storage;
using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Modelo;
using HuecasAppUsers.VistaModelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace HuecasAppUsers.Datos
{
    public class UsuarioD
    {
        #region Variables
        string rutafoto;
        public string _IdUsuario;
        #endregion
        #region Insertar
        public async Task<string> InserUsuario(UsuarioM parametros)
        {
            try
            {
                var data = await Constantes.firebase
               .Child("Usuario")
               .PostAsync(new UsuarioM()
               {
                   Admin = parametros.Admin,
                   FotoUsuario = parametros.FotoUsuario,
                   Apellido = parametros.Apellido,
                   Contrasenia = parametros.Contrasenia,
                   Correo = parametros.Correo,
                   Estado = true,
                   Nombre = parametros.Nombre,
                   NumEncuesta = parametros.NumEncuesta,
                   EncuestasEliminadas = parametros.EncuestasEliminadas,
                   IdAdministrador = "",
                   IdUsuario = parametros.IdUsuario,
               });
                _IdUsuario = data.Key;
                return _IdUsuario;
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        
        #endregion
        #region Actualizar
        public async Task AddNumEncuesta(UsuarioM p)
        {
            var obtenerData = (await Constantes.firebase
                .Child("Usuario")
                .OnceAsync<UsuarioM>()).Where(a => a.Object.Correo == p.Correo)
                .FirstOrDefault();

            var usuario = obtenerData.Object;
            usuario.NumEncuesta++;

            await Constantes.firebase
               .Child("Usuario")
               .Child(obtenerData.Key)
               .PutAsync(usuario);
        }
        public async Task AddNumVaneoEncuesta(UsuarioM p)
        {
            try
            {


                var obtenerData = (await Conexiones.Constantes.firebase
                    .Child("Usuario")
                    .OnceAsync<UsuarioM>()).Where(a => a.Key == p.IdUsuario)
                    .FirstOrDefault();

                var usuario = obtenerData.Object;
                usuario.EncuestasEliminadas++;

                await Conexiones.Constantes.firebase
                   .Child("Usuario")
                   .Child(obtenerData.Key)
                   .PutAsync(usuario);
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        public async Task UsuarioVaneado(UsuarioM p)
        {
            try
            {



                var obtenerData = (await Conexiones.Constantes.firebase
                    .Child("Usuario")
                    .OnceAsync<UsuarioM>()).Where(a => a.Key == p.IdUsuario)
                    .FirstOrDefault();

                var usuario = obtenerData.Object;
                usuario.Estado = false;

                await Conexiones.Constantes.firebase
                   .Child("Usuario")
                   .Child(obtenerData.Key)
                   .PutAsync(usuario);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task EncuestaVaneada(UsuarioM p)
        {
            try
            {
                var obtenerData = (await Conexiones.Constantes.firebase
                    .Child("Usuario")
                    .OnceAsync<UsuarioM>()).Where(a => a.Key == p.IdUsuario)
                    .FirstOrDefault();

                if (obtenerData != null)
                {
                    var encuesta = obtenerData.Object;
                    encuesta.Estado = false;

                    await Conexiones.Constantes.firebase
                       .Child("Usuario")
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
        #region Mostrar
        


        public async Task<List<UsuarioM>> MostUsuario()
        {
            var usuarios = (await Constantes.firebase
                .Child("Usuario")
                .OnceAsync<UsuarioM>()).Select(item => new UsuarioM
                {
                    IdUsuario = item.Key,
                    Admin = item.Object.Admin,
                    Apellido = item.Object.Apellido,
                    Contrasenia = item.Object.Contrasenia,
                    FotoUsuario = item.Object.FotoUsuario,
                    Correo = item.Object.Correo,
                    Estado = item.Object.Estado,
                    IdAdministrador = item.Object.IdAdministrador,
                    Nombre = item.Object.Nombre,
                    NumEncuesta = item.Object.NumEncuesta ,
                    EncuestasEliminadas = item.Object.EncuestasEliminadas

                }).ToList();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(usuarios);
            return usuarios;
        }
        public async Task<List<UsuarioM>> MostUsuarioXcorreo(UsuarioM p)
        {
            var useXcorreo= (await Constantes.firebase
                .Child("Usuario")
                .OnceAsync<UsuarioM>())
                .Where(a => a.Object.Correo == p.Correo && a.Object.Estado == true)
                .Select(item => new UsuarioM
                {
                    IdUsuario = item.Key,
                    Admin = item.Object.Admin,
                    FotoUsuario = item.Object.FotoUsuario,
                    Apellido = item.Object.Apellido,
                    Contrasenia = item.Object.Contrasenia,
                    Correo = item.Object.Correo,
                    Estado = item.Object.Estado,
                    IdAdministrador = item.Object.IdAdministrador,
                    Nombre = item.Object.Nombre,
                    NumEncuesta = item.Object.NumEncuesta

                }).ToList();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(useXcorreo);
            return useXcorreo;
        }


        public async Task<List<UsuarioM>> MostUsuarioXId(string p)
        {
            var usuario = await Constantes.firebase
                .Child("Usuario")
                .Child(p)
                .OnceSingleAsync<UsuarioM>();
            return new List<UsuarioM> { usuario};
        }

        #endregion
        

        #region Multimedia

        public async Task<string> SubirFotoUser(Stream imagen, string nombreUsuario)
        {

            var storageImagen = await new FirebaseStorage("huecasapp-d8da1.appspot.com")
                .Child("Usuarios")
                .Child(nombreUsuario + ".jpg")
                .PutAsync(imagen);
            rutafoto = storageImagen;
            return rutafoto;
        }

        #endregion



    }
}
