using Calculator.Models.DataModels;
using Calculator.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Calculator.Controllers
{
    public class HistoryController : IHistoryController
    {
        private List<HistoryElement> _history = new();
        private string _historySessionPath;

        public HistoryController()
        {
            _historySessionPath = Path.Combine(FileSystem.AppDataDirectory, Guid.NewGuid().ToString()+".history.json");
            Debug.WriteLine(_historySessionPath);
            LoadHistory();
        }

        public void AddToHistory(Equation equation)
        {
            _history.Add(new HistoryElement
            {
                Equation = equation,
                Date = DateTime.Now,
                FileName = _historySessionPath
            });
            SaveHistory(_history.Last());
        }

        public List<HistoryElementVM> GetHistory()
        {
            List<HistoryElementVM> history = _history.Select(x => new HistoryElementVM
            {
                Equation = x.Equation.UpperDisplay,
                Result = x.Equation.Result.ToString(),
            }).ToList();
            for(int i = 0; i < history.Count; i++)
            {
                history[i].Id = i;
            }
            history.Reverse();
            return history;
        }
        private void SaveHistory(HistoryElement historyElement)
        {
            string jsonElement = JsonSerializer.Serialize(historyElement);
            File.AppendAllLines(_historySessionPath, new string[] { jsonElement });
        }
        private void LoadHistory()
        {
            if (Directory.Exists(FileSystem.AppDataDirectory))
            {
                List<string> historyPaths = Directory.GetFiles(FileSystem.AppDataDirectory)
                    .Where(x => x.EndsWith(".history.json"))
                    .OrderByDescending(x => new FileInfo(x).CreationTime)
                    .ToList();
                int historyLength = historyPaths.Count > 5 ? 5 : historyPaths.Count;
                if (historyLength == 0) return;
                for (int i = historyLength - 1; i >= 0; i--)
                {
                    string historySessionPath = historyPaths[i];
                    string[] historyElements = File.ReadAllLines(historySessionPath);
                    foreach (string element in historyElements)
                    {
                        HistoryElement historyElement = JsonSerializer.Deserialize<HistoryElement>(element);
                        _history.Add(historyElement);
                    }
                }
            }
        }

        public void CopyEquation(int Id)
        {
            string equation = _history[Id].Equation.UpperDisplay +  _history[Id].Equation.MainDisplay;
            Clipboard.SetTextAsync(equation);
        }
        public void DeleteHistoryElement(int Id)
        {
            HistoryElement historyElement = _history[Id];
            _history.RemoveAt(Id);
            List<string> historyElements =  File.ReadAllLines(historyElement.FileName).ToList();
            historyElements.Remove(JsonSerializer.Serialize(historyElement));
            File.WriteAllLines(historyElement.FileName, historyElements);
        }
    }
}
