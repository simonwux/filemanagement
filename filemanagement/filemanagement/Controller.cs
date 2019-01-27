using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManagement
{
    class Controller : IController
    {
        private IModel model;
        private IFolder currentFolder;
        private IFolder root;
        private const string saveFileName = "log.csv";

        public Controller(IModel model)
        {
            this.model = model;
            this.root = this.model.CreateRoot();
            this.currentFolder = this.root;
        }

        public string GetCurrentFolderName()
        {
            return this.currentFolder.GetName();
        }


        private void PrintAllToFile()
        {
            StreamWriter sw = new StreamWriter(saveFileName);
            sw.Close();
            model.PrintToFile("root", root);
        }

        private void ReadFromFile()
        {
            StreamReader sr = new StreamReader(saveFileName);
            StringBuilder log = new StringBuilder();
            string add = sr.ReadLine();
            while (add != null)
            {
                log.Append(add);
                add = sr.ReadLine();
            }
            sr.Close();
            string logResult = log.ToString();
            string[] res = logResult.Split(',');
            int index = 0;
            while (index < res.Length)
            {
                string route = res[index];
                if (route.Equals(""))
                {
                    break;
                }
                index++;
                string[] routeRes = route.Split('/');
                IFolder current = root;
                for (int i = 1; i < routeRes.Length; i++)
                {
                    current = current.GetFolder(routeRes[i]);
                }
                string name = res[index];
                index++;
                if (name.EndsWith(".txt"))
                {
                    string content = res[index];
                    index++;
                    model.AddFile(name, content, current);
                }
                else
                {
                    model.AddFolder(name, current);
                }
            }

        }

        public string ProcessCommand(string command)
        {
            string res = "";
            Console.WriteLine(command);
            string[] cmd = command.Split();
            cmd[0] = cmd[0].ToLower();
            if (cmd.Length == 0)
            {
                throw new Exception("No command given");
            }
            if (cmd[0].Equals("createdir"))
            {
                if (cmd.Length <= 1)
                {
                    throw new Exception("No enough parameter given");
                }
                try
                {
                    this.model.AddFolder(cmd[1], this.currentFolder);
                }
                catch (Exception e)
                {
                    Console.Write(e.ToString());
                }

            }
            else if (cmd[0].Equals("createfile"))
            {
                if (cmd.Length <= 2)
                {
                    throw new Exception("No enough parameter given");
                }
                try
                {
                    this.model.AddFile(cmd[1], cmd[2], this.currentFolder);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else if (cmd[0].Equals("changedir"))
            {
                if (cmd.Length < 2)
                {
                    throw new Exception("No enough parameter given");
                }
                try
                {
                    IFolder tmp = this.model.ChangeDir(cmd[1].Split('/'), this.root);
                    if (tmp != null)
                    {
                        this.currentFolder = tmp;
                    }
                    //Console.WriteLine("Nowdir " + currentFolder.GetName());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else if (cmd[0].Equals("delete"))
            {
                if (cmd.Length < 2)
                {
                    throw new Exception("No enough parameter given");
                }
                try
                {
                    this.model.Delete(cmd[1], this.root);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else if (cmd[0].Equals("print"))
            {
                try
                {
                    res = this.model.Print(this.currentFolder);
                }
                catch (Exception e) { }
            }
            else if (cmd[0].Equals("printall"))
            {
                try
                {
                    res = this.model.PrintAll(this.currentFolder, 1);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else if (cmd[0].Equals("open"))
            {
                try
                {
                    res = this.model.Open(cmd[1] + ".txt", this.currentFolder);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else if (cmd[0].Equals("write"))
            {
                try
                {
                    this.model.Write(cmd[1], cmd[2], this.currentFolder);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else if (cmd[0].Equals("format"))
            {
                try
                {
                    this.root = this.model.CreateRoot();
                    this.currentFolder = this.root;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else if (cmd[0].Equals("exit"))
            {
                try
                {
                    PrintAllToFile();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            else if (cmd[0].Equals("start"))
            {
                try
                {
                    ReadFromFile();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return res;
        }
    }
}
