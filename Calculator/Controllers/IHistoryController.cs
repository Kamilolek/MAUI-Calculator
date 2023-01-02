using Calculator.Models.DataModels;
using Calculator.Models.ViewModels;

namespace Calculator.Controllers
{
    public interface IHistoryController
    {
        void AddToHistory(Equation equation);
        void CopyEquation(int Id);
        void DeleteHistoryElement(int Id);
        List<HistoryElementVM> GetHistory();
    }
}