using Calculator.Models.DataModels;

namespace Calculator.Controllers
{
    public interface IEquationController
    {
        void AddDot();
        void AddNumber(string number);
        void AddOperation(Operator operation, bool isResult = false);
        void Backspace();
        void Calculate();
        void ChangeMark();
        void Clear();
        Equation GetEquation();
    }
}