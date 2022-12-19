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

        public Operator(int priority, string symbol, string name)
        {
            Priority = priority;
            Symbol = symbol;
            Name = name;
        }

    }
    public static class Operators
    {
        public static Dictionary<string, Operator> OperatorsDictionary = new()
        {
            { "Add", new Operator(1, "+", "Add") },
            { "Subtract", new Operator(1, "-", "Subtract") },
            { "Multiply", new Operator(2, "*", "Multiply") },
            { "Divide", new Operator(2, "/", "Divide") },
            { "Power", new Operator(3, "^", "Power") },
            { "OpeningBracket", new Operator(0, "(", "OpeningBracket") },
            { "ClosingBracket", new Operator(1, ")", "ClosingBracket") }
        };
        public static Operator GetOperator(string type)
        {
            return OperatorsDictionary[type];
        }
    }
}
