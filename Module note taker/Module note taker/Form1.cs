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
using System.Globalization;

namespace Module_note_taker
{
    public partial class Form1 : Form
    {
        public Form1() // starting point of form application
        {
            InitializeComponent();
            string file = Path.GetFullPath(@"modules\");
            string[] Addresses = Directory.GetFiles(file);
            string[,] modules = new string[(Addresses.Length), 15];
            int count = 0;

            foreach (string filepath in Addresses)// create a new tab for every file in modules 
            {
                string title = readIn.ModT(filepath, modules, count);
                TabPage myTabPage = new TabPage(title);
                tabControl1.TabPages.Add(myTabPage);
                
                readIn.ModC(filepath, modules, count);
                readIn.ModS(filepath, modules, count);
                readIn.ModL(filepath, modules, count);
                readIn.ModA(filepath, modules, count);

                string folder = Path.GetFullPath(@"modules\notes\" + title);
                if (!System.IO.File.Exists(folder))
                {
                    System.IO.Directory.CreateDirectory(folder); //creates a note directory for every module
                }


                //series of labels to display module data
                Label AsM = new Label();
                
                AsM.Text = "Assignments \n" + modules[count, 9] + "\n" + modules[count, 10] + "\n" + modules[count, 11] + "\n" + modules[count, 12] + "\n" + modules[count, 13] + "\n" + modules[count, 14] + "\n";
                AsM.Dock = DockStyle.Top;
                AsM.BackColor = System.Drawing.Color.Transparent;
                AsM.MaximumSize = new Size(550, 0);
                AsM.AutoSize = true;
                myTabPage.Controls.Add(AsM);
                
                //gap in labels for formatting
                Label Gip = new Label();
                Gip.Dock = DockStyle.Top;

                Label LoM = new Label();
                LoM.Text = "Learning Objectives \n" + modules[count, 3] + "\n" + modules[count, 4] + "\n" + modules[count, 5] + "\n" + modules[count, 6] + "\n" + modules[count, 7] + "\n" + modules[count, 8] + "\n";
                LoM.Dock = DockStyle.Top;
                LoM.BackColor = System.Drawing.Color.Transparent;
                LoM.MaximumSize = new Size(550, 0);
                LoM.AutoSize = true;
                myTabPage.Controls.Add(LoM);

                Label space = new Label();
                space.Dock = DockStyle.Top;

                Label SyM = new Label();
                SyM.Text = "Synopsis \n" + modules[count, 2] + "\n";
                SyM.Dock = DockStyle.Top;
                SyM.BackColor = System.Drawing.Color.Transparent;
                SyM.MaximumSize = new Size(550, 0);
                SyM.AutoSize = true;
                myTabPage.Controls.Add(SyM);

                Label spc = new Label();
                spc.Dock = DockStyle.Top;

                Label TiM = new Label();
                TiM.Text = "Title \n" + modules[count, 1] + "\n";
                TiM.Dock = DockStyle.Top;
                AsM.BackColor = System.Drawing.Color.Transparent;
                TiM.MaximumSize = new Size(550, 0);
                TiM.AutoSize = true;
                myTabPage.Controls.Add(TiM);

                Label Gap = new Label();
                Gap.Dock = DockStyle.Top;

                Label CoM = new Label();
                CoM.Text = "Course Code \n" + modules[count,0] + "\n";
                CoM.Dock = DockStyle.Top;
                CoM.BackColor = System.Drawing.Color.Transparent;
                CoM.MaximumSize = new Size(550, 0);
                CoM.AutoSize = true;
                myTabPage.Controls.Add(CoM);

                

                Button note = new Button(); //creates instance of button for this specific tab
                note.Text = "Manage notes";
                note.Dock = DockStyle.Bottom;
                note.Height = 60;
                note.Click += (sender, eventArgs) =>
                {
                    Notes noteform = new Notes(folder); //creates instance of note form
                    noteform.Show();
                };
                myTabPage.Controls.Add(note);

                Button remove = new Button(); //creates instance of button for this specific tab
                remove.Text = "Delete Module";
                remove.Dock = DockStyle.Bottom;
                remove.Height = 60;
                Control controlR = remove.Parent;
                remove.Click += (sender, eventArgs) =>
                {
                    //dialog box for confirming deletion of module
                    DialogResult dialogResult = MessageBox.Show("Are you sure you wish to delete module?", "Delete", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        tabControl1.TabPages.Remove(myTabPage);
                        File.Delete(filepath);
                        Console.WriteLine("module deleted");
                    }
                    else if (dialogResult == DialogResult.No)
                    {
                       
                    }
                };
                myTabPage.Controls.Add(remove);

                count++;
            }            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            newmodule addmod = new newmodule(); // creates instance of add module file
            addmod.Show();
            this.Dispose(false); //so that new modules can be read in the main form is closed and reopened when newmodule ends.
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();//properly closes program when main menu gets closed.
        }
    }

    class readIn
    {
        public static string ModC(string filepath, string[,] modules, int count) //performs read in for the title of the module
        {
            try //tries the streamreader with the entered file name.
            {
                using (StreamReader sr = new StreamReader(filepath)) //opens module text file.
                {
                    while (sr.Peek() > -1)
                    {
                        string line = sr.ReadLine();
                        if (line.Trim() == "CODE") //when readline reaches code the next line is read into array
                        {
                            line = sr.ReadLine();
                            modules[count, 0] = line;
                            return (modules[count, 0]);
                        }


                    }
                }
            }



            catch (Exception e) //returns shortened error message if unable to read in text.
            {
                Console.WriteLine("{0}", e.ToString());
               // readIn.ModT(file);
            }
            return ("");
        }

        public static string ModT(string filepath, string[,] modules, int count) //performs read in for the title of the module
        {
            try //tries the streamreader with the entered file name.
            {
                using (StreamReader sr = new StreamReader(filepath)) //opens module text file.
                {
                    while (sr.Peek() > -1)
                    {
                        string line = sr.ReadLine();
                        if (line.Trim() == "TITLE") //when readline reaches title the next line is read into array
                        {
                            line = sr.ReadLine();
                            modules[count, 1] = line;
                            return (modules[count, 1]);

                        }


                    }
                }
            }

            catch (Exception e) //returns shortened error message if unable to read in text.
            {
                Console.WriteLine("{0}", e.ToString());
                // readIn.ModT(file);
            }
            return ("");
        }

        public static string ModS(string filepath, string[,] modules, int count) //performs read in for the title of the module
        {
            try //tries the streamreader with the entered file name.
            {
                using (StreamReader sr = new StreamReader(filepath)) //opens module text file.
                {
                    while (sr.Peek() > -1)
                    {
                        string line = sr.ReadLine();
                        if (line.Trim() == "SYNOPSIS")  //when readline reaches synopsis the next line is read into array
                        {
                            line = sr.ReadLine();
                            modules[count, 2] = line;
                            return (modules[count, 2]);
                        }


                    }
                }
            }



            catch (Exception e) //returns shortened error message if unable to read in text.
            {
                Console.WriteLine("{0}", e.ToString());
                // readIn.ModT(file);
            }
            return ("");
        }

        public static void ModL(string filepath, string[,] modules, int count) //performs read in for the title of the module
        {
            try //tries the streamreader with the entered file name.
            {
                using (StreamReader sr = new StreamReader(filepath)) //opens module text file.
                {
                    while (sr.Peek() > -1)
                    {
                        string line = sr.ReadLine();
                        if (line.Trim() == "LO") //when readline reaches LO keeps reading lines in untill assignment is reached
                        {
                            for(int i = 3; i <= 8; i++)
                            {
                                line = sr.ReadLine();
                                if (line.Trim() == "ASSIGNMENT")
                                {
                                    return;
                                }
                                else
                                {
                                    modules[count, i] = line;
                                }
                            }
                           
                        }
                    }
                }
            }

            catch (Exception e) //returns shortened error message if unable to read in text.
            {
                Console.WriteLine("{0}", e.ToString());
                // readIn.ModT(file);
            }
        }

        public static void ModA(string filepath, string[,] modules, int count) //performs read in for the title of the module
        {
            try //tries the streamreader with the entered file name.
            {
                using (StreamReader sr = new StreamReader(filepath)) //opens module text file.
                {
                    while (sr.Peek() > -1)
                    {
                        string line = sr.ReadLine();
                        if (line.Trim() == "ASSIGNMENT")
                        {
                            for (int i = 9; i <= 14; i++)
                            {
                                line = sr.ReadLine();
                                modules[count, i] = line;

                                DateTime dt;
                                string late = "   Submission date has passed"; // string for late flag
                                var regex = new System.Text.RegularExpressions.Regex(@"\b\d{2}\.\d{2}.\d{4}\b"); //filters date from assignment 
                                if (modules[count, i] != null)
                                {
                                        foreach (System.Text.RegularExpressions.Match m in regex.Matches(modules[count, i]))
                                        {

                                            if (DateTime.TryParseExact(m.Value, "dd.MM.yyyy", null, DateTimeStyles.None, out dt))
                                            {

                                                if (DateTime.Compare(dt, DateTime.Today) > 0) //compares date with todays date.
                                                {
                                                    modules[count, i] += late; // adds late flag to string
                                                    Console.WriteLine(modules[count, i]);
                                                }
                                                Console.WriteLine(count);
                                            }
                                            Console.WriteLine(count);
                                        }

                                }
                            }
                        }
                    }
                }
            }

            catch (Exception e) //returns shortened error message if unable to read in text.
            {
                Console.WriteLine("{0}", e.ToString());
                // readIn.ModT(file);
            }
        }


    }

}


