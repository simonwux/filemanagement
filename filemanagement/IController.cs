using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    interface IController
    {
        string GetCurrentFolderName();

        string ProcessCommand(string command);
    }
}
