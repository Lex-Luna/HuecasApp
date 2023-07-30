using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Firebase.Auth;
using HuecasAppUsers.Conexiones;
using Newtonsoft.Json;
using Xamarin.Essentials;
using System;
using HuecasAppUsers.Vista;
using HuecasAppUsers.Datos;
using HuecasAppUsers.Modelo;

namespace HuecasAppUsers.VistaModelo
{
    public class CrearCorreoVM : BaseVM
    {
        #region CONSTRUCTOR
        public CrearCorreoVM(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion
        #region VARIABLES
        string _IdUsuario;
        string txtNombre;
        string txtApellido;
        string txtCorreo;
        string txtContraseña;
        #endregion
        #region OBJETOS 
        public string TxtNombre { get { return txtNombre; } set { SetValue(ref txtNombre, value); } }
        public string TxtApellido { get { return txtApellido; } set { SetValue(ref txtApellido, value); } }
        public string TxtCorreo { get { return txtCorreo; } set { SetValue(ref txtCorreo, value); } }
        public string TxtContraseña { get { return txtContraseña; } set { SetValue(ref txtContraseña, value); } }

        #endregion
        #region PROCESOS

        public async Task btnCrearcuenta()
        {
            if (!string.IsNullOrEmpty(TxtNombre))
            {
                if (!string.IsNullOrEmpty(TxtApellido))
                {
                    if (!string.IsNullOrEmpty(TxtCorreo))
                    {
                        if (!string.IsNullOrEmpty(TxtContraseña))
                        {
                            await CrearCuenta();
                            await IniciarSesion();
                            await ObteberIdUsuario();
                            await InsertarUsuario();
                            await NavContenedor();
                        }
                        else
                            await DisplayAlert("Alerta", "Agregue una contraseña", "OK");
                    }
                    else
                        await DisplayAlert("Alerta", "Agregue un correo", "OK");
                }
                else
                    await DisplayAlert("Alerta", "Agregue un apellido", "OK");

            }
            else
                await DisplayAlert("Alerta", "Agregue un nombre", "OK");
        }

        public async Task CrearCuenta()
        {
            var funcion = new CuentaD();
            await funcion.CrearCuenta(TxtCorreo,TxtContraseña);
        }

        private async Task IniciarSesion()
        {
            var funcion = new CuentaD();
            await funcion.ValidCuenta(TxtCorreo, TxtContraseña);
        }

        public async Task ObteberIdUsuario()
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapyFirebase));
                var guardarId = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                var refrescarCOntenido = await authProvider.RefreshAuthAsync(guardarId);
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(refrescarCOntenido));
                _IdUsuario = guardarId.User.LocalId;
                //Preferences.Remove("MyFirebaseRefreshToken");

            }
            catch (Exception)
            {
                await DisplayAlert("Alerta", "X tu seguridad la sesion se a cerrado", "Ok");

            }
        }

        async Task NavContenedor()
        {
            await Navigation.PushAsync(new NavigationPage(new Contenedor()));
        }

        public async Task InsertarUsuario()
        {
            try
            {
                var funcion = new UsuarioD();
                var parametros = new UsuarioM();
                parametros.Apellido = TxtApellido;
                parametros.Contrasenia = TxtContraseña;
                parametros.Correo = TxtCorreo;
                parametros.Estado = true;
                parametros.IdAdministrador = "lUUpQuSwqibNTFqEq4LVQKK8kEG2";
                parametros.Nombre =TxtNombre;
                _IdUsuario = await funcion.InserUsuario(parametros);
                //Process.GetCurrentProcess().CloseMainWindow();


            }
            catch (Exception er)
            {

                await DisplayAlert("Alerta", "no se pudo crear el usuario", "Ok" + er);
            }


        }


        /*private void CerrarSesion()
        {
            Preferences.Remove("MyFirebaseRefreshToken");
        }*/

        private async Task Volver()
        {
            await Navigation.PopAsync();
        }



        #endregion
        #region COMANDOS
        public ICommand btnCrearcuentaComand => new Command(async () => await btnCrearcuenta());
        public ICommand Volvercomamd => new Command(async () => await Volver());
        //public ICommand NavClientesComand => new Command(async () => await NavClientes());

        #endregion
    }
}
