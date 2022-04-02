using System;
using System.Collections.Generic;
using SpotMeBro.Database;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SpotMeBro.HelperFiles;
using Microcharts;
using SkiaSharp;

namespace SpotMeBro.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowExerciseProgress : ContentPage
    {
        string thisExerciseName;
        List<WorkoutModel> allExercisesEverDone;
        int halfWay;
        int length;
        DateTime dateFrombackend;
        DateTime dateTobackend;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            exerciseTitlex.Text = "CURRENT PROGRESS FOR: " + thisExerciseName;
        }
        public ShowExerciseProgress(WorkoutModel exercise)
        {
            InitializeComponent();
            thisExerciseName = exercise.ExerciseName;
            allExercisesEverDone = DatabaseClass.getExerciseFromAllTime(thisExerciseName);
            length = allExercisesEverDone.Count;
            halfWay = length / 2;
            setChartsData();
        }

        private void setChartsData()
        {
            //set data for the REPS CHART
            ChartEntry[] reps = new ChartEntry[length];
            int maxRep = allExercisesEverDone[0].Reps;
            int maxRepIndex = 0;
            int minRep = allExercisesEverDone[0].Reps;
            int minRepIndex = 0;

            for (int i = 0; i < length; i++)
            {

                string Date = allExercisesEverDone[i].Date.ToString();
                string shortDate = Date.Substring(0, Date.IndexOf(' '));
                if(allExercisesEverDone[i].Reps > maxRep)
                {
                    maxRep = allExercisesEverDone[i].Reps;
                    maxRepIndex = i;
                }
                if(allExercisesEverDone[i].Reps < minRep)
                {
                    minRep = allExercisesEverDone[i].Reps;
                    minRepIndex = i;
                }
                if (i == 0 ||
                    i == halfWay ||
                    i == length - 1)
                {
                    reps[i] = new ChartEntry(allExercisesEverDone[i].Reps)
                    {
                        Label = shortDate,
                        ValueLabel = allExercisesEverDone[i].Reps.ToString(),
                        Color = SKColor.Parse("#0d00ff")
                    };
                }
                else
                {
                    reps[i] = new ChartEntry(allExercisesEverDone[i].Reps)
                    {
                        Color = SKColor.Parse("#0d00ff"),
                    };
                }
            }
            reps[maxRepIndex] = new ChartEntry(maxRep)
            {
                Label = "MAX REP",
                ValueLabel = maxRep.ToString(),
                Color = SKColor.Parse("#55ff00"),
            };
            reps[minRepIndex] = new ChartEntry(minRep)
            {
                Label = "MIN REP",
                ValueLabel = minRep.ToString(),
                Color = SKColor.Parse("#ff0000"),
            };


            //set data for the WEIGHT CHART
            ChartEntry[] weight = new ChartEntry[length];
            int maxWeight = allExercisesEverDone[0].Weight;
            int maxWeightIndex = 0;
            int minWeight = allExercisesEverDone[0].Weight;
            int minWeightIndex = 0;
            for (int i = 0; i < length; i++)
            {
                string Date = allExercisesEverDone[i].Date.ToString();
                string shortDate = Date.Substring(0, Date.IndexOf(' '));
                if (allExercisesEverDone[i].Weight > maxWeight)
                {
                    maxWeight = allExercisesEverDone[i].Weight;
                    maxWeightIndex = i;
                }
                if (allExercisesEverDone[i].Weight < minWeight)
                {
                    minWeight = allExercisesEverDone[i].Weight;
                    minWeightIndex = i;
                }
                if (i == 0 ||
                    i == halfWay ||
                    i == length - 1)
                {
                    weight[i] = new ChartEntry(allExercisesEverDone[i].Weight)
                    {
                        Label = shortDate,
                        ValueLabel = allExercisesEverDone[i].Weight.ToString(),
                        Color = SKColor.Parse("#0d00ff")
                    };
                }
                else
                {
                    weight[i] = new ChartEntry(allExercisesEverDone[i].Weight)
                    {
                        Color = SKColor.Parse("#0d00ff"),
                    };
                }
            }

            weight[maxWeightIndex] = new ChartEntry(maxWeight)
            {
                Label = "MAX Weight",
                ValueLabel = maxWeight.ToString(),
                Color = SKColor.Parse("#55ff00"),
            };
            weight[minWeightIndex] = new ChartEntry(minWeight)
            {
                Label = "MIN Weight",
                ValueLabel = minWeight.ToString(),
                Color = SKColor.Parse("#ff0000"),
            };

            repsChart.Chart = new LineChart()
            {
                Entries = reps,
                LabelTextSize = 35f,
                MinValue = 0,
                MaxValue = maxRep + 1,
                ValueLabelOrientation = Orientation.Horizontal,
                LineSize = 15,
                PointMode = PointMode.Circle,
                PointSize = 25
            };
            weightChart.Chart = new LineChart()
            {
                Entries = weight,
                LabelTextSize = 35f,
                MinValue = 0,
                MaxValue = maxWeight + 20,
                ValueLabelOrientation = Orientation.Horizontal,
                LineSize = 15,
                PointMode = PointMode.Circle,
                PointSize = 25
            };
        }
        private async void backToMain_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Main());
        }
    }
}