using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    class Model : IModel
    {
        public IFolder CreateRoot()
        {
            return new Folder("root");
        }


        public int PrintToFile(string route, IFolder folder)
        {
            //Console.WriteLine(folder.GetName());
            StreamWriter sw = new StreamWriter("log.csv", true);
            StringBuilder res = new StringBuilder();
            folder.GetAllFolder()
                .Select(e => route + "," + e.Key + ",")
                .Aggregate(res, (a, b) => a.Append(b));
            folder.GetAllFile()
                .Select(e => route + "," + e.Key + "," + e.Value.GetContent() + ",")
                .Aggregate(res, (a, b) => a.Append(b));
            sw.Write(res.ToString());
            sw.Close();

            foreach (var item in folder.GetAllFolder())
            {
                PrintToFile(route + "/" + item.Key, item.Value);
            }
            //folder.GetAllFolder()
            //    .Select(e => PrintToFile(e.Value, route + "/" + e.Key));
            return 0;

        }

        public void AddFile(string fileName, string content, IFolder folder)
        {
            if (fileName == null || fileName.Equals(""))
            {
                throw new Exception("File name cannot be empty.");
            }
            if (folder == null)
            {
                throw new Exception("Father dir cannot be empty.");
            }
            if (folder.GetFile(fileName) != null)
            {
                throw new Exception("File already exists.");
            }

            folder.PutFile(fileName, new File(fileName, content));
        }

        public void AddFolder(string folderName, IFolder folder)
        {
            if (folderName == null || folderName.Equals(""))
            {
                throw new Exception("File name cannot be empty.");
            }
            if (folder == null)
            {
                throw new Exception("Father dir cannot be empty.");
            }
            if (folder.GetFile(folderName) != null)
            {
                throw new Exception("File already exists.");
            }

            folder.PutFolder(folderName, new Folder(folderName));
        }


        public void Delete(string name, IFolder folder)
        {
            if (name == null || name.Equals(""))
            {
                throw new Exception("Name cannot be empty.");
            }

            if (folder == null)
            {
                throw new Exception("Folder does not exist.");
            }

            if (name.EndsWith(".txt"))
            {
                if (folder.GetFile(name) == null)
                {
                    throw new Exception("The file does not exist.");
                }
                this.DeleteFile(name, folder);
            }
            else
            {
                if (folder.GetFolder(name) == null)
                {
                    throw new Exception("The file does not exist.");
                }
                this.DeleteFolder(name, folder);

            }


        }

        public void DeleteFolder(string folderName, IFolder folder)
        {
            folder.DeleteFolder(folderName);
        }

        public void DeleteFile(string fileName, IFolder folder)
        {
            folder.DeleteFile(fileName);
        }

        public void Write(string fileName, string content, IFolder folder)
        {
            if (!folder.GetAllFolder().ContainsKey(fileName)) {
                throw new Exception("File has not been created.");
            }
            folder.PutFile(fileName, new File(fileName, content));
        }

        public string Print(IFolder folder)
        {
            StringBuilder res = new StringBuilder();
            folder.GetAllFolder()
                .Select(e => "\t" + e.Key + "\n")
                .Aggregate(res, (a, b) => a.Append(b));
            folder.GetAllFile()
                .Select(e => "\t" + e.Key + "\n")
                .Aggregate(res, (a, b) => a.Append(b));
            //foreach (var item in folder.GetAllFolder())
            //{
            //    res.Append("\t");
            //    res.Append(item.Key);
            //    res.Append("\n");
            //}
            //foreach (var item in folder.GetAllFile())
            //{
            //    res.Append("\t");
            //    res.Append(item.Key);
            //    res.Append("\n");
            //}
            return res.ToString();
        }

        public string PrintAll(IFolder folder, int layer)
        {
            StringBuilder res = new StringBuilder();
            StringBuilder head = new StringBuilder();
            for (int i = 0; i < layer; i++)
            {
                head.Append("\t");
            }
            string headTab = head.ToString();
            folder.GetAllFolder()
                .Select(e => headTab + e.Key + "\n" + PrintAll(e.Value, layer + 1))
                .Aggregate(res, (a, b) => a.Append(b));
            folder.GetAllFile()
                .Select(e => headTab + e.Key + "\n")
                .Aggregate(res, (a, b) => a.Append(b));

            //foreach (var item in folder.GetAllFolder())
            //{
            //    for (int i = 0; i < layer; i++)
            //    {
            //        res.Append("\t");
            //    }
            //    res.Append(item.Key);
            //    res.Append("\n");
            //    res.Append(this.PrintAll(item.Value, layer + 1));
            //}
            //foreach (var item in folder.GetAllFile())
            //{
            //    for (int i = 0; i < layer; i++)
            //    {
            //        res.Append("\t");
            //    }
            //    res.Append(item.Key);
            //    res.Append("\n");
            //}
            return res.ToString();
        }

        public IFolder ChangeDir(string[] route, IFolder folder)
        {
            //if (route.Length == 0)
            //{
            //    throw new Exception("Empty route.");
            //}
            //if (!folder.GetName().Equals(route[0]))
            //{
            //    throw new Exception("Wrong route.");
            //}
            IFolder tmp = folder;
            //Console.WriteLine("Root " + folder.GetName());
            for (int i = 1; i < route.Length; i++)
            {
                try
                {
                    tmp = tmp.GetFolder(route[i]);
                }
                catch (Exception e)
                {
                    throw new Exception("Wrong route.");
                }
            }
            //Console.WriteLine("nowdir  " + tmp.GetName());
            return tmp;
        }


        public string Open(string name, IFolder folder)
        {
            if (folder.GetFile(name) == null)
            {
                return "";
            }
            return folder.GetFile(name).GetContent();
        }


    }
}
