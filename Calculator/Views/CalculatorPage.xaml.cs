using Calculator.Controllers;
using Calculator.Models.DataModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Calculator.Views;

public partial class CalculatorPage : ContentPage
{
    private readonly IEquationController _equationController;
    public CalculatorPage(EquationController equationController)
    {
        InitializeComponent();
        _equationController = equationController;
        UpdateDisplay();
    }
    protected override void OnAppearing()
    {
        UpdateDisplay();
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
        if (equation.IsResult)
        {
            MainDisplay.Text = equation.MainDisplay;
        }
        else
        {
            MainDisplay.Text = Regex.Replace(equation.MainDisplay, @"(\d)(?=(\d{3})+$)", "$1,");
        }
        UpperDisplay.Text = equation.UpperDisplay;
        Entry.Focus();
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

    private void KeyPressed(object sender, TextChangedEventArgs e)
    {
        string key = Entry.Text;
        Entry.Text = "";
        string numberPattern = @"[0-9]";
        string operationPattern = @"[+\-*/]";
        string syntaxPattern = @"[=cC]";
        if (Regex.IsMatch(key, numberPattern))
        {
            _equationController.AddNumber(key);
        }
        else if (Regex.IsMatch(key, operationPattern))
        {
            Operator operation = Operators.GetOperator(key[0]);
            _equationController.AddOperation(operation);
        }
        else if (Regex.IsMatch(key, syntaxPattern))
        {
            switch (key)
            {
                case "c":
                case "C":
                    _equationController.Clear();
                    break;
                case "=":
                    _equationController.Calculate();
                    break;
            }
        }
        UpdateDisplay();
    }

    private void KeyFinish(object sender, EventArgs e)
    {
        _equationController.Calculate();
        UpdateDisplay();
        _equationController.Clear();
    }
}