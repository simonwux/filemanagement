using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    class File : IFile
    {
        private readonly string name;

        private readonly string content;

        public File(string name, string content)
        {
            this.name = name;
            this.content = content;
        }

        public string GetName()
        {
            return name;
        }

        public string GetContent()
        {
            return content;
        }
    }
}
