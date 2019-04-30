using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    class Directory : IDirectory
    {

        private string name;

        private Dictionary<string, IDirectory> directory;

        private Dictionary<string, IFile> file;

        public Directory(string name)
        {
            this.name = name;
            this.directory = new Dictionary<string, IDirectory>();
            this.file = new Dictionary<string, IFile>();
        }

        public string GetName()
        {
            return name;
        }

        public IDirectory GetFolder(string folderName)
        {
            if (this.directory.ContainsKey(folderName))
            {
                return this.directory[folderName];
            }
            return null;
        }

        public IFile GetFile(string fileName)
        {
            IFile res;
            if (this.file.ContainsKey(fileName))
            {
                return this.file[fileName];
            }
            return null;
        }

        public void PutFolder(string folderName, IDirectory folder)
        {
            if (!this.directory.ContainsKey(folderName))
            {
                this.directory.Add(folderName, folder);
            }
        }

        public void PutFile(string fileName, IFile file)
        {
            if (this.file.ContainsKey(fileName))
            {
                this.file.Remove(fileName);
            }
            this.file.Add(fileName, file);
        }


        public void DeleteFolder(string folderName)
        {
            if (this.directory[folderName] == null)
            {
                throw new Exception("Folder does not exist.");
            }
            this.directory.Remove(folderName);
        }

        public void DeleteFile(string fileName)
        {
            if (this.file[fileName] == null)
            {
                throw new Exception("File does not exist.");
            }

            this.file.Remove(fileName);
        }

        public Dictionary<string, IDirectory> GetAllFolder()
        {
            return this.directory;
        }

        public Dictionary<string, IFile> GetAllFile()
        {
            return this.file;
        }
    }
}
