using AppTraining.MVVM.ModelView;

namespace AppTraining.MVVM.View;

public partial class PopUpJournal 
{
    public static event EventHandler PopupClosed;

    public JournalViewModel journalViewModel;
    public PopUpJournal()
	{
		InitializeComponent();
        BindingContext = new PopUpJournalViewModel();
        
    }

    private void ClosePopup(object sender, EventArgs e)
    {

        Close();
        PopupClosed?.Invoke(null, EventArgs.Empty);

    }

    
}