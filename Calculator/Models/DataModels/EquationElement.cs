using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models.DataModels
{
    public class EquationElement
    {
        public OperationDataType DataType { get; set; }
        public float Number { get; set; }
        public Operator Operation { get; set; }

    }
}
