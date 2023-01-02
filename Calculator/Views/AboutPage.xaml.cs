using Calculator.Models.ViewModels;

namespace Calculator.Views;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
	}

    private void OpenGitHub(object sender, EventArgs e)
    {
		Launcher.OpenAsync(new AboutVM().ProjectRepo);
    }
}