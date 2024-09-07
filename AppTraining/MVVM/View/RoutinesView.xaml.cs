using AppTraining.MVVM.Model;
using AppTraining.MVVM.ModelView;
using AppTraining.Repositories;

namespace AppTraining.MVVM.View;

public partial class RoutinesView : ContentPage
{
    //public static RoutinesViewModel RoutinesViewModel { get; private set; }
    public RoutinesView()
	{
		InitializeComponent();
		BindingContext = new RoutinesViewModel();
        
	}
    protected override  void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is RoutinesViewModel viewModel)
        {
             viewModel.Refresh();
        }
    }

    //private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    //{
    //    var routine = ((VisualElement)sender).BindingContext as Exercise;

    //    App.ExerciseRepo.GenerateOneTraining(routine);

    //    Navigation.PushAsync(new TrainingView());
    //}
}