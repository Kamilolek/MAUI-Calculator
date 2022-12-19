using Calculator.Models.DataModels;
using Calculator.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Controllers
{
    public class HistoryController : IHistoryController
    {
        private List<HistoryElement> _history = new();
        private string _historySessionPath;
        private IEquationController _equationController;
        public HistoryController(EquationController equationController)
        {
            _equationController = equationController;

        }

        public void AddToHistory(Equation equation)
        {
            _history.Add(new HistoryElement
            {
                Equation = equation,
                Date = DateTime.Now
            });
            SaveHistory(_history.Last());
        }

        public List<HistoryElementVM> GetHistory()
        {
            List<HistoryElementVM> history = _history.Select(x => new HistoryElementVM
            {
                Equation = x.Equation.UpperDisplay,
                Result = x.Equation.Result.ToString(),
                EquationObject = x.Equation
            }).ToList();
            return history;
        }
        private void SaveHistory(HistoryElement historyElement)
        {
            
        }
    }
}
