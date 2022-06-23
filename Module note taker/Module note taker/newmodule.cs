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
    public partial class newmodule : Form
    {
        public newmodule()
        {
            InitializeComponent();
            string[] modules = new string[15]; //array holds the inputted values
            string file = Path.GetFullPath(@"modules\"); // finds the modules
            button2.Click += (sender, eventArgs) => //when button is pressed values are written to new module file
           {
               modules[0] = textBox2.Text; //code
               modules[1] = textBox3.Text; //Title
               modules[2] = richTextBox1.Text; //synopsis
               modules[3] = textBox4.Text; //LO
               modules[4] = textBox5.Text; //LO
               modules[5] = textBox6.Text; //LO
               modules[6] = textBox7.Text; //LO
               modules[7] = textBox8.Text; //LO
               modules[8] = textBox9.Text; //LO

               if (comboBox1.Text != "") //only writes assignment values if the combobox is set to a valid string
               {
                    modules[9] = comboBox1.Text + ": " + dateTimePicker1.Value.ToString();
               }

               if (comboBox2.Text != "")
               {
                   modules[10] = comboBox2.Text + ": " + dateTimePicker2.Value.ToString();
               }

               if (comboBox3.Text != "")
               {
                   modules[11] = comboBox3.Text + ": " + dateTimePicker3.Value.ToString();
               }

               if (comboBox4.Text != "")
               {
                   modules[12] = comboBox4.Text + ": " + dateTimePicker4.Value.ToString();
               }

               if (comboBox5.Text != "")
               {
                   modules[13] = comboBox5.Text + ": " + dateTimePicker4.Value.ToString();
               }

               if (comboBox6.Text != "")
               {
                   modules[14] = comboBox6.Text + ": " + dateTimePicker6.Value.ToString();
               }
               
               // creates module 
               FileStream fs = File.Create(file + modules[0] + ".txt");
               fs.Close();
               using (StreamWriter sr = new StreamWriter(file + modules[0] + ".txt"))
               {
                   sr.WriteLine("CODE");
                   sr.WriteLine(modules[0]);
                   sr.WriteLine("TITLE");
                   sr.WriteLine(modules[1]);
                   sr.WriteLine("SYNOPSIS");
                   sr.WriteLine(modules[2]);
                   sr.WriteLine("LO");
                   sr.WriteLine(modules[3]);
                   sr.WriteLine(modules[4]);
                   sr.WriteLine(modules[5]);
                   sr.WriteLine(modules[6]);
                   sr.WriteLine(modules[7]);
                   sr.WriteLine(modules[8]);
                   sr.WriteLine("ASSIGNMENT");
                   sr.WriteLine(modules[9]);
                   sr.WriteLine(modules[10]);
                   sr.WriteLine(modules[11]);
                   sr.WriteLine(modules[12]);
                   sr.WriteLine(modules[13]);
                   sr.WriteLine(modules[14]);
               }
               Console.WriteLine("module created");
               // after module created form closes and menu is initialised
               Form1 reset = new Form1(); 
               reset.Show();
               this.Dispose(false);

           };

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void newmodule_FormClosed(object sender, FormClosedEventArgs e)
        {
            // if form is closed menu is initialised
            Form1 reset = new Form1();
            reset.Show();
            this.Dispose(false);
        }
    }
}
