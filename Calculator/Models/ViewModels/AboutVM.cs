using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models.ViewModels
{
    internal class AboutVM
    {
        public string Version => AppInfo.VersionString;
        public string Build => AppInfo.BuildString;
        public string Title => AppInfo.Name;
        public string Developer => "Kamil Augustak";
        public string GitHubURL => "https://github.com/Kamilolek";
        public string ProjectRepo => "https://github.com/Kamilolek/Calculator";

    }
}
