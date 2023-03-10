using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models.DataModels
{
    public class Equation
    {
        public string UpperDisplay { get; set; }
        public string MainDisplay { get; set; }
        public float Result { get; set; }
        public bool IsResult { get; set; } = false;
        public List<EquationElement> EquationElements { get; set; } = new();
    }
}
