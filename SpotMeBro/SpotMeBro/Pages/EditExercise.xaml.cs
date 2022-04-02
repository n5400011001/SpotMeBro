using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpotMeBro.Database;
using SpotMeBro.HelperFiles;
using System;


namespace SpotMeBro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditExercise : ContentPage
    {
        public EditExercise()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            collectionView.ItemsSource = DatabaseClass.getAllExercises();
        }

        private async void AddExerciseToThisList_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditExercise1());
        }

        private async void EditExercise_Clicked(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var exercise = (ExerciseModel)layout.BindingContext;
            await Navigation.PushAsync(new EditThisExercise(exercise));
        }
    }
}