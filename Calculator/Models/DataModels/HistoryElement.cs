using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models.DataModels
{
    public class HistoryElement
    {
        public Equation Equation { get; set; }
        public DateTime Date { get; set; }
    }
}
