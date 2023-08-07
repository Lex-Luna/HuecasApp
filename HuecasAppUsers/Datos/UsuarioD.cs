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
    public class UsuarioD
    {
        #region Variables
        string rutafoto;
        public string _IdUsuario;
        #endregion
        #region Insertar
        public async Task <string> InserUsuario(UsuarioM parametros)
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
                   IdAdministrador = "lUUpQuSwqibNTFqEq4LVQKK8kEG2"
               });
                _IdUsuario = data.Key;
                return _IdUsuario;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }
        public async Task AddNumEncuesta(UsuarioM p)
        {
            var obtenerData = (await Conexiones.Constantes.firebase
                .Child("Usuario")
                .OnceAsync<UsuarioM>()).Where(a => a.Object.Correo == p.Correo)
                .FirstOrDefault();

            var usuario = obtenerData.Object;
            usuario.NumEncuesta++;

            await Conexiones.Constantes.firebase
               .Child("Usuario")
               .Child(obtenerData.Key)
               .PutAsync(usuario);
        }
        #endregion
        #region Mostrar
        public async Task<List<UsuarioM>> MostUsuario()
        {
            return (await Constantes.firebase
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
                    NumEncuesta = item.Object.NumEncuesta

                }).ToList();
        }
        public async Task<List<UsuarioM>> MostUsuarioXcorreo(UsuarioM p)
        {
            return (await Constantes.firebase
                .Child("Usuario")
                .OnceAsync<UsuarioM>())
                .Where(a=>a.Object.Correo==p.Correo)
                .Select(item => new UsuarioM
                {
                    IdUsuario = item.Key,
                    Admin =item.Object.Admin,
                    FotoUsuario = item.Object.FotoUsuario,
                    Apellido = item.Object.Apellido,
                    Contrasenia = item.Object.Contrasenia,
                    Correo = item.Object.Correo,
                    Estado = item.Object.Estado,
                    IdAdministrador = item.Object.IdAdministrador,
                    Nombre = item.Object.Nombre,
                    NumEncuesta = item.Object.NumEncuesta

                }).ToList();
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
