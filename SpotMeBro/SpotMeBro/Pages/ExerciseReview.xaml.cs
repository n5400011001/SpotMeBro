using System;
using SpotMeBro.HelperFiles;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpotMeBro.Database;

namespace SpotMeBro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseReview : ContentPage
    {
        string exerciseName;
        string thisWorkoutMarkerString;
        DateTime thisWorkoutMarker;
        public ExerciseReview(DateTime date, string exercise)
        {
            InitializeComponent();
            exerciseName = exercise;
            thisWorkoutMarkerString = date.ToString();
            thisWorkoutMarker = date;
            BindingContext = exerciseName;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            exerciseTitlex.Text = exerciseName;
            thisExerciseSets.ItemsSource = DatabaseClass.getSetsDoneForExerciseReview(thisWorkoutMarker, exerciseName);
        }

        private async void backToTodaysWorkout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TodaysWorkout(thisWorkoutMarker));
        }
    }
}