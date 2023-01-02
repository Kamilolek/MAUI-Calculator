using Calculator.Controllers;
using Calculator.Models.ViewModels;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Calculator.Views;

public partial class HistoryPage : ContentPage
{
    private IHistoryController _historyController;
    public List<HistoryElementVM> History { get; private set; }
    public HistoryPage(HistoryController historyController)
    {
        _historyController = historyController;
        History = _historyController.GetHistory();
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        History = _historyController.GetHistory();
        HistoryList.ItemsSource = History;

    }

    private void DeleteEquation(object sender, EventArgs e)
    {
        _historyController.DeleteHistoryElement((int)((Button)sender).BindingContext);
        History = _historyController.GetHistory();
        HistoryList.ItemsSource = History;
    }

    private void CopyEquation(object sender, EventArgs e)
    {
        _historyController.CopyEquation((int)((Button)sender).BindingContext);
        var toast = Toast.Make("Equation copied to clipboard", ToastDuration.Short);
        toast.Show();
    }
}