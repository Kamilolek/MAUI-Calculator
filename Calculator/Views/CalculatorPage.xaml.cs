using Calculator.Controllers;
using Calculator.Models.DataModels;

namespace Calculator.Views;

public partial class CalculatorPage : ContentPage
{
    private IEquationController _equationController;
    public CalculatorPage(EquationController equationController)
    {
        InitializeComponent();
        _equationController = equationController;
    }

    private void NumberClick(object sender, EventArgs e)
    {
        string data = ((Button)sender).Text;
        _equationController.AddNumber(data);
        UpdateDisplay();
    }

    private void OperationClick(object sender, EventArgs e)
    {
        string data = ((Button)sender).BindingContext.ToString();
        if (data == "Equals")
        {
            _equationController.Calculate();
            UpdateDisplay();
            _equationController.Clear();
        }
        else
        {
            Operator operation = Operators.GetOperator(data);
            _equationController.AddOperation(operation);
            UpdateDisplay();
        }
    }
    private void UpdateDisplay()
    {
        Equation equation = _equationController.GetEquation();
        MainDisplay.Text = equation.MainDisplay;
        UpperDisplay.Text = equation.UpperDisplay;
    }

    private void SyntaxClick(object sender, EventArgs e)
    {
        string data = ((Button)sender).BindingContext.ToString();
        switch (data)
        {
            case "+-":
                _equationController.ChangeMark();
                break;
            case ".":
                _equationController.AddDot();
                break;
            case "Clear":
                _equationController.Clear();
                break;
            case "Back":
                _equationController.Backspace();
                break;
        }
        UpdateDisplay();
    }
}