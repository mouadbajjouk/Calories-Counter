namespace CalorieCounterWinForm
{
    public partial class Form1 : Form
    {
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public int GymActivities { get; set; }
        public int BMR { get; set; }
        public int TDEE { get; set; }
        public int CaloriesToAdd { get; set; } = 0;
        public int CaloriesToRemove { get; set; } = 0;
        public int FinalTDEE { get; set; } = 0;
        public int ProteinMacro { get; set; } = 0;
        public int FatMacro { get; set; } = 0;
        public int CarbsMacro { get; set; } = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                Weight = int.Parse(textBox1.Text);
                Height = int.Parse(textBox2.Text);
                Age = int.Parse(textBox3.Text);

                if (Weight == 0 || Height == 0 || Age == 0) throw new InvalidOperationException();

                var result = Math.Round((Weight * 10) + (Height * 6.25) - (Age * 5) + 5);

                BMR = Convert.ToInt32(result);

                label7.Text = BMR.ToString();
            }
            catch
            {
                MessageBox.Show("Incorrect input (eitheir some fields are empty or numbers are in decimal, try to use integers instead)", "Input error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GymActivities = comboBox1.SelectedIndex;

                switch (GymActivities)
                {
                    case 0:
                        TDEE = Convert.ToInt32(Math.Round(BMR * 1.2));
                        break;
                    case 1:
                        TDEE = Convert.ToInt32(Math.Round(BMR * 1.35));
                        break;
                    case 2:
                        TDEE = Convert.ToInt32(Math.Round(BMR * 1.35));
                        break;
                    case 3:
                        TDEE = Convert.ToInt32(Math.Round(BMR * 1.35));
                        break;
                    case 4:
                        TDEE = Convert.ToInt32(Math.Round(BMR * 1.5));
                        break;
                    case 5:
                        TDEE = Convert.ToInt32(Math.Round(BMR * 1.5));
                        break;
                    case 6:
                        TDEE = Convert.ToInt32(Math.Round(BMR * 1.7));
                        break;
                    case 7:
                        TDEE = Convert.ToInt32(Math.Round(BMR * 1.9));
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                MessageBox.Show("", "Selected value error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            label12.Text = TDEE.ToString() + " " + "cal";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                CaloriesToAdd = int.Parse(textBox4.Text);

                if (CaloriesToAdd < 0) throw new InvalidOperationException();

                label14.Text = "Your additional calories : " + CaloriesToAdd.ToString();
            }
            catch
            {
                MessageBox.Show("Try to enter a integer value, or a correct one", "Incorrect value", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                CaloriesToRemove = int.Parse(textBox5.Text);

                if (CaloriesToRemove < 0) throw new InvalidOperationException();

                FinalTDEE = TDEE + CaloriesToAdd - CaloriesToRemove;

                if (FinalTDEE < 0) throw new ArgumentOutOfRangeException();

                label15.Text = "Calories to remove : " + CaloriesToRemove.ToString();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Try to enter a integer value, or a correct one", "Incorrect value", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("With all these values, your TDEE will be less than zero!", "Incorrect values", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FinalTDEE = TDEE + CaloriesToAdd - CaloriesToRemove;

            label17.Text = "Your final TDEE is : " + FinalTDEE.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (FinalTDEE <= 0) throw new ArgumentOutOfRangeException();

                CalculateMacros(comboBox2.SelectedIndex);

                label21.Text = "Protein required per day:" + " " + ProteinMacro;
                label22.Text = "Fat required per day:" + " " + FatMacro;
                label23.Text = "Carbs required per day:" + " " + CarbsMacro;
            }
            catch
            {
                MessageBox.Show("Get your final TDEE first!", "Incorrect calculation", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CalculateMacros(int index)
        {
            int leftCalories;

            switch (index)
            {
                case 0:
                    ProteinMacro = Convert.ToInt32(Math.Round(1.6 * Weight));

                    leftCalories = FinalTDEE - ProteinMacro * 4;

                    FatMacro = Convert.ToInt32(Math.Round(0.5 * Weight));

                    leftCalories -= FatMacro * 9;

                    CarbsMacro = Convert.ToInt32(Math.Round(leftCalories / 4f));
                    break;
                case 1:
                    ProteinMacro = Convert.ToInt32(Math.Round(2.0 * Weight));

                    leftCalories = FinalTDEE - ProteinMacro * 4;

                    FatMacro = Convert.ToInt32(Math.Round(0.5 * Weight));

                    leftCalories -= FatMacro * 9;

                    CarbsMacro = Convert.ToInt32(Math.Round(leftCalories / 4f));
                    break;
                case 2:
                    ProteinMacro = Convert.ToInt32(Math.Round(2.5 * Weight));

                    leftCalories = FinalTDEE - ProteinMacro * 4;

                    FatMacro = Convert.ToInt32(Math.Round(0.5 * Weight));

                    leftCalories -= FatMacro * 9;

                    CarbsMacro = Convert.ToInt32(Math.Round(leftCalories / 4f));
                    break;
                default:
                    break;
            }


        }
    }
}