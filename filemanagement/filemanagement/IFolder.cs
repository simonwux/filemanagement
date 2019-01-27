using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    interface IFolder : IItem
    {
        IFolder GetFolder(string folderName);

        Dictionary<string, IFolder> GetAllFolder();

        IFile GetFile(string fileName);

        Dictionary<string, IFile> GetAllFile();

        void PutFolder(string folderName, IFolder folder);

        void PutFile(string fileName, IFile file);

        void DeleteFolder(string folderName);

        void DeleteFile(string fileName);
    }
}
