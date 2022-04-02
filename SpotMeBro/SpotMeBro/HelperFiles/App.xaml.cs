using System;
using SpotMeBro.HelperFiles;
using SpotMeBro.Pages;
using Xamarin.Forms;
using System.IO;


namespace SpotMeBro
{
    public partial class App : Application
    {
        public App()
        {
            //InitializeData();
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage())
            //{
            //    BarBackgroundColor = Color.FromHex("#003057")
            //}
            ;
        }

        protected override void OnStart()
        {

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
