using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SimpleMaths;

namespace SimpleMathWPF
{
    /// <summary>
    /// Interaction logic for ChooseDifficulty.xaml
    /// </summary>
    public partial class ChooseDifficulty : Window
    {
        public ChooseDifficulty()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] arr = Enum.GetNames(typeof(Difficulty));
            //string[] orr = Enum.GetNames(typeof(Operations));
            //listView1.Items.Add(orr[0]);
            //listView1.Items.Add(orr[1]);
            //listView1.Items.Add(orr[2]);
            //listView1.Items.Add(orr[3]);
            opAdd.Content = Operations.ADD.ToString();
            opSubtract.Content = Operations.SUBTRACT.ToString();
            opMultiply.Content = Operations.MULTIPLY.ToString();
            opDivide.Content = Operations.DIVIDE.ToString();


            rdEasy.Content = arr[0].ToString();
            rdMedium.Content = arr[1].ToString();
            rdHard.Content = arr[2].ToString();
        }

        public Difficulty GetOption()
        {
            Difficulty d = new Difficulty();
            

            if(rdEasy.IsChecked == true)
            {
                d = Difficulty.EASY;
            }else if (rdMedium.IsChecked == true)
            {
                d = Difficulty.MEDIUM;
            }
            if (rdHard.IsChecked == true)
            {
                d = Difficulty.HARD;
            }

            return d;
        }

        public Operations[] GetSelectedItems()
        {
            List<Operations> enums = new List<Operations>();

            if(opAdd.IsChecked == true)
            {
                enums.Add(Operations.ADD);
            }

            if (opSubtract.IsChecked == true)
            {
                enums.Add(Operations.SUBTRACT);
            }

            if (opMultiply.IsChecked == true)
            {
                enums.Add(Operations.MULTIPLY);
            }

            if (opDivide.IsChecked == true)
            {
                enums.Add(Operations.DIVIDE);
            }



            return enums.ToArray();

        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            User.difficulty = GetOption();
            User.operations = GetSelectedItems();
            Close();


        }
    }
}
