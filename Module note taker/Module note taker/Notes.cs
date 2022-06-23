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

namespace Module_note_taker
{
    public partial class Notes : Form
    {
        public Notes(string folder)//accepts program starting directory
        {
            InitializeComponent();
            string[] NAddress = Directory.GetFiles(folder);
            List<string> textList = new List<string>(); //list for storing the note texts as they're read in
            
            int count = 0; //counter for list position
            foreach (string A in NAddress)
            {
                string title = Path.GetFileName(A);
                TabPage myTabPage = new TabPage(title);
                myTabPage.Dock = DockStyle.Fill;
                tabControl1.TabPages.Add(myTabPage);
                textList.Add(File.ReadAllText(A)); //appends current notes text to list

                RichTextBox text = new RichTextBox();
                text.Multiline = true;
                text.Dock = DockStyle.Fill;
                Control controlT = text.Parent;
                text.Text = textList[count]; //fills textbox with current list string
                myTabPage.Controls.Add(text);

                Button save = new Button(); //creates instance of button for this specific tab
                save.Text = "save";
                save.Dock = DockStyle.Top;
                Control controlS = save.Parent;
                save.Click += (sender, eventArgs) =>
                {
                   System.IO.File.WriteAllText(A, text.Text); ; 
                };
                myTabPage.Controls.Add(save);

                Button remove = new Button(); //creates instance of button for this specific tab
                remove.Text = "Delete Module";
                remove.Dock = DockStyle.Bottom;
                Control controlR = remove.Parent;
                remove.Click += (sender, eventArgs) =>
                {   
                    //dialog box for confirming deletion of note
                    DialogResult dialogResult = MessageBox.Show("Are you sure you wish to delete note?", "Delete", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        tabControl1.TabPages.Remove(myTabPage);
                        File.Delete(A);
                        Console.WriteLine("note deleted \n {0}", A); //writes the path to console
                    }
                    else if (dialogResult == DialogResult.No) //if no is pressed nothing happens.
                    {

                    }
                };
                myTabPage.Controls.Add(remove);
                count++;
            }



            button1.Click += (sender, eventArgs) =>
            {
                //creates inputbox for new 
                string input = Microsoft.VisualBasic.Interaction.InputBox("Enter name of new note", "new note", "", -1, -1) + ".txt";
                string filepath = System.IO.Path.Combine(folder, input);
                if (!System.IO.File.Exists(filepath))
                {
                    using (System.IO.FileStream fs = System.IO.File.Create(filepath))//using filestream create new note
                    {
                        TabPage myTabPage = new TabPage(input);
                        var title = Path.GetFileName(input);
                        myTabPage.Dock = DockStyle.Fill;
                        tabControl1.TabPages.Add(myTabPage);

                        RichTextBox text = new RichTextBox(); //creates instance of richtextbox for this specific tab
                        text.Multiline = true;
                        text.Dock = DockStyle.Fill;
                        Control controlT = text.Parent;
                        myTabPage.Controls.Add(text);

                        Button save = new Button(); //creates instance of button for this specific tab
                        save.Text = "save";
                        save.Dock = DockStyle.Top;
                        Control controlS = save.Parent;
                        save.Click += (send, arg) =>
                        {
                            System.IO.File.WriteAllText(filepath, text.Text);
                            fs.Close();
                        };
                        myTabPage.Controls.Add(save);

                        Button remove = new Button(); //creates instance of button for this specific tab
                        remove.Text = "Delete Module";
                        remove.Dock = DockStyle.Bottom;
                        Control controlR = remove.Parent;
                        remove.Click += (send, arg) =>
                        {
                            //dialog box for confirming deletion of note
                            DialogResult dialogResult = MessageBox.Show("Are you sure you wish to delete note?", "Delete", MessageBoxButtons.YesNo);
                            if (dialogResult == DialogResult.Yes)
                            {
                                tabControl1.TabPages.Remove(myTabPage); //removes the notes tab
                                File.Delete(filepath); //deletes current notes text file
                                fs.Close(); //close filestream instance to avoid editing errors
                                Console.WriteLine("note deleted");//sends message to console

                            }
                            else if (dialogResult == DialogResult.No)
                            {

                            }
                        };
                        myTabPage.Controls.Add(remove);
                        fs.Close();
                    }
                }
                else
                {
                    //dialogue box in case the user might overwrite an existing note
                    DialogResult dialogResult = MessageBox.Show("file already exists, do you wish to replace it?", "conflict", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        System.IO.FileStream fs = System.IO.File.Create(filepath); //instance of filestream for creating file
                        fs.Close(); //close filestream instance to avoid editing errors
                        Notes noteform = new Notes(folder); //resets the form
                        noteform.Show(); 
                        this.Dispose(false);
                        Console.WriteLine("note replaced"); //sends message to console
                    }

                    else if (dialogResult == DialogResult.No)
                    {

                    }
                }
            };



        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
