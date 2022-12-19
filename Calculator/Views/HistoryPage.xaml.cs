using Calculator.Controllers;
using Calculator.Models.ViewModels;

namespace Calculator.Views;

public partial class HistoryPage : ContentPage
{
    private IHistoryController _historyController;
    public HistoryPage(HistoryController historyController)
    {
        _historyController = historyController;
        InitializeComponent();
    }
}