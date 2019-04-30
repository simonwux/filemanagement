using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    interface IDirectory : IItem
    {
        IDirectory GetFolder(string folderName);

        Dictionary<string, IDirectory> GetAllFolder();

        IFile GetFile(string fileName);

        Dictionary<string, IFile> GetAllFile();

        void PutFolder(string folderName, IDirectory folder);

        void PutFile(string fileName, IFile file);

        void DeleteFolder(string folderName);

        void DeleteFile(string fileName);
    }
}
