using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace filemanagement
{
     partial class Form1 : Form
    {
         class node
         {
             public int type, address; //0wenjianjia 1wenjian
             public string name, content, saddress;
             public node next;
             public node(int t, string s, int add)
             {
                 type = t;
                 name = s;
                 address = add;
                 content = "";
                 saddress = "";
                 next = null;
             }
         }
         class empty
         {
             public int x;
             public empty next;
             public empty(int t)
             {
                 x = t;
                 next = null;
             }
         }
         bool flag,namerepeat;
         node[] local = new node[10007];
         empty emptyhead;
         const string savefilename = "savefile.txt";

         void deletenode(node p)
         {
             node q = p.next;
             while (q != null)
             {
                 deletenode(local[q.address]);
                 q = q.next;
             }
             empty o = new empty(p.address);
             o.next = emptyhead;
             emptyhead = o;
             local[p.address] = null;
         }
         public void format()
        {
             for (int i=0;i<=10000;i++) local[i]=null;
             local[0]=new node(0,"local",0);

             empty p;

             for (int i = 9999; i >= 1; i--)
             {
                 p = new empty(i);
                 p.next = emptyhead;
                 emptyhead = p;
             }
        }
         node find(string s)
         {
             flag = true;
             int i, o = 6;
             node p = local[0];

             while (o <= s.Length - 1)
             {
                 string name = "";
                 for (i = o; i <= s.Length - 1; i++)
                     if (s[i] != ':') name = name + s[i]; else break;
                 o = i + 1;
                 if (name != "")
                 {
                     p = p.next;
                     while (p != null)
                     {
                         if (p.name == name) break;
                         p = p.next;
                     }
                     if (p == null)
                     {
                         flag = false;
                         return local[0];
                     }
                     p = local[p.address];
                 }
             }
             if (p != null) return p; else { flag = false; return local[0]; }
         }
         int applynode(int t, string sonname)
         {
             int newadd = emptyhead.x;
             local[newadd] = new node(t, sonname, newadd);
             emptyhead = emptyhead.next;
             return newadd;
         }
         void create(string s, int t, string sonname, string con)
         {
             namerepeat = false;
             node p = find(s);
             if (p != null)
             {
                 node q=p.next;
                 while (q != null)
                 {
                     if (q.name==sonname)
                     {
                         namerepeat=true;
                         return;
                     }
                     q = q.next;
                 }
                 int newadd = applynode(t, sonname);
                 q = new node(t, sonname, newadd);
                 q.next = p.next;
                 p.next = q;
                 if (t == 1) local[newadd].content = con;
                 local[newadd].saddress = s;
             }
         }
         void fatherchange(node p, int t)
         {
             node q = p.next;
             while (p != null)
             {
                 if (q.address == t)
                 {
                     p.next = q.next;
                     break;
                 }
                 p = q;
                 q = q.next;
             }
         }
         void deletefile(string s)
         {
             int i, j;
             string add = "";
             for (j = s.Length - 1; j >= 0; j--)
                 if (s[j] == ':') break;
             j--;
             for (i = 0; i <= j; i++) add = add + s[i];
             node p = find(s);
             if (flag == false) return;
             node q = find(add);
             fatherchange(q, p.address);
             deletenode(p);

         }
         void printall(node p, int t)
         {
             string st="";
             int i;
             for (i = 1; i <= t; i++) st=st+"\t";
             if (t == 0) this.listBox1.Items.Add(p.name); else
             if (p.type == 1) this.listBox1.Items.Add(st+p.name + ".txt\n"); else this.listBox1.Items.Add(st+p.name + "\n");

             node q = p.next;
             while (q != null)
             {
                 printall(local[q.address], t + 1);
                 q = q.next;
             }
         }
         void print(node p)
         {
             textBox1.Text = dir;
             this.listBox1.Items.Clear();
             this.listBox1.Items.Add(p.name+"\n");
             node q = p.next;
             while (q != null)
             {
                 if (q.type == 1) this.listBox1.Items.Add("\t" + q.name + ".txt\n"); else this.listBox1.Items.Add("\t" + q.name + "\n"); 
                 q = q.next;
             }
         }
         void open(string s)
         {
             node p = find(s);
             this.listBox1.Items.Clear();
             this.listBox1.Items.Insert(0, p.content + "\n");
         }
         void close()
         {
             print(find(dir));
         }
         void write(string s, string newcon)
         {
             node p = find(s);
             p.content = newcon;
             open(s);
         }
         void check(string s)
         {
             node p = find(s);

         }

         void readfile()
         {
             string s, add="", name, con;
             int i, t;
             StreamReader sr = new StreamReader(savefilename);
             add = sr.ReadLine();
             while (add!= null)
             {
                 t = 0;
                 name = sr.ReadLine();
                 s = "";
                 con = "";
                 if (name.Length>4)
                    for (i = name.Length - 4; i <= name.Length - 1; i++) s = s + name[i];
                 if (s == ".txt")
                 {
                     con = sr.ReadLine();
                     s = "";
                     for (i = 0; i <= name.Length - 5; i++) s = s + name[i];
                     name = s;
                     t = 1;
                 }
                 create(add, t, name, con);

                 add = sr.ReadLine();

             }
             sr.Close();
         }
         node[] stack = new node[10007];
         void printtofile(node p)
         {
             StreamWriter sw = new StreamWriter(savefilename);
             int top = 1;
             stack[top] = p;
             node x;
             while (top > 0)
             {
                 x = stack[top];
                 top--;
                 if (x.address != 0)
                 {
                     if (x.type == 1)
                     {
                         sw.WriteLine(x.saddress);
                         sw.WriteLine(x.name + ".txt");
                         sw.WriteLine(x.content);
                     }
                     else
                     {
                         sw.WriteLine(x.saddress);
                         sw.WriteLine(x.name);
                     }
                 }
                 node q = x.next;
                 while (q != null)
                 {
                     top++;
                     stack[top] = local[q.address];
                     q = q.next;
                 }
             }
             sw.Close();
         }
         void exitfile()
         {
             printtofile(local[0]);
         }

         string add = "", name = "", con = "", dir = "local", comm = "";

         public Form1()
         {
             InitializeComponent();
         }
         private void button1_Click(object sender, EventArgs e)
         {
             con = richTextBox1.Text;
             name = textBox2.Text;
             create(dir, 1, name, con);
             print(find(dir));
             if (namerepeat) listBox1.Items.Add("文件名与当前目录中文件/文件夹名重复");
         }
         private void button4_Click(object sender, EventArgs e)
         {
             con = "";
             name = textBox2.Text;
             Console.WriteLine(name);
             create(dir, 0, name, con);
             print(find(dir));
             if (namerepeat) listBox1.Items.Add("文件夹名与当前目录中文件/文件夹名重复");
         }

         private void button6_Click(object sender, EventArgs e)
         {
             dir = textBox1.Text;
             bool f1;
             node p = find(dir);
             f1 = flag;
             if (f1==false) dir="local";
             print(find(dir));
             if (f1 == false) this.listBox1.Items.Add("找不到相应目录，输出根目录");
         }

         private void textBox1_TextChanged(object sender, EventArgs e)
         {

         }

         private void textBox2_TextChanged(object sender, EventArgs e)
         {

         }

         private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
         {

         }

         private void button8_Click(object sender, EventArgs e)
         {
             format();
             print(find(dir));
         }

         private void richTextBox1_TextChanged(object sender, EventArgs e)
         {

         }

         private void button3_Click(object sender, EventArgs e)
         {
             name=textBox2.Text;
             node p=find(dir+":"+name);
             if (p.type == 0)
             {
                 if (flag) {dir = dir + ":" + name; print(find(dir));}
                 else this.listBox1.Items.Add("找不到要打开的文件/文件夹");
             }
             else
             {
                 if (flag) open(dir + ":" + name); else this.listBox1.Items.Add("找不到要打开的文件/文件夹");
             }
         }

         private void Form1_Load(object sender, EventArgs e)
         {
             format();
             readfile();
             print(find(dir));
         }

         private void button7_Click(object sender, EventArgs e)
         {
             exitfile();
             Close();
         }

         private void button2_Click(object sender, EventArgs e)
         {
             bool f1;
             name=textBox2.Text;
             deletefile(dir + ":" + name);
             f1 = flag;
             print(find(dir));
             if (f1==false) this.listBox1.Items.Add("找不到要删除的文件/文件夹");
         }

         private void button5_Click(object sender, EventArgs e)
         {
             string newcon;
             name=textBox2.Text;
             newcon=richTextBox1.Text;
             write(dir + ":" + name, newcon);
         }

         private void button9_Click(object sender, EventArgs e)
         {
             print(find(dir));
         }

         private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
         {

         }

         private void button10_Click(object sender, EventArgs e)
         {
             this.listBox1.Items.Clear();
             printall(local[0], 0);
         }
    }
}
