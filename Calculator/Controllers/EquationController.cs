using Calculator.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Controllers
{
    public class EquationController : IEquationController
    {
        private Equation _equation;
        private readonly IHistoryController _historyController;
        private float _lastResult;
        public EquationController(HistoryController historyController)
        {
            _historyController = historyController;
            _equation = new()
            {
                UpperDisplay = "",
                MainDisplay = ""
            };
        }
        public void ChangeMark()
        {
            if(_equation.MainDisplay == null)
            {
                _equation.MainDisplay = "-";
                return;
            }
            if (_equation.MainDisplay.StartsWith("-"))
            {
                _equation.MainDisplay = _equation.MainDisplay.Substring(1);
            }
            else
            {
                _equation.MainDisplay = "-" + _equation.MainDisplay;
            }
        }
        public void AddDot()
        {
            if (!_equation.MainDisplay.Contains('.'))
            {
                _equation.MainDisplay += ".";
            }
        }

        public void AddOperation(Operator operation, bool isResult = false)
        {
            try
            {
                float number = float.Parse(_equation.MainDisplay);
                _equation.EquationElements.Add(new EquationElement
                {
                    DataType = OperationDataType.Number,
                    Number = number
                });
            }
            catch
            {
                if (_equation.EquationElements.Count == 0)
                {
                    if(operation.Name == "Root")
                    {
                        _equation.EquationElements.Add(new EquationElement
                        {
                            DataType = OperationDataType.Number,
                            Number = 2
                        });
                    }
                    else
                    {
                        _equation.EquationElements.Add(new EquationElement
                        {
                            DataType = OperationDataType.Number,
                            Number = _lastResult
                        });
                    }
                }
            }
            _equation.MainDisplay = "";
            if (isResult)
            {
                _equation.UpperDisplay = GenerateUpperDisplay() + "= ";
            }
            else
            {

                if (_equation.EquationElements.Last().DataType == OperationDataType.Number && operation.Name == "OpeningBracket")
                {
                    _equation.EquationElements.Add(new EquationElement
                    {
                        DataType = OperationDataType.Operation,
                        Operation = Operators.GetOperator("Multiply")
                    });
                }


                _equation.EquationElements.Add(new EquationElement
                {
                    DataType = OperationDataType.Operation,
                    Operation = operation
                });
                _equation.UpperDisplay = GenerateUpperDisplay();
            }

        }

        private string GenerateUpperDisplay()
        {
            string upperDisplay = "";
            foreach (EquationElement element in _equation.EquationElements)
            {
                if (element.DataType == OperationDataType.Number)
                {
                    upperDisplay += element.Number + " ";
                }
                else
                {
                    upperDisplay += element.Operation.Symbol + " ";
                }
            }
            return upperDisplay;
        }

        public void AddNumber(string number)
        {
            if (_equation.MainDisplay == "0")
            {
                _equation.MainDisplay = "";
            }
            _equation.MainDisplay += number;
        }

        public void Calculate()
        {
            AddOperation(null, true);
            List<EquationElement> equationElementsRPN = ConvertToReversedPolishNotation();
            CalculateWithReversedPolishNotation(equationElementsRPN);
        }


        private List<EquationElement> ConvertToReversedPolishNotation()
        {
            try
            {
                List<EquationElement> output = new();
                Stack<EquationElement> stack = new();
                foreach (EquationElement element in _equation.EquationElements)
                {
                    if (element.DataType == OperationDataType.Number)
                    {
                        output.Add(element);
                    }
                    else
                    {
                        if (element.Operation.Name == "OpeningBracket")
                        {
                            stack.Push(element);
                        }
                        else if (element.Operation.Name == "ClosingBracket")
                        {
                            while (stack.Peek().Operation.Name != "OpeningBracket")
                            {
                                output.Add(stack.Pop());
                            }
                            stack.Pop();
                        }
                        else
                        {
                            while (stack.Count > 0 && stack.Peek().Operation.Priority >= element.Operation.Priority)
                            {
                                output.Add(stack.Pop());
                            }
                            stack.Push(element);
                        }
                    }
                }
                while (stack.Count > 0)
                {
                    output.Add(stack.Pop());
                }
                return output;
            }
            catch (Exception e)
            {
                if (e.Message == "Stack empty.")
                {
                    _equation.UpperDisplay = "";
                    _equation.MainDisplay = "Syntax Error";
                }
                else
                {
                    _equation.UpperDisplay = "";
                    _equation.MainDisplay = "Error";
                }
                return new List<EquationElement>();
            }
        }    

        private void CalculateWithReversedPolishNotation(List<EquationElement> equationElements)
        {
            Stack<float> stack = new();
            try
            {
                for (int i = 0; i < equationElements.Count; i++)
                {
                    if (equationElements[i].DataType == OperationDataType.Number)
                    {
                        stack.Push(equationElements[i].Number);
                    }
                    else
                    {
                        float number2 = stack.Pop();
                        float number1 = stack.Pop();
                        if (number2 == 0 && equationElements[i].Operation.Name == "Divide")
                        {
                            _equation.UpperDisplay = "";
                            _equation.MainDisplay = "Undefined";
                            return;
                        }
                        Debug.WriteLine(number1 + " " + number2 + equationElements[i].Operation.Name);
                        float result = Calculate(number1, number2, equationElements[i].Operation);
                        stack.Push(result);
                    }

                }
                _equation.Result = stack.Pop();
                _equation.MainDisplay = _equation.Result.ToString();
                _historyController.AddToHistory(_equation);
                _lastResult = _equation.Result;
            }
            catch (Exception e)
            {
                if (e.Message == "Stack empty.")
                {
                    _equation.UpperDisplay = "";
                    _equation.MainDisplay = "Syntax Error";
                }
                else
                {
                    _equation.UpperDisplay = "";
                    _equation.MainDisplay = "Error";
                }
                return;
            }
        }

        private float Calculate(float number1, float number2, Operator operation)
        {
            switch (operation.Name)
            {
                case "Add":
                    return number1 + number2;
                case "Subtract":
                    return number1 - number2;
                case "Multiply":
                    return number1 * number2;
                case "Divide":
                    return number1 / number2;
                case "Power":
                    return (float)Math.Pow(number1, number2);
                case "Root":
                    return (float)Math.Pow(number2, 1 / number1);
                case "Modulo":
                    return number1 % number2;
                default:
                    return 0;
            }
        }

        public void Clear()
        {
            _equation = new Equation
            {
                MainDisplay = "",
                UpperDisplay = ""
            };
        }

        public void Backspace()
        {
            if (_equation.MainDisplay.Length >= 1)
            {
                _equation.MainDisplay = _equation.MainDisplay.Substring(0, _equation.MainDisplay.Length - 1);
            }
            else
            {
                if (_equation.EquationElements.Count <= 1)
                {
                    Clear();
                }
                else
                {
                    _equation.EquationElements.RemoveAt(_equation.EquationElements.Count - 1);
                }
                _equation.UpperDisplay = GenerateUpperDisplay();
            }
        }

        public Equation GetEquation()
        {
            return _equation;
        }
    }
}
