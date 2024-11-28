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
using Plugin.Media.Abstractions;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Linq;

namespace HuecasAppUsers.VistaModelo
{
    public class CrearCorreoVM : BaseVM
    {
        #region CONSTRUCTOR
        public CrearCorreoVM(INavigation navigation)
        {
            Navigation = navigation;
            /*CerrarSecion();*/
        }
        #endregion
        #region VARIABLES
        string _IdUsuario;
        string txtNombre;
        string txtApellido;
        string txtCorreo;
        string txtContraseña;
        string rutafoto;
        MediaFile foto;
        #endregion
        #region OBJETOS 
        public string TxtNombre { get { return txtNombre; } set { SetValue(ref txtNombre, value); } }
        public string TxtApellido { get { return txtApellido; } set { SetValue(ref txtApellido, value); } }
        public string TxtCorreo { get { return txtCorreo; } set { SetValue(ref txtCorreo, value); } }
        public string TxtContraseña { get { return txtContraseña; } set { SetValue(ref txtContraseña, value); } }

        #endregion
        #region PROCESOS
        void CerrarSecion()
        {
            Preferences.Remove("MyFirebaseRefreshToken");

        }
        public async Task btnCrearcuenta()
        {

            try
            {
                if (foto != null)
                {
                    if (!string.IsNullOrEmpty(TxtNombre))
                    {
                        if (!string.IsNullOrEmpty(TxtApellido))
                        {
                            if (!string.IsNullOrEmpty(TxtCorreo))
                            {
                                if (!string.IsNullOrEmpty(TxtContraseña))
                                {
                                    //await DisplayAlert("Alerta", "Esta seguro de haber tomado la foto con el Icono de la camara?", "No", "Si");
                                    await CrearCuenta();
                                    await ObtenerIdUsuario();
                                    await InsertarUsuario();
                                    await IniciarSesion();
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
                else
                    await DisplayAlert("Alerta", "No se a tomado una foto al usuario", "OK");
            }
            catch (Exception e)
            {

                throw e;
            }




        }
        //Crear cuenta Antigua
        /*public async Task CrearCuenta()
        {
            var funcion = new CuentaD();
            await funcion.CrearCuenta(TxtCorreo, TxtContraseña);
        }*/
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        //Nueva crear Cuenta
        public async Task CrearCuenta()
        {
            try
            {
                // Validar formato de correo
                if (!IsValidEmail(TxtCorreo))
                {
                    await DisplayAlert("Error", "Por favor ingrese un correo electrónico válido", "OK");
                    return;
                }
                // Validar longitud de contraseña
                if (TxtContraseña.Length < 6)
                {
                    await DisplayAlert("Error", "La contraseña debe tener al menos 6 caracteres", "OK");
                    return;
                }

                var funcion = new CuentaD();
                await funcion.CrearCuenta(TxtCorreo, TxtContraseña);
            }
            catch (FirebaseAuthException ex)
            {
                switch (ex.Reason)
                {
                    case AuthErrorReason.EmailExists:
                        await DisplayAlert("Error", "Este correo ya está registrado", "OK");
                        break;
                    case AuthErrorReason.WeakPassword:
                        await DisplayAlert("Error", "La contraseña es muy débil", "OK");
                        break;
                    default:
                        await DisplayAlert("Error", "Error al crear la cuenta: " + ex.Message, "OK");
                        break;
                }
                throw; // Re-lanzar la excepción si necesitas manejarla en un nivel superior
            }
        }

        private async Task IniciarSesion()
        {
            var funcion = new CuentaD();
            await funcion.ValidCuenta(TxtCorreo, TxtContraseña);
        }

        public async Task<string> ObtenerIdUsuario()
        {
            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(Constantes.WebapyFirebase));
                var guardarId = JsonConvert.DeserializeObject<FirebaseAuth>(Preferences.Get("MyFirebaseRefreshToken", ""));
                var refrescarCOntenido = await authProvider.RefreshAuthAsync(guardarId);
                /*Esete es el token que se guarda en el Usuario*/
                Preferences.Set("MyFirebaseRefreshToken", JsonConvert.SerializeObject(refrescarCOntenido));
                _IdUsuario = guardarId.User.LocalId;
                //Preferences.Remove("MyFirebaseRefreshToken");

            }
            catch (Exception)
            {
                await DisplayAlert("Alerta", "X tu seguridad la sesion se a cerrado", "Ok");

            }
            return _IdUsuario;
        }
        async Task NavContenedor()
        {
            await Navigation.PushAsync(new NavigationPage(new Contenedor()));
        }

        public async Task InsertarUsuario()
        {
            try
            {
                await SubirFotoUser();
                var funcion = new UsuarioD();
                var parametros = new UsuarioM();
                parametros.Apellido = TxtApellido;
                parametros.FotoUsuario = rutafoto;
                parametros.Contrasenia = TxtContraseña;
                parametros.Correo = TxtCorreo;
                parametros.Estado = true;
                parametros.IdAdministrador = "";
                parametros.Nombre = TxtNombre;
                parametros.IdUsuario = _IdUsuario; 
                await funcion.InserUsuario(parametros);
                //_IdUsuario = await funcion.InserUsuario(parametros);
            }
            catch (Exception er)
            {
                //await DisplayAlert("Alerta", "no se pudo crear el usuario", "Ok" + er);
                throw er;
            }
        }

        public async Task TomarFoto()
        {
            var camara = new StoreCameraMediaOptions();
            camara.PhotoSize = PhotoSize.Medium;
            camara.SaveToAlbum = true;
            try
            {
                foto = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(camara);
                if (foto != null)
                {
                    Foto = ImageSource.FromStream(() =>
                    {
                        return foto.GetStream();
                    });

                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task SubirFotoUser()
        {
            try
            {
                var funcion = new UsuarioD();
                if (rutafoto == null)
                {
                    rutafoto = await funcion.SubirFotoUser(foto.GetStream(), txtNombre);
                }
                else
                {
                    await DisplayAlert("No tomaste foto", "La seccion foto saldra vacia", "Ok");
                    rutafoto = "No hay foto";
                }
            }
            catch (Exception)
            {
                await DisplayAlert("No tomaste foto", "La seccion foto saldra vacia", "Ok");
                rutafoto = "No hay foto";
            }

        }

        public async Task<string> VerUsuario()
        {
            try
            {
                UsuarioD funcion = new UsuarioD();
                var datos = await funcion.MostUsuario();

                var settings = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };

                if (datos != null && datos.Any())
                {
                    return JsonConvert.SerializeObject(datos, settings);
                }
                else
                {
                    return "No se encontraron usuarios";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "No se pudo obtener la lista de usuarios: " + ex.Message, "OK");
                Debug.WriteLine($"Error en VerUsuario: {ex.Message}");
                return "Error al obtener usuarios: " + ex.Message;
            }
        }


        private async Task Volver()
        {
            await Navigation.PopAsync();
        }



        #endregion
        #region COMANDOS
        public ICommand btnCrearcuentaComand => new Command(async () => await btnCrearcuenta());
        public ICommand Volvercomamd => new Command(async () => await Volver());
        public ICommand TomarFotoComand => new Command(async () => await TomarFoto());
        public ICommand btnVerUsuario => new Command(async () => await VerUsuario());

        #endregion
    }
}
