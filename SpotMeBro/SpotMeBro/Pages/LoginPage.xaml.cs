using System;
using System.Text;
using System.Security.Cryptography;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpotMeBro.Database;
using SpotMeBro.HelperFiles;
using System.Collections.Generic;

namespace SpotMeBro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        string username;
        string password;
        List<LoginModel> returnedUserResults;

        public LoginPage()
        {
            InitializeComponent();
            DatabaseClass.InitializeTables();//Creates the tables if none exist


        }
        static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            username = usernameX.Text;
            password = passwordX.Text;
            string hashString = username + password;
            byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(hashString);
            byte[] tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
            string storedHash = ByteArrayToString(tmpHash);
            returnedUserResults = DatabaseClass.lookForUserHash(storedHash);
            if (returnedUserResults.Count > 0)
            {
                DisplayAlert("Welcome", "Logging you in, " + returnedUserResults[0].username, "OK");
                await Navigation.PushAsync(new Main());
            }
            else
            {
                await DisplayAlert("Error", "Wrong username and/or password.", "OK");
            }
        }
    }
}