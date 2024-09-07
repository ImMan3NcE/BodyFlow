namespace AppTraining.MVVM.View;

public partial class MainView : ContentPage
{
	public MainView()
	{
		InitializeComponent();
	}

    private void  Button_Clicked(object sender, EventArgs e)
    {
         Navigation.PushAsync(new RoutinesView());
    }

    private void Button_Clicked_1(object sender, EventArgs e)
    {
        Navigation.PushAsync(new JournalView());
    }
}