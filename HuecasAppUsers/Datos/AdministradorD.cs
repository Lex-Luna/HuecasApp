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
    public class AdministradorD
    {
        public async Task InserUsuario(AdministradorM parametros)
        {
            await Constantes.firebase.Child("Administrador")
                .PostAsync(new AdministradorM()
                {
                    Contrasenia = parametros.Contrasenia,
                    Email = parametros.Email,
                    Nombres = parametros.Nombres
                });
        }

        public async Task<List<AdministradorM>> MostAdministrador()
        {
            return (await Constantes.firebase
                .Child("Administrador")
                .OnceAsync<AdministradorM>()).Select(item => new AdministradorM
                {

                    IdAdministrador = item.Key,
                    Contrasenia = item.Object.Contrasenia,
                    Email = item.Object.Email,
                    Nombres = item.Object.Nombres

                }).ToList();
        }
    }
}
