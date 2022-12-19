using Calculator.Models.DataModels;
using Calculator.Models.ViewModels;

namespace Calculator.Controllers
{
    public interface IHistoryController
    {
        void AddToHistory(Equation equation);
        List<HistoryElementVM> GetHistory();
    }
}