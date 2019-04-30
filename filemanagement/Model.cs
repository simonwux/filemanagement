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
        public IDirectory CreateRoot()
        {
            return new Directory("root");
        }


        public int PrintToFile(string route, IDirectory folder)
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

        public void AddFile(string fileName, string content, IDirectory folder)
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

        public void AddFolder(string folderName, IDirectory folder)
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

            folder.PutFolder(folderName, new Directory(folderName));
        }


        public void Delete(string name, IDirectory directory)
        {
            Console.WriteLine(directory.ToString());
            if (name == null || name.Equals(""))
            {
                throw new Exception("Name cannot be empty.");
            }

            if (directory == null)
            {
                throw new Exception("Directory does not exist.");
            }

            if (name.EndsWith(".txt"))
            {
                if (directory.GetFile(name) == null)
                {
                    throw new Exception("The file does not exist.");
                }
                this.DeleteFile(name, directory);
            }
            else
            {
                if (directory.GetFolder(name) == null)
                {
                    throw new Exception("The file does not exist.");
                }
                this.DeleteFolder(name, directory);

            }


        }

        public void DeleteFolder(string folderName, IDirectory directory)
        {
            directory.DeleteFolder(folderName);
        }

        public void DeleteFile(string fileName, IDirectory directory)
        {
            directory.DeleteFile(fileName);
        }

        public void Write(string fileName, string content, IDirectory directory)
        {
            if (!directory.GetAllFile().ContainsKey(fileName)) {
                throw new Exception("File has not been created.");
            }
            directory.PutFile(fileName, new File(fileName, content));
        }

        public string Print(IDirectory directory)
        {
            StringBuilder res = new StringBuilder();
            directory.GetAllFolder()
                .Select(e => "\t" + e.Key + "\n")
                .Aggregate(res, (a, b) => a.Append(b));
            directory.GetAllFile()
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

        public string PrintAll(IDirectory directory, int layer)
        {
            StringBuilder res = new StringBuilder();
            StringBuilder head = new StringBuilder();
            for (int i = 0; i < layer; i++)
            {
                head.Append("\t");
            }
            string headTab = head.ToString();
            directory.GetAllFolder()
                .Select(e => headTab + e.Key + "\n" + PrintAll(e.Value, layer + 1))
                .Aggregate(res, (a, b) => a.Append(b));
            directory.GetAllFile()
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

        public IDirectory ChangeDir(string[] route, IDirectory directory)
        {
            //if (route.Length == 0)
            //{
            //    throw new Exception("Empty route.");
            //}
            //if (!folder.GetName().Equals(route[0]))
            //{
            //    throw new Exception("Wrong route.");
            //}
            IDirectory tmp = directory;
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


        public string Open(string name, IDirectory directory)
        {
            if (directory.GetFile(name) == null)
            {
                return "";
            }
            return directory.GetFile(name).GetContent();
        }


    }
}
