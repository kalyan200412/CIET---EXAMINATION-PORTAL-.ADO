using System;
using System.Windows.Forms;

namespace ado2
{
    public partial class Form24 : Form
    {
        private int currentQuestionIndex = 0;
        private int score = 0;
        private int[] userAnswers;

        private readonly string[] questions = {
         "Q1. What keyword is used to declare a class in C#?",
         "Q2. Which method is used to open a database connection in ADO.NET?",
         "Q3. What is the default access modifier for a class in C#?",
         "Q4. Which C# keyword is used to handle exceptions?",
         "Q5. What method is used to execute a SQL command in ADO.NET?",
         "Q6. Which of these is a value type in C#?",
         "Q7. What is the purpose of the 'using' statement in C#?",
         "Q8. Which method in ADO.NET is used to fill a DataSet?",
         "Q9. What is the base class for all data access in ADO.NET?",
        "Q10. Which C# keyword is used to define an interface?"
        };

        private readonly string[][] options = {
            new string[] { "class", "struct", "interface", "object" },
            new string[] { "Open()", "Connect()", "Begin()", "Start()" },
            new string[] { "private", "public", "protected", "internal" },
            new string[] { "try", "catch", "throw", "finally" },
            new string[] { "ExecuteQuery()", "ExecuteNonQuery()", "RunCommand()", "ExecuteCommand()" },
            new string[] { "int", "string", "List", "Dictionary" },
            new string[] { "To include namespaces", "To create objects", "To manage resources", "To handle exceptions" },
            new string[] { "Fill()", "Load()", "Execute()", "Add()" },
            new string[] { "DataSet", "SqlCommand", "SqlConnection", "DataTable" },
            new string[] { "class", "interface", "enum", "struct" }
        };

        private readonly int[] correctAnswers = { 0, 0, 1, 0, 1, 0, 2, 0, 0, 1 };

        public Form24()
        {
            InitializeComponent();
            userAnswers = new int[questions.Length];
            for (int i = 0; i < userAnswers.Length; i++)
            {
                userAnswers[i] = -1;
            }
            LoadQuestion();
        }

        private void LoadQuestion()
        {
            if (currentQuestionIndex < questions.Length)
            {
                label1.Text = questions[currentQuestionIndex];
                radioButton1.Text = options[currentQuestionIndex][0];
                radioButton2.Text = options[currentQuestionIndex][1];
                radioButton3.Text = options[currentQuestionIndex][2];
                radioButton4.Text = options[currentQuestionIndex][3];

                radioButton1.Checked = userAnswers[currentQuestionIndex] == 0;
                radioButton2.Checked = userAnswers[currentQuestionIndex] == 1;
                radioButton3.Checked = userAnswers[currentQuestionIndex] == 2;
                radioButton4.Checked = userAnswers[currentQuestionIndex] == 3;

                button3.Enabled = currentQuestionIndex > 0;
                button2.Enabled = currentQuestionIndex < questions.Length - 1;
            }
        }

        private void SaveUserAnswer()
        {
            if (radioButton1.Checked) userAnswers[currentQuestionIndex] = 0;
            if (radioButton2.Checked) userAnswers[currentQuestionIndex] = 1;
            if (radioButton3.Checked) userAnswers[currentQuestionIndex] = 2;
            if (radioButton4.Checked) userAnswers[currentQuestionIndex] = 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!radioButton1.Checked && !radioButton2.Checked && !radioButton3.Checked && !radioButton4.Checked)
            {
                MessageBox.Show("Please select an answer before submitting!", "No Answer Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                SaveUserAnswer();
                MessageBox.Show("Your answer has been submitted successfully!", "Answer Submitted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (currentQuestionIndex < questions.Length - 1)
                {
                    currentQuestionIndex++;
                    LoadQuestion();
                }
                else
                {
                    DialogResult result = MessageBox.Show("You have completed all the questions. Are you sure you want to submit all your answers?",
                                                          "Submit All Answers",
                                                          MessageBoxButtons.YesNo,
                                                          MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        button1_Click(sender, e);
                    }
                    else
                    {
                        MessageBox.Show("You can review your answers before submitting.", "Review Answers", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Do you want to go back to the previous question?", "Go Back", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SaveUserAnswer();
                if (currentQuestionIndex > 0)
                {
                    currentQuestionIndex--;
                    LoadQuestion();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to go to the next question?", "Next Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                SaveUserAnswer();
                if (currentQuestionIndex < questions.Length - 1)
                {
                    currentQuestionIndex++;
                    LoadQuestion();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveUserAnswer();
            score = 0;

            for (int i = 0; i < userAnswers.Length; i++)
            {
                if (userAnswers[i] == correctAnswers[i])
                {
                    score++;
                }
            }

            MessageBox.Show($"Your score: {score} out of {questions.Length}", "Final Score", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (score >= 4)
            {
                MessageBox.Show("Congratulations! You passed the test!", "Pass", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Form18 form18 = new Form18();
                form18.ShowDialog();
            }
            else
            {
                MessageBox.Show("You did not qualify. Please study well.", "Test Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Hide();
                Form18 form18 = new Form18();
                form18.ShowDialog();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton2_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton3_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton4_CheckedChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }

        private void buttonLeaveExam_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to leave the exam?", "Leave Exam", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to leave the exam?", "Leave Exam", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                MessageBox.Show("You have left the exam.", "Exam Exited", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Form18 form18 = new Form18();
                form18.ShowDialog();
            }
            else
            {
                MessageBox.Show("Continue with your exam.", "Exam Resumed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void Form24_Load(object sender, EventArgs e) { }

        private void label1_Click_1(object sender, EventArgs e)
        {
            // Handle label1 click event here
        }

        private void label2_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"Your score: {score} out of {questions.Length}", "Final Score", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label5_Click(object sender, EventArgs e) { }
        private void label2_Click_1(object sender, EventArgs e) { }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string resultMessage = "Final Results:\n\n";
            for (int i = 0; i < questions.Length; i++)
            {
                string userAnswerText = userAnswers[i] >= 0 ? options[i][userAnswers[i]] : "No answer selected";
                string correctAnswerText = options[i][correctAnswers[i]];

                resultMessage += $"Question {i + 1}: {questions[i]}\n";
                resultMessage += $"Your Answer: {userAnswerText}\n";
                resultMessage += $"Correct Answer: {correctAnswerText}\n\n";
            }

            resultMessage += $"Your total score: {score} out of {questions.Length}";

            MessageBox.Show(resultMessage, "Final Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label2_Click_2(object sender, EventArgs e)
        {
           
        }
    }
}
