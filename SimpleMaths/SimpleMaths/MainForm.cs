using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleMaths
{
    public partial class MainForm : Form
    {
        List<Questions> AddItems = new List<Questions>();
        List<Questions> SubtractItems = new List<Questions>();
        List<Questions> MultiplyItems = new List<Questions>();
        List<Questions> DivideItems = new List<Questions>();
        // The last time the timer was started
        private DateTime _startTime = DateTime.MinValue;

        // Time between now and when the timer was started last
        private TimeSpan _currentElapsedTime = TimeSpan.Zero;

        // Time between now and the first time timer was started after a reset
        private TimeSpan _totalElapsedTime = TimeSpan.Zero;

        // Whether or not the timer is currently running
        private bool _timerRunning = false;


        public MainForm()
        {
            InitializeComponent();
            HidePanels();
            button2.Hide();
#if CHEATMODE
            button2.Show();
#endif

        }

        public void HidePanels()
        {
            panelAdd.Hide();
            panelSubtract.Hide();
            panelMultiply.Hide();
            panelDivde.Hide();
        }

        private void menuitemDifficult_Click(object sender, EventArgs e)
        {
            HidePanels();
            ChooseDifficulty frmAbout = new ChooseDifficulty();
            if (frmAbout.ShowDialog() == DialogResult.OK)
            {
                labelDifficulty.Text = User.difficulty.ToString();

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

                _startTime = DateTime.Now;

                // Store the total elapsed time so far
                _totalElapsedTime = _currentElapsedTime;

                timer1.Start();
                _timerRunning = true;
            }

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

        public void AddQuestionLists(Questions q, Operations o)
        {
            
            if (CheckSign(o))
            {
                if(o == Operations.ADD)
                {
                    AddToLabels("labelAdd", q);
                    panelAdd.Show();
                    //var a = labelAdd1.Text;
                    //var b = labelAdd2.Text;
                }else if (o == Operations.SUBTRACT)
                {
                    AddToLabels("labelSubtract", q);
                    panelSubtract.Show();
                }else if (o == Operations.MULTIPLY)
                {
                    AddToLabels("labelMultiply", q);
                    panelMultiply.Show();

                }else if (o == Operations.DIVIDE)
                {
                    AddToLabels("labelDivide", q);
                    panelDivde.Show();
                }
                User.operations = User.operations.Where(val => val != o).ToArray();
            }
                
        }

        public void AddToLabels(string labelName, Questions q)
        {
            var labels = new List<Label>(); 
            if(labelName == "labelAdd")
            {
                labels = new List<Label>() { labelAdda, labelAddb, labelAddc, labelAddd, labelAdde, labelAddf, labelAddg, labelAddh, labelAddi, labelAddj };

            }
            else if (labelName == "labelSubtract")
            {
                labels = new List<Label>() { labelSubtract1, labelSubtract2, labelSubtract3, labelSubtract4, labelSubtract5, labelSubtract6, labelSubtract7, labelSubtract8, labelSubtract9, labelSubtract10 };
            }else if (labelName == "labelMultiply")
            {
                labels = new List<Label>() { labelMultiply1, labelMultiply2, labelMultiply3, labelMultiply4, labelMultiply5, labelMultiply6, labelMultiply7, labelMultiply8, labelMultiply9, labelMultiply10 };
            }else if (labelName == "labelDivide")
            {
                labels = new List<Label>() { labelDivide1, labelDivide2, labelDivide3, labelDivide4, labelDivide5, labelDivide6, labelDivide7, labelDivide8, labelDivide9, labelDivide10 };
            }
            foreach (var l in labels)
            {
                l.Text = CheckQuestionList(q);
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
                    while(FindInList(y))
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
                    if(y == null)
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

        public string AnswerAdd(string question)
        {
            return User.AdditionList.Find(q => q.QuestionAdd == question).ToString();
        }

        public string AnswerSubtract(string question)
        {
            return User.AdditionList.Find(q => q.QuestionSubtract == question).ToString();
        }

        public string AnswerMultiply(string question)
        {
            return User.AdditionList.Find(q => q.QuestionMultiply == question).ToString();
        }

        public string AnswerDivide(string question)
        {
            return User.AdditionList.Find(q => q.QuestionDivde == question).ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            _timerRunning = false;

            if (panelAdd.Visible == true)
            {

                lblAddScore.Text = "Your Score Is: " + GetScore(GetAnswersCal(Operations.ADD), GetUserAnswers(Operations.ADD));
            }

            if (panelSubtract.Visible == true)
            {
                lblSubtractScore.Text = "Your Score Is: " + GetScore(GetAnswersCal(Operations.SUBTRACT), GetUserAnswers(Operations.SUBTRACT));
            }

            if (panelMultiply.Visible == true)
            {
                lblMultiplyScore.Text = "Your Score Is: " + GetScore(GetAnswersCal(Operations.MULTIPLY), GetUserAnswers(Operations.MULTIPLY));
            }

            if (panelDivde.Visible == true)
            {
                lblDivideScore.Text = "Your Score Is: " + GetScore(GetAnswersCal(Operations.DIVIDE), GetUserAnswers(Operations.DIVIDE));
            }

            SendEmail();
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
            string body = "Addidition Score: " + lblAddScore.Text + System.Environment.NewLine +
                "Subtraction Score: " + lblSubtractScore.Text + System.Environment.NewLine +
                "Multiplication Score: " + lblMultiplyScore.Text + System.Environment.NewLine +
                "Division Score: " + lblDivideScore.Text + System.Environment.NewLine + User.difficulty.ToString() +
                Environment.NewLine +  _currentElapsedTimeDisplay.Text;

            MailMessage mm = new MailMessage("info@nishank.co.uk", "info@nishank.co.uk", "Test Results", body);
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
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
            } else if (o == Operations.SUBTRACT)
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
            } else if (o == Operations.MULTIPLY)
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
            } else if (o == Operations.DIVIDE)
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
            }else if (o == Operations.SUBTRACT)
            {
                var x = User.SubtractionList.ToArray<Questions>();
                int i = 0;
                foreach (Questions q in x)
                {
                    test[i] = q.Answer.ToString();
                    i++;
                }
            } else if (o == Operations.MULTIPLY)
            {
                var x = User.MultiplyList.ToArray<Questions>();
                int i = 0;
                foreach (Questions q in x)
                {
                    test[i] = q.Answer.ToString();
                    i++;
                }
            } else if (o == Operations.DIVIDE)
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

        private string[] GetAddAnswersCal()
        {
            var x = User.AdditionList.ToArray<Questions>();
            string[] test = new string[10];

            int i = 0;
            foreach (Questions q in x)
            {
                test[i] = q.Answer.ToString();
                i++;
            }

            return test;
        }

        private string[] GetAddAnswers()
        {
            string[] answers = new string[] {   textBoxAdd1.Text,
                                                textBoxAdd2.Text,
                                                textBoxAdd3.Text,
                                                textBoxAdd4.Text,
                                                textBoxAdd5.Text,
                                                textBoxAdd6.Text,
                                                textBoxAdd7.Text,
                                                textBoxAdd8.Text,
                                                textBoxAdd9.Text,
                                                textBoxAdd10.Text
                                             };

            return answers;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] r = new string[10];

            if (panelAdd.Visible == true)
            {

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

            }

            if (panelSubtract.Visible == true)
            {
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
                
            }

            if (panelMultiply.Visible == true)
            {
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


            }

            if (panelDivde.Visible == true)
            {
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

        private void User_Validating(object sender, CancelEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            int i = 0;
            if (!string.IsNullOrEmpty(textbox.Text) &&
                 !int.TryParse(textbox.Text, out i)
              )
            {
                MessageBox.Show("Enter a number!!!!");
                textbox.Text = "";
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // We do this to chop off any stray milliseconds resulting from 
            // the Timer's inherent inaccuracy, with the bonus that the 
            // TimeSpan.ToString() method will now show correct HH:MM:SS format
            var timeSinceStartTime = DateTime.Now - _startTime;
            timeSinceStartTime = new TimeSpan(timeSinceStartTime.Hours,
                                              timeSinceStartTime.Minutes,
                                              timeSinceStartTime.Seconds);

            // The current elapsed time is the time since the start button was
            // clicked, plus the total time elapsed since the last reset
            _currentElapsedTime = timeSinceStartTime + _totalElapsedTime;

            // These are just two Label controls which display the current 
            // elapsed time and total elapsed time
            //_totalElapsedTimeDisplay.Text = _currentElapsedTime.ToString();
            _currentElapsedTimeDisplay.Text = timeSinceStartTime.ToString();
        }
    }
}
