using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models.DataModels
{
    public class Operator
    {
        public int Priority { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string[] MatchingOperators { get; set; }

        public Operator(int priority, string symbol, string name, string[] matchingOperators)
        {
            Priority = priority;
            Symbol = symbol;
            Name = name;
            MatchingOperators = matchingOperators;
        }

    }
    public static class Operators
    {
        public static Dictionary<string, Operator> OperatorsDictionary = new()
        {
            { "Add", new Operator(1, "+", "Add", new string[]{"+"} )},
            { "Subtract", new Operator(1, "-", "Subtract",  new string[]{"-"}) },
            { "Multiply", new Operator(2, "×", "Multiply",  new string[]{"*", "×"}) },
            { "Divide", new Operator(2, "÷", "Divide",  new string[]{"/", "\\", "÷"}) },
            { "Modulo", new Operator(2, "%", "Modulo", new string[]{"%"}) },
            { "Root", new Operator(3, "√", "Root", new string[]{"p", "√"}) },
            { "Power", new Operator(3, "^", "Power",  new string[]{"^"}) },
            { "OpeningBracket", new Operator(0, "(", "OpeningBracket",  new string[]{"(", "[", "{"}) },
            { "ClosingBracket", new Operator(1, ")", "ClosingBracket",  new string[]{")", "]", "}"}) }
        };
        public static Operator GetOperator(string type)
        {
            return OperatorsDictionary[type];
        }
        public static Operator GetOperator(char symbol)
        {
            return OperatorsDictionary.Values.FirstOrDefault(x => x.MatchingOperators.Contains(symbol.ToString()));
        }
    }
}
