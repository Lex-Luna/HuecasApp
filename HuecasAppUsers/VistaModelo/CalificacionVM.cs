using HuecasAppUsers.Datos;
using HuecasAppUsers.Modelo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace HuecasAppUsers.VistaModelo
{
    public class CalificacionVM
    : BaseVM
    {

        #region CONSTRUCTOR
        public CalificacionVM(INavigation navigation)
        {
            Navigation = navigation;
        }
        #endregion
        #region VariablesCalificacion
        public string txtLocal;
        public string txtComida;
        public string txtAtencion;
        public bool recomendado = true;
        #endregion
        #region ObjetosCalificacion 
        public string TxtAtencion { get { return txtAtencion; } set { SetValue(ref txtAtencion, value); } }
        public string TxtLocal { get { return txtLocal; } set { SetValue(ref txtLocal, value); } }
        public string TxtComida { get { return txtComida; } set { SetValue(ref txtComida, value); } }
        public bool Recomendado { get { return recomendado; } set { SetValue(ref recomendado, value); } }

        #endregion
        #region ProcesosCalificacion
        private async Task Volver()
        {
            await Navigation.PopAsync();
        }

        public async Task AgregarCalificacion()
        {
            //await Subirfoto();
            var funcion = new CalificacionD();
            var parametros = new CalificacionM();
            parametros.CalificacionAtencion  = TxtAtencion;
            parametros.CalificacionComida = TxtComida;
            parametros.CalificacionLugar = TxtComida;
            parametros.Recomendacion = Recomendado;
            
            await funcion.InserCalificacion(parametros);
        }


        #endregion
        #region ComandosCalificacion
        //public ICommand InsertarRecolecoresComand => new Command(async () => await InsertarRecolecoresProces());
        public ICommand Volvercomamd => new Command(async () => await Volver());
        //public ICommand NavClientesComand => new Command(async () => await NavClientes());

        #endregion
    }
    
    
}
