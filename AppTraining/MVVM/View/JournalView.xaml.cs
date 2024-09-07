using AppTraining.MVVM.ModelView;
using CommunityToolkit.Maui.Views;

namespace AppTraining.MVVM.View;

public partial class JournalView : ContentPage
{
    
    public JournalView()
	{
		InitializeComponent();
        BindingContext = new JournalViewModel();

        PopUpJournal.PopupClosed += OnPopupClosed;

    }

    public void OpenPopUp(object sender, EventArgs e)
    {
        //this.ShowPopup(new PopUpJournal());

    }

    private void OnPopupClosed(object sender, EventArgs e)
    {
        //PopUpJournal.PopupClosed -= OnPopupClosed;


        if (BindingContext is JournalViewModel viewModel)
        {
            viewModel.Refresh();
        }

    }

}