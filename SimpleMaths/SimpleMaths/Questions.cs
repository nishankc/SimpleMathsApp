using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleMaths
{
    public class Questions
    {
        public int FirstNumber { get; set; }
        public int SecondNumber { get; set; }
        public Operations[] Sign { get; set; }
        public int Answer { get; set; }
        public string QuestionAdd { get; set; }
        public string QuestionSubtract { get; set; }
        public string QuestionMultiply { get; set; }
        public string QuestionDivde { get; set; }


        public Questions()
        {

        }

        public Questions(Difficulty difficulty)
        {
            this.Sign = User.operations;
            var x = GeneraterRandomNumber(difficulty);
            var y = GeneraterRandomNumber(difficulty);
            int[] arr = new int[2];

            arr = CheckIfEqual(x, y, difficulty);




            if (CheckAdd())
            {
                this.FirstNumber = arr[0];
                this.SecondNumber = arr[1];
                this.QuestionAdd = FirstNumber.ToString() + "+" + SecondNumber.ToString() + "=";
                this.Answer = GetAnswer(Sign[0], FirstNumber, SecondNumber);
            }
            else if (CheckSubtract())
            {
                this.FirstNumber = arr[0];
                this.SecondNumber = arr[1];
                this.QuestionSubtract = FirstNumber.ToString() + "-" + SecondNumber.ToString() + "=";
                this.Answer = GetAnswer(Sign[0], FirstNumber, SecondNumber);
            }
            else if (CheckMultiply())
            {
                //arr = CheckForDivide(arr, difficulty);
                this.FirstNumber = arr[0];
                this.SecondNumber = arr[1];
                this.QuestionMultiply = FirstNumber.ToString() + "X" + SecondNumber.ToString() + "=";
                this.Answer = GetAnswer(Sign[0], FirstNumber, SecondNumber);
            }
            else if (CheckDivide())
            {
                arr = CheckForDivide(arr, difficulty);
                this.FirstNumber = arr[0];
                this.SecondNumber = arr[1];
                this.QuestionDivde = FirstNumber.ToString() + "÷" + SecondNumber.ToString() + "=";
                this.Answer = GetAnswer(Sign[0], FirstNumber, SecondNumber);
            }






        }

        //private static bool CheckOpera(Operations operation)
        //{
        //    bool x = false;
        //    if (operation == Operations.ADD)
        //        x = User.operations.Contains<Operations>(operation);
        //    else if (operation == Operations.SUBTRACT)
        //        x = User.operations.Contains<Operations>(operation);
        //    else if (operation == Operations.MULTIPLY)
        //        x = User.operations.Contains<Operations>(operation);
        //    else if (operation == Operations.DIVIDE)
        //        x = User.operations.Contains<Operations>(operation);


        //    return x;
        ////}

        //private bool CheckIfNull(int i, Operations operation)
        //{
        //    var value = false;
        //    if (Sign[i] != null)
        //    {
        //        value = true;

        //        if ((Operations)Sign[i] == operation)
        //        {
        //            value = true;
        //        }
        //    }

        //    return value;

        //}

        private bool CheckOperation(Operations operation)
        {
            return User.operations.Contains<Operations>(operation);
        }


        private int[] CheckForDivide(int[] arr, Difficulty difficulty)
        {
            var r = new Random();

            if (difficulty == Difficulty.EASY)
            {
                if (arr[1] > 10)
                {
                    arr[1] = r.Next(1, 9);
                }

                while (arr[0] % arr[1] != 0)
                {
                    arr[0] = arr[1] * r.Next(1, 12);
                }
            }else if (difficulty == Difficulty.MEDIUM)
            {
                if (arr[1] > 10)
                {
                    arr[1] = r.Next(1, 9);
                }

                while (arr[0] % arr[1] != 0)
                {
                    arr[0] = arr[1] * r.Next(1, 15);
                }
            }else if (difficulty == Difficulty.HARD)
            {
                if (arr[1] > 10)
                {
                    arr[1] = r.Next(1, 20);
                }

                while (arr[0] % arr[1] != 0)
                {
                    arr[0] = arr[1] * r.Next(1, 12);
                }
            }

            return arr;


        }

        private int[] CheckIfEqual(int x, int y, Difficulty difficulty)
        {
            var r = new Random();
            int[] arr = new int[] { x, y };

            if (difficulty == Difficulty.EASY)
            {
                while (arr[0] == arr[1])
                {
                    arr[0] = r.Next(arr[0], 20);
                    if (arr[0] <= arr[1])
                    {
                        arr[0] = r.Next(y + 1, 20);
                    }


                }
            }else if (difficulty == Difficulty.MEDIUM)
            {
                while (arr[0] == arr[1])
                {
                    arr[0] = r.Next(arr[0], 50);
                    if (arr[0] <= arr[1])
                    {
                        arr[0] = r.Next(y + 1, 50);
                    }


                }
            }else if (difficulty == Difficulty.HARD)
            {
                while (arr[0] == arr[1])
                {
                    arr[0] = r.Next(arr[0], 100);
                    if (arr[0] <= arr[1])
                    {
                        arr[0] = r.Next(y + 1, 100);
                    }


                }
            }



            return arr;
        }

        public bool CalcIsPrime(int number)
        {

            if (number == 1) return false;
            if (number == 2) return true;

            if (number % 2 == 0) return false; // Even number     

            for (int i = 2; i < number; i++)
            { // Advance from two to include correct calculation for '4'
                if (number % i == 0) return false;
            }

            return true;

        }


        public int GetAnswer(Operations operation, int firstNumber, int secondNumber)
        {
            int answer = 0;

            if (operation == Operations.ADD)
            {
                answer = firstNumber + secondNumber;
            }

            if (operation == Operations.SUBTRACT)
            {
                answer = firstNumber - secondNumber;
            }

            if (operation == Operations.MULTIPLY)
            {
                answer = firstNumber * secondNumber;
            }

            if (operation == Operations.DIVIDE)
            {
                answer = firstNumber / secondNumber;
            }

            return answer;
        }


        public int GeneraterRandomNumber(Difficulty difficulty)
        {
            var levels = GetDifficultyLevel(difficulty);
            var ran = new Random().Next(levels[0], levels[1]);

            return ran;

        }

        private int[] GetDifficultyLevel(Difficulty difficulty)
        {
            int[] arr = new int[2];
            if (difficulty == Difficulty.EASY)
            {
                arr[0] = 1;
                arr[1] = 20;

                if (CheckOperation(Operations.MULTIPLY))
                {
                    arr[0] = 1;
                    arr[1] = 12;
                }
            }
            else if (difficulty == Difficulty.MEDIUM)
            {
                arr[0] = 20;
                arr[1] = 50;
                if (CheckOperation(Operations.MULTIPLY))
                {
                    arr[0] = 1;
                    arr[1] = 15;
                }
            }
            else if (difficulty == Difficulty.HARD)
            {
                arr[0] = 40;
                arr[1] = 100;
                if (CheckOperation(Operations.MULTIPLY))
                {
                    arr[0] = 1;
                    arr[1] = 20;
                }
            }

            return arr;
        }

        private static bool CheckAdd()
        {
            return User.operations.Contains<Operations>(Operations.ADD);
        }

        private static bool CheckSubtract()
        {
            return User.operations.Contains<Operations>(Operations.SUBTRACT);
        }

        private static bool CheckMultiply()
        {
            return User.operations.Contains<Operations>(Operations.MULTIPLY);
        }

        private static bool CheckDivide()
        {
            return User.operations.Contains<Operations>(Operations.DIVIDE);
        }
    }
}
