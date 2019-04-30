using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    class Folder : IFolder
    {

        private string name;

        private Dictionary<string, IFolder> folder;

        private Dictionary<string, IFile> file;

        public Folder(string name)
        {
            this.name = name;
            this.folder = new Dictionary<string, IFolder>();
            this.file = new Dictionary<string, IFile>();
        }

        public string GetName()
        {
            return name;
        }

        public IFolder GetFolder(string folderName)
        {
            if (this.folder.ContainsKey(folderName))
            {
                return this.folder[folderName];
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

        public void PutFolder(string folderName, IFolder folder)
        {
            if (!this.folder.ContainsKey(folderName))
            {
                this.folder.Add(folderName, folder);
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
            if (this.folder[folderName] == null)
            {
                throw new Exception("Folder does not exist.");
            }
            this.folder.Remove(folderName);
        }

        public void DeleteFile(string fileName)
        {
            if (this.file[fileName] == null)
            {
                throw new Exception("File does not exist.");
            }

            this.file.Remove(fileName);
        }

        public Dictionary<string, IFolder> GetAllFolder()
        {
            return this.folder;
        }

        public Dictionary<string, IFile> GetAllFile()
        {
            return this.file;
        }
    }
}
