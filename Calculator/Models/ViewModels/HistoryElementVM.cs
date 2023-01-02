using Calculator.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models.ViewModels
{
    public class HistoryElementVM
    {
        public string Equation { get; set; }
        public string Result { get; set; }
        public int Id { get; set; }
    }
}
