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

        private void GetBMR()
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
            GymActivities = comboBox1.SelectedIndex;
        }

        private void SwitchOnGymActivities()
        {
            try
            {
                TDEE = GymActivities switch
                {
                    0 => Convert.ToInt32(Math.Round(BMR * 1.2)),
                    1 => Convert.ToInt32(Math.Round(BMR * 1.35)),
                    2 => Convert.ToInt32(Math.Round(BMR * 1.35)),
                    3 => Convert.ToInt32(Math.Round(BMR * 1.35)),
                    4 => Convert.ToInt32(Math.Round(BMR * 1.5)),
                    5 => Convert.ToInt32(Math.Round(BMR * 1.5)),
                    6 => Convert.ToInt32(Math.Round(BMR * 1.7)),
                    7 => Convert.ToInt32(Math.Round(BMR * 1.9)),
                    _ => throw new ArgumentNullException(),
                };
            }
            catch (Exception)
            {
                MessageBox.Show("", "Selected value error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void GetTDEE()
        {
            SwitchOnGymActivities();
            label12.Text = TDEE.ToString() + " " + "cal";
        }

        private void GetCaloriesToAdd()
        {
            try
            {
                CaloriesToAdd = string.IsNullOrWhiteSpace(textBox4.Text) ? 0 : int.Parse(textBox4.Text);

                if (CaloriesToAdd < 0) throw new InvalidOperationException();

                label14.Text = "Your additional calories : " + CaloriesToAdd.ToString();
            }
            catch
            {
                MessageBox.Show("Try to enter a integer value, or a correct one", "Incorrect value", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetCaloriesToRemove()
        {
            try
            {
                CaloriesToRemove = string.IsNullOrWhiteSpace(textBox5.Text) ? 0 : int.Parse(textBox5.Text);

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

        private void GetFinalTDEE()
        {
            FinalTDEE = TDEE + CaloriesToAdd - CaloriesToRemove;

            label17.Text = "Your final TDEE is : " + FinalTDEE.ToString();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            GetBMR();
            GetTDEE();
            GetCaloriesToAdd();
            GetCaloriesToRemove();
            GetFinalTDEE();
            try
            {
                if (FinalTDEE <= 0) throw new ArgumentOutOfRangeException();

                CalculateMacros(comboBox2.SelectedIndex);

                label21.Text = "Protein required per day:" + " " + ProteinMacro;
                label22.Text = "Fat required per day:" + " " + FatMacro;
                label23.Text = "Carbs required per day:" + " " + CarbsMacro;

                label24.Text = Math.Round((double)ProteinMacro / GymActivities).ToString() + "g of protein per meal";
                label25.Text = Math.Round((double)FatMacro / GymActivities).ToString() + "g of fat per meal";

                var carbsHalf = (double)CarbsMacro / 2;

                label27.Text = Math.Round(carbsHalf / GymActivities).ToString() + "g of carbs per meal (away from gym session)";
                label28.Text = Math.Round(carbsHalf / 2 / GymActivities).ToString() + "g of carbs before and after gym session";
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