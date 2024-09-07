using AppTraining.MVVM.ModelView;

namespace AppTraining.MVVM.View;

public partial class TrainingView : ContentPage
{
	public TrainingView()
	{
		InitializeComponent();
        TrainingViewModel viewModel = new TrainingViewModel();
        
        BindingContext = viewModel;
    }

    //private void BackToViewRoutines(object sender, TappedEventArgs e)
    //{
    //     Navigation.PopAsync();
    //}
}