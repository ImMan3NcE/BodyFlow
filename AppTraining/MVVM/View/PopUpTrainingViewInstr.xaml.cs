using AppTraining.MVVM.ModelView;

namespace AppTraining.MVVM.View;

public partial class PopUpTrainingViewInstr 
{
	public PopUpTrainingViewInstr()
	{
		InitializeComponent();
		BindingContext = new PopUpTrainingViewModel();

        var displayInfo = DeviceDisplay.Current.MainDisplayInfo;
        
        int winWidth = (int)(displayInfo.Width / displayInfo.Density / 1.5);
        int winHeight = (int)(displayInfo.Height / displayInfo.Density / 2);
        

        Size = new Size(winWidth, winHeight);
    }
}