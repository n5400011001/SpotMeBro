using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpotMeBro.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SpotMeBro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Options : ContentPage
    {
        public Options()
        {
            InitializeComponent();
        }

        private async void deleteAllWorkouts_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Warning!", "You are about to delete all workouts you've ever recorded. This cannot be undone.", "DELETE", "Cancel");
            if (answer == true)
            {
                DatabaseClass.clearAllWorkouts();
                DisplayAlert("Alert", "All workouts have been deleted", "OK");
            }
        }
        private async void deleteAllExercises_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Warning!", "You are about to delete all of the exercises from this list.", "DELETE", "Cancel");
            if (answer == true)
            {
                DatabaseClass.clearAllExercises();
                DisplayAlert("Alert", "All exercises have been deleted", "OK");
            }
        }
    }
}