using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    interface IModel
    {
        IFolder CreateRoot();

        void AddFile(string fileName, string content, IFolder folder);

        void AddFolder(string folderName, IFolder folder);

        void Delete(string name, IFolder folder);

        void DeleteFolder(string folderName, IFolder folder);

        void DeleteFile(string fileName, IFolder folder);

        void Write(string fileName, string content, IFolder folder);

        string Print(IFolder folder);

        string PrintAll(IFolder folder, int layer);

        string Open(string name, IFolder folder);

        IFolder ChangeDir(string[] route, IFolder folder);

        int PrintToFile(string route, IFolder folder);
    }
}
