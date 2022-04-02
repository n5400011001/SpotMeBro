using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpotMeBro.Database;
using SpotMeBro.HelperFiles;

namespace SpotMeBro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickNextExercise : ContentPage
    {
        DateTime thisWorkout;
        public PickNextExercise(DateTime date)
        {
            InitializeComponent();
            thisWorkout = date;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = DatabaseClass.getAllExercises();
        }
        private async void StartExercise_Clicked(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var exercise = (ExerciseModel)layout.BindingContext;
            await Navigation.PushAsync(new CurrentExercise(thisWorkout, exercise));
        }

        private async void backToTodaysWorkout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TodaysWorkout(thisWorkout));
        }
    }
}