using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleMaths
{
    public partial class ChooseDifficulty : Form
    {
        public ChooseDifficulty()
        {
            InitializeComponent();
        }

        private void ChooseDifficulty_Load(object sender, EventArgs e)
        {
            string[] arr = Enum.GetNames(typeof(Difficulty));
            //string[] orr = Enum.GetNames(typeof(Operations));
            //listView1.Items.Add(orr[0]);
            //listView1.Items.Add(orr[1]);
            //listView1.Items.Add(orr[2]);
            //listView1.Items.Add(orr[3]);
            checkedListBox1.Items.Add(Operations.ADD);
            checkedListBox1.Items.Add(Operations.SUBTRACT);
            checkedListBox1.Items.Add(Operations.MULTIPLY);
            checkedListBox1.Items.Add(Operations.DIVIDE);
            

            radioButton1.Text = arr[0].ToString();
            radioButton2.Text = arr[1].ToString();
            radioButton3.Text = arr[2].ToString();


        }

        public Difficulty GetOption()
        {

            var check = groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
            return (Difficulty)check.Tag;
        }

        public Operations[] GetSelectedItems()
        {
            Operations[] enums = checkedListBox1.CheckedItems.OfType<Operations>().ToArray();

           

            return enums;

        }



        private void button1_Click(object sender, EventArgs e)
        {
            User.difficulty = GetOption();
            User.operations = GetSelectedItems();
            



            //foreach (int i in listView1.SelectedItems)
            //{
            //    var x = listView1.SelectedItems[i].Tag;
            //}

            Dispose();
            Close();
        }

     
    }
}
