using Practica_SaladilloWare.Assets;
using Practica_SaladilloWare.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Practica_SaladilloWare
{
    public partial class App : Application
    {
        public static Pedido_Repository Pedido_Repository { get; set; }
        public static PlacaBase_Repository PlacaBase_Repository { get; set; }
        public static Chasis_Repository Chasis_Repository { get; set; }
        public static Procesador_Repository Procesador_Repository { get; set; }
        public static RAM_Repository Ram_Repository { get; set; }
        public static TarjetaGrafica_Repository TarjetaGrafica_Repository { get; set; }
        public static Usuario_Repository Usuario_Repository { get; set; }

        public App(string filename)
        {
            InitializeComponent();

            Pedido_Repository = new Pedido_Repository(filename);
            PlacaBase_Repository = new PlacaBase_Repository(filename);
            Chasis_Repository = new Chasis_Repository(filename);
            Procesador_Repository = new Procesador_Repository(filename);
            Ram_Repository = new RAM_Repository(filename);
            TarjetaGrafica_Repository = new TarjetaGrafica_Repository(filename);
            Usuario_Repository = new Usuario_Repository(filename);

            MainPage = new LogIn_View();
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
