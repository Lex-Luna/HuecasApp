using HuecasAppUsers.Conexiones;
using HuecasAppUsers.Datos;
using HuecasAppUsers.Modelo;
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
    public class DetalleEncuestaUserVM : BaseVM
    {
        #region CONSTRUCTOR
        public DetalleEncuestaUserVM(INavigation navigation, EncuestaM e)//
        {
            Navigation = navigation;
            IdCalificacion = e.IdCalificacion;

            Task.Run(async () =>
            {
                await MostrarCalificacionId(IdCalificacion);

            }).Wait();
        }

        #endregion
        #region Variables


        #endregion
        #region Objetos

        public ObservableCollection<CalificacionM> LisCalificacion { get; set; }

        #endregion
        #region Procesos
        public string IdCalificacion { get; set; }
        private async Task Volver()
        {
            await Navigation.PopAsync();
        }

        private async Task MostrarCalificacionId(string Id)
        {
            try
            {
                LisCalificacion = new ObservableCollection<CalificacionM>(calificacion);
                CalificacionD f = new CalificacionD();
                var calificacion = await f.MostCalificacionXId(Id);

            }
            catch (Exception e)
            {

                Debug.WriteLine("Error: No se pudo consulta la tabla Calificacion " + e);
            }

        }



        #endregion
        #region Comandos
        //public ICommand InsertarRecolecoresComand => new Command(async () => await InsertarRecolecoresProces());
        public ICommand Volvercomamd => new Command(async () => await Volver());
        //public ICommand NavClientesComand => new Command(async () => await NavClientes());

        #endregion

    }
}
