using APP12_TiendaInformatica.Asset;
using APP12_TiendaInformatica.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace APP12_TiendaInformatica
{
    public partial class App : Application
    {
        public static PedidoRepository pedidoRepository { get; set; }
        public static PlacaBaseRepository placaBaseRepository { get; set; }
        public static ChasisRepository chasisRepository { get; set; }
        public static ProcesadorRepository procesadorRepository { get; set; }
        public static RAMRepository ramRepository { get; set; }
        public static TarjetaGraficaRepository tarjetaGraficaRepository { get; set; }
        public static UsuarioRepository usuarioRepository { get; set; }

        public App(string filename)
        {
            InitializeComponent();

            pedidoRepository = new PedidoRepository(filename);
            placaBaseRepository = new PlacaBaseRepository(filename);
            chasisRepository = new ChasisRepository(filename);
            procesadorRepository = new ProcesadorRepository(filename);
            ramRepository = new RAMRepository(filename);
            tarjetaGraficaRepository = new TarjetaGraficaRepository(filename);
            usuarioRepository = new UsuarioRepository(filename);

            MainPage = new Login_View_Model();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
