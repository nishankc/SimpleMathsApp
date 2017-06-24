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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleMaths;
using System.Net.Mail;

namespace SimpleMathWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Questions> AddItems = new List<Questions>();
        List<Questions> SubtractItems = new List<Questions>();
        List<Questions> MultiplyItems = new List<Questions>();
        List<Questions> DivideItems = new List<Questions>();

        public MainWindow()
        {
            InitializeComponent();
            HidePanels();
        }

        public void HidePanels()
        {
            AddColumn.Width = new GridLength(0);
            SubtractColumn.Width = new GridLength(0);
            MultiplyColumn.Width = new GridLength(0);
            DivideColumn.Width = new GridLength(0);
            ButtonColumn.Width = new GridLength(0);
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            HidePanels();
            ChooseDifficulty frm = new ChooseDifficulty();
            frm.ShowDialog();


            if (frm.DialogResult.HasValue && frm.DialogResult.Value)
            {


                ClearLocalLists();
                var q = new Questions(User.difficulty);

                if (CheckSign(Operations.ADD))
                {
                    AddQuestionLists(q, Operations.ADD);
                }
                if (CheckSign(Operations.SUBTRACT))
                {
                    AddQuestionLists(q, Operations.SUBTRACT);
                }
                if (CheckSign(Operations.MULTIPLY))
                {
                    AddQuestionLists(q, Operations.MULTIPLY);
                }
                if (CheckSign(Operations.DIVIDE))
                {
                    AddQuestionLists(q, Operations.DIVIDE);
                }
                AddToQuestionLists();

                ButtonColumn.Width = new GridLength(1, GridUnitType.Star);
            }



        }


        private void ClearLocalLists()
        {
            if (AddItems.Count > 0)
                AddItems.Clear();
            if (SubtractItems.Count > 0)
                SubtractItems.Clear();
            if (MultiplyItems.Count > 0)
                MultiplyItems.Clear();
            if (DivideItems.Count > 0)
                DivideItems.Clear();
        }

        private static bool CheckSign(Operations o)
        {
            return User.operations.Contains<Operations>(o);
        }

        private void AddToQuestionLists()
        {
            User.AdditionList = AddItems;
            User.SubtractionList = SubtractItems;
            User.MultiplyList = MultiplyItems;
            User.DivideList = DivideItems;
        }

        public void AddQuestionLists(Questions q, Operations o)
        {

            if (CheckSign(o))
            {
                if (o == Operations.ADD)
                {
                    AddToLabels("labelAdd", q);
                    AddColumn.Width = new GridLength(1, GridUnitType.Star);
                    //var a = labelAdd1.Text;
                    //var b = labelAdd2.Text;
                }
                else if (o == Operations.SUBTRACT)
                {
                    AddToLabels("labelSubtract", q);
                    SubtractColumn.Width = new GridLength(1, GridUnitType.Star);
                }
                else if (o == Operations.MULTIPLY)
                {
                    AddToLabels("labelMultiply", q);
                    MultiplyColumn.Width = new GridLength(1, GridUnitType.Star);

                }
                else if (o == Operations.DIVIDE)
                {
                    AddToLabels("labelDivide", q);
                    DivideColumn.Width = new GridLength(1, GridUnitType.Star);
                }
                User.operations = User.operations.Where(val => val != o).ToArray();
            }

        }

        public void AddToLabels(string labelName, Questions q)
        {
            var labels = new List<Label>();
            if (labelName == "labelAdd")
            {
                labels = new List<Label>() { lblAdd1, lblAdd2, lblAdd3, lblAdd4, lblAdd5, lblAdd6, lblAdd7, lblAdd8, lblAdd9, lblAdd10 };

            }
            else if (labelName == "labelSubtract")
            {
                labels = new List<Label>() { lblSubtract1, lblSubtract2, lblSubtract3, lblSubtract4, lblSubtract5, lblSubtract6, lblSubtract7, lblSubtract8, lblSubtract9, lblSubtract10 };
            }
            else if (labelName == "labelMultiply")
            {
                labels = new List<Label>() { lblMultiply1, lblMultiply2, lblMultiply3, lblMultiply4, lblMultiply5, lblMultiply6, lblMultiply7, lblMultiply8, lblMultiply9, lblMultiply10 };
            }
            else if (labelName == "labelDivide")
            {
                labels = new List<Label>() { lblDivide1, lblDivide2, lblDivide3, lblDivide4, lblDivide5, lblDivide6, lblDivide7, lblDivide8, lblDivide9, lblDivide10 };
            }
            foreach (var l in labels)
            {
                l.Content = CheckQuestionList(q);
            }

            labels.Clear();

        }

        public string CheckQuestionList(Questions text)
        {
            var x = "";
            Questions q1 = new Questions();
            q1 = text;
            if (CheckSign(Operations.ADD))
            {
                if (AddItems.Count == 0)
                {
                    AddItems.Add(text);
                    x = text.QuestionAdd;

                }
                else
                {
                    var y = text.QuestionAdd;
                    while (FindInList(y))
                    {
                        q1 = new Questions(User.difficulty);
                        y = q1.QuestionAdd;

                    }
                    x = q1.QuestionAdd;
                    AddItems.Add(q1);
                }
            }
            else if (CheckSign(Operations.SUBTRACT))
            {
                if (SubtractItems.Count == 0)
                {
                    q1 = new Questions(User.difficulty);
                    SubtractItems.Add(q1);
                    x = q1.QuestionSubtract;

                }
                else
                {
                    var y = text.QuestionSubtract;
                    if (y == null)
                    {
                        q1 = new Questions(User.difficulty);
                        y = q1.QuestionSubtract;
                    }
                    while (FindInList(y))
                    {
                        q1 = new Questions(User.difficulty);
                        y = q1.QuestionSubtract;

                    }
                    x = q1.QuestionSubtract;
                    SubtractItems.Add(q1);
                }

            }

            else if (CheckSign(Operations.MULTIPLY))
            {
                if (MultiplyItems.Count == 0)
                {
                    q1 = new Questions(User.difficulty);
                    MultiplyItems.Add(q1);
                    x = q1.QuestionMultiply;

                }
                else
                {
                    var y = text.QuestionMultiply;
                    if (y == null)
                    {
                        q1 = new Questions(User.difficulty);
                        y = q1.QuestionMultiply;
                    }
                    while (FindInList(y))
                    {
                        q1 = new Questions(User.difficulty);
                        y = q1.QuestionMultiply;

                    }
                    x = q1.QuestionMultiply;
                    MultiplyItems.Add(q1);
                }

            }
            else if (CheckSign(Operations.DIVIDE))
            {
                if (DivideItems.Count == 0)
                {
                    q1 = new Questions(User.difficulty);
                    DivideItems.Add(q1);
                    x = q1.QuestionDivde;

                }
                else
                {
                    var y = text.QuestionDivde;
                    if (y == null)
                    {
                        q1 = new Questions(User.difficulty);
                        y = q1.QuestionDivde;
                    }
                    while (FindInList(y))
                    {
                        q1 = new Questions(User.difficulty);
                        y = q1.QuestionDivde;

                    }
                    x = q1.QuestionDivde;
                    DivideItems.Add(q1);
                }

            }

            return x;



        }

        public bool FindInList(string text)
        {
            bool test = false;
            if (CheckSign(Operations.ADD))
                test = AddItems.Any(x => x.QuestionAdd == text);
            else if (CheckSign(Operations.SUBTRACT))
                test = SubtractItems.Any(x => x.QuestionSubtract == text);
            else if (CheckSign(Operations.MULTIPLY))
                test = MultiplyItems.Any(x => x.QuestionMultiply == text);
            else if (CheckSign(Operations.DIVIDE))
                test = DivideItems.Any(x => x.QuestionDivde == text);
            return test;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            AddScore.Content = "Your Score Is: " + GetScore(GetAnswersCal(Operations.ADD), GetUserAnswers(Operations.ADD));


            SubtractScore.Content = "Your Score Is: " + GetScore(GetAnswersCal(Operations.SUBTRACT), GetUserAnswers(Operations.SUBTRACT));


            MultiplyScore.Content = "Your Score Is: " + GetScore(GetAnswersCal(Operations.MULTIPLY), GetUserAnswers(Operations.MULTIPLY));



            DivideScore.Content = "Your Score Is: " + GetScore(GetAnswersCal(Operations.DIVIDE), GetUserAnswers(Operations.DIVIDE));


            SendEmail();

        }

        private string GetScore(string[] comp, string[] user)
        {
            int score = 10;

            for (int i = 0; i < comp.Length; i++)
            {
                if (comp[i] != user[i])
                    score--;
            }

            return score.ToString();
        }

        private string[] GetUserAnswers(Operations o)
        {
            string[] answers = new string[10];
            if (o == Operations.ADD)
            {
                answers[0] = textBoxAdd1.Text;
                answers[1] = textBoxAdd2.Text;
                answers[2] = textBoxAdd3.Text;
                answers[3] = textBoxAdd4.Text;
                answers[4] = textBoxAdd5.Text;
                answers[5] = textBoxAdd6.Text;
                answers[6] = textBoxAdd7.Text;
                answers[7] = textBoxAdd8.Text;
                answers[8] = textBoxAdd9.Text;
                answers[9] = textBoxAdd10.Text;
            }
            else if (o == Operations.SUBTRACT)
            {
                answers[0] = textBoxSubtract1.Text;
                answers[1] = textBoxSubtract2.Text;
                answers[2] = textBoxSubtract3.Text;
                answers[3] = textBoxSubtract4.Text;
                answers[4] = textBoxSubtract5.Text;
                answers[5] = textBoxSubtract6.Text;
                answers[6] = textBoxSubtract7.Text;
                answers[7] = textBoxSubtract8.Text;
                answers[8] = textBoxSubtract9.Text;
                answers[9] = textBoxSubtract10.Text;
            }
            else if (o == Operations.MULTIPLY)
            {
                answers[0] = textBoxMultiply1.Text;
                answers[1] = textBoxMultiply2.Text;
                answers[2] = textBoxMultiply3.Text;
                answers[3] = textBoxMultiply4.Text;
                answers[4] = textBoxMultiply5.Text;
                answers[5] = textBoxMultiply6.Text;
                answers[6] = textBoxMultiply7.Text;
                answers[7] = textBoxMultiply8.Text;
                answers[8] = textBoxMultiply9.Text;
                answers[9] = textBoxMultiply10.Text;
            }
            else if (o == Operations.DIVIDE)
            {
                answers[0] = textBoxDivide1.Text;
                answers[1] = textBoxDivide2.Text;
                answers[2] = textBoxDivide3.Text;
                answers[3] = textBoxDivide4.Text;
                answers[4] = textBoxDivide5.Text;
                answers[5] = textBoxDivide6.Text;
                answers[6] = textBoxDivide7.Text;
                answers[7] = textBoxDivide8.Text;
                answers[8] = textBoxDivide9.Text;
                answers[9] = textBoxDivide10.Text;
            }

            return answers;
        }

        private string[] GetAnswersCal(Operations o)
        {
            string[] test = new string[10];
            if (o == Operations.ADD)
            {
                var x = User.AdditionList.ToArray<Questions>();
                //Array.Reverse(x);
                int i = 0;
                foreach (Questions q in x)
                {
                    test[i] = q.Answer.ToString();
                    i++;
                }
            }
            else if (o == Operations.SUBTRACT)
            {
                var x = User.SubtractionList.ToArray<Questions>();
                int i = 0;
                foreach (Questions q in x)
                {
                    test[i] = q.Answer.ToString();
                    i++;
                }
            }
            else if (o == Operations.MULTIPLY)
            {
                var x = User.MultiplyList.ToArray<Questions>();
                int i = 0;
                foreach (Questions q in x)
                {
                    test[i] = q.Answer.ToString();
                    i++;
                }
            }
            else if (o == Operations.DIVIDE)
            {
                var x = User.DivideList.ToArray<Questions>();
                int i = 0;
                foreach (Questions q in x)
                {
                    test[i] = q.Answer.ToString();
                    i++;
                }
            }

            return test;
        }

        private void SendEmail()
        {
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.34sp.com";
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("info@nishank.co.uk", "babykomal");
            string body = "Addidition Score: " + AddScore.Content + System.Environment.NewLine +
                "Subtraction Score: " + SubtractScore.Content + System.Environment.NewLine +
                "Multiplication Score: " + MultiplyScore.Content + System.Environment.NewLine +
                "Division Score: " + DivideScore.Content + System.Environment.NewLine + User.difficulty.ToString();
            //+    Environment.NewLine + _currentElapsedTimeDisplay.Text;

            MailMessage mm = new MailMessage("info@nishank.co.uk", "info@nishank.co.uk", "Test Results", body);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string[] r = new string[10];

            

                r = GetAnswersCal(Operations.ADD);
                textBoxAdd1.Text = r[0];
                textBoxAdd2.Text = r[1];
                textBoxAdd3.Text = r[2];
                textBoxAdd4.Text = r[3];
                textBoxAdd5.Text = r[4];
                textBoxAdd6.Text = r[5];
                textBoxAdd7.Text = r[6];
                textBoxAdd8.Text = r[7];
                textBoxAdd9.Text = r[8];
                textBoxAdd10.Text = r[9];

            
                r = GetAnswersCal(Operations.SUBTRACT);
                textBoxSubtract1.Text = r[0];
                textBoxSubtract2.Text = r[1];
                textBoxSubtract3.Text = r[2];
                textBoxSubtract4.Text = r[3];
                textBoxSubtract5.Text = r[4];
                textBoxSubtract6.Text = r[5];
                textBoxSubtract7.Text = r[6];
                textBoxSubtract8.Text = r[7];
                textBoxSubtract9.Text = r[8];
                textBoxSubtract10.Text = r[9];

            
                r = GetAnswersCal(Operations.MULTIPLY);
                textBoxMultiply1.Text = r[0];
                textBoxMultiply2.Text = r[1];
                textBoxMultiply3.Text = r[2];
                textBoxMultiply4.Text = r[3];
                textBoxMultiply5.Text = r[4];
                textBoxMultiply6.Text = r[5];
                textBoxMultiply7.Text = r[6];
                textBoxMultiply8.Text = r[7];
                textBoxMultiply9.Text = r[8];
                textBoxMultiply10.Text = r[9];


           
                r = GetAnswersCal(Operations.DIVIDE);
                textBoxDivide1.Text = r[0];
                textBoxDivide2.Text = r[1];
                textBoxDivide3.Text = r[2];
                textBoxDivide4.Text = r[3];
                textBoxDivide5.Text = r[4];
                textBoxDivide6.Text = r[5];
                textBoxDivide7.Text = r[6];
                textBoxDivide8.Text = r[7];
                textBoxDivide9.Text = r[8];
                textBoxDivide10.Text = r[9];

            


        }


    }
}
