using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    interface IModel
    {
        IDirectory CreateRoot();

        void AddFile(string fileName, string content, IDirectory folder);

        void AddFolder(string folderName, IDirectory folder);

        void Delete(string name, IDirectory folder);

        void DeleteFolder(string folderName, IDirectory folder);

        void DeleteFile(string fileName, IDirectory folder);

        void Write(string fileName, string content, IDirectory folder);

        string Print(IDirectory folder);

        string PrintAll(IDirectory folder, int layer);

        string Open(string name, IDirectory folder);

        IDirectory ChangeDir(string[] route, IDirectory folder);

        int PrintToFile(string route, IDirectory folder);
    }
}
