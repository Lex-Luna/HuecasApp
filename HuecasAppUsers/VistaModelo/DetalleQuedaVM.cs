using HuecasAppUsers.Datos;
using HuecasAppUsers.Modelo;
using HuecasAppUsers.Vista;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HuecasAppUsers.VistaModelo
{
    public class DetalleQuedaVM : BaseVM
    {
        public DetalleQuedaVM(INavigation navigation, UsuarioM p)
        {
            
            Navigation = navigation;
            IdUsuario = p.IdUsuario;
            Task.Run(async () =>
            {
                await MostraruserId(IdUsuario);
                

            }).Wait();
        }
        #region Variables
        //string _IdUsuario;
        string _apellido;
        string _correo;
        string _nombre;
        string _contrania;
        string _idAdmin;
        string fotoUsuario;
        bool _estado;
        int _numEncuesta;

        #endregion
        #region Objetos
        public int NumEncuesta
        {
            get { return _numEncuesta; }
            set { SetValue(ref _numEncuesta, value); }
        }
        public string Apellido
        {
            get { return _apellido; }
            set { SetValue(ref _apellido, value); }
        }
        public string Contrania
        {
            get { return _contrania; }
            set { SetValue(ref _contrania, value); }
        }

        public string IdAdmin
        {
            get { return _idAdmin; }
            set { SetValue(ref _idAdmin, value); }
        }

        public bool Estado
        {
            get { return _estado; }
            set { SetValue(ref _estado, value); }
        }

        public string Nombre
        {
            get { return _nombre; }
            set { SetValue(ref _nombre, value); }
        }
        public string IdUsuario { get; set; }
        #endregion
        #region Procesos

        

        public ObservableCollection<UsuarioM> LisUsuario{ get; set; }
        //string _IdUsuario;
        
        public async Task MostraruserId(string Id)
        {
            try
            {
                var f = new UsuarioD();
                var usuario = await f.MostUsuarioXId(Id);
                LisUsuario= new ObservableCollection<UsuarioM>(usuario);


            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: No se pudo consulta la tabla Calificacion " + e);
            }

        }
        async Task VanearUsuario(string id)
        {
            try
            {
                var f = new UsuarioD();
                var p = new UsuarioM { IdUsuario = id };
                
                await f.UsuarioVaneado(p);
                await DisplayAlert("Alerta", "Usuario Baneado Correctamente", "OK");
                await Navigation.PushAsync(new MenuAdmin());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion
        #region Comandos
        //public Command VanearUsuarioCommand { get; }
        public ICommand VanearUsuarioComamd => new Command(async () => await VanearUsuario(IdUsuario));
        #endregion

    }
}
