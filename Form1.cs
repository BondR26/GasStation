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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        GroupBox GasStation;
        GroupBox MiniCafe;
        GroupBox TotalCost;
        GroupBox sum_and_price;
        GroupBox Calculate_Gas;
        GroupBox Calculate_Cafe;

        Label gas;
        Label price;
        Label different_prices;
        Label l_calc_gas;
        Label l_calc_cafe;
        Label final_calc;

        List<Label> uah;
        List<Label> food_prices;

        List<TextBox> food_inputs;

        RadioButton quantity;
        RadioButton sum;

        ComboBox gas_options;

        TextBox quantity_input_field;
        TextBox sum_input_field;

        List<CheckBox> food_checkboxes;

        Button final_calculation;

        float[] prices = { 30.5f, 36.1f, 33.5f, 26f, 15f };
        string[] list = { "A-96", "A-76", "A-100", "Diesel", "Propane Gas" };
        string[] food_strings = { "Hot-Dog", "Hamburger", "French Fries", "Coca-Cola" };
        string[] food_prices_strings = { "0,6", "0,3", "0,9", "0,8" };
        string[] food_inputs_strings = { "", "", "", "" };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Final_Calc(ref GroupBox father, Label lb, Point location)
        {
            lb = new Label();
            lb.Text = "0.00";
            lb.Location = location;
            lb.Font = new Font("Arial", 14);
            father.Controls.Add(lb);
        }
        private void Form1_Final_Calculation(ref GroupBox father, ref Button button, Point location)
        {
            button = new Button();
            button.Enabled = true;
            button.Text = "Calculate";
            button.Location = location;
            button.Size = new Size(100, 60);
            button.FlatStyle = FlatStyle.Popup;
            button.BackColor = Color.Aqua;
            button.Click += Form1_Total_Cost;
            father.Controls.Add(button);
        }
        private void Fill_CalculateItem(ref GroupBox father, ref GroupBox child, Point location)
        {
            child = new GroupBox();

            child = new GroupBox();
            child.Text = "Calculate Cafe";

            child.Location = location;
            father.Controls.Add(child);
        }

        private void FillFood(ref GroupBox Calculate_Cafe, ref List<CheckBox> list, string[] str, Point point)
        {
            list = new List<CheckBox>();

            for (int i = 0; i < str.Length; i++)
            {
                CheckBox cb = new CheckBox();
                if (i == 0)
                    cb.Checked = true;
                cb.Text = str[i];
                cb.AutoSize = true;
                cb.Location = new Point(point.X, point.Y);
                cb.Click += Form1_Food_CheckBoxes_Checked;
                point.Y += 30;
                list.Add(cb);
                Calculate_Cafe.Controls.Add(cb);
            }
        }

        private void Prices_For_Food(ref GroupBox Calculate_Cafe, ref List<Label> list, string[] str, Point point)
        {
            list = new List<Label>();

            for (int i = 0; i < str.Length; i++)
            {
                Label lb = new Label();

                lb.Enabled = false;
                lb.Text = str[i];
                lb.AutoSize = true;
                lb.BorderStyle = BorderStyle.FixedSingle;
                lb.Location = new Point(point.X, point.Y);
                point.Y += 30;
                list.Add(lb);
                Calculate_Cafe.Controls.Add(lb);
            }
        }

        private void Inputs_For_Food(ref GroupBox Calculate_Cafe, ref List<TextBox> list, string[] str, Point point)
        {
            list = new List<TextBox>();

            for (int i = 0; i < str.Length; i++)
            {
                TextBox tb = new TextBox();
                if (i == 0)
                    tb.Enabled = true;
                else
                    tb.Enabled = false;
                tb.Text = str[i];
                tb.AutoSize = true;
                tb.Location = new Point(point.X, point.Y);
                point.Y += 30;
                tb.KeyUp += Form1_Food_Calculate_Prices;
                list.Add(tb);
                Calculate_Cafe.Controls.Add(tb);
            }
        }

        private void Quantity_Input_Field_Load(ref GroupBox gb, ref GroupBox for_orientation)
        {
            quantity_input_field = new TextBox();
            quantity_input_field.Text = "0";
            quantity_input_field.Location = new Point(for_orientation.Location.X + 110, for_orientation.Location.Y + 10);
            //Correlate radiobutton and textbox
            quantity_input_field.AcceptsTab = true;
            quantity_input_field.KeyUp += Form1_Calculate_Prices_for_Gas;
            quantity_input_field.Size = new Size(80, 20);
            gb.Controls.Add(quantity_input_field);
        }

        private void Sum_Input_Field_Load(ref GroupBox gb, ref GroupBox for_orientation)
        {
            sum_input_field = new TextBox();
            sum_input_field.Text = "0";
            sum_input_field.Location = new Point(for_orientation.Location.X + 110, for_orientation.Location.Y + 40);
            sum_input_field.Enabled = false;
            //Correlate radiobutton and textbox
            sum_input_field.AcceptsTab = true;
            sum_input_field.KeyUp += Form1_Calculate_Prices_for_Gas;
            sum_input_field.Size = new Size(80, 20);
            gb.Controls.Add(sum_input_field);
        }

        private void Form1_GasStation_UAH()
        {
            uah = new List<Label>();

            List<Point> uah_locations = new List<Point>{new Point(quantity_input_field.Location.X + quantity_input_field.Width + 10, quantity_input_field.Location.Y),
                new Point(different_prices.Location.X+different_prices.Width + 5, different_prices.Location.Y),
                new Point(sum_input_field.Location.X + sum_input_field.Width+5, sum_input_field.Location.Y),
                new Point(l_calc_gas.Location.X + l_calc_gas.Width + 5, l_calc_gas.Location.Y+10),
                new Point(l_calc_cafe.Location.X + l_calc_cafe.Width +5, l_calc_cafe.Location.Y + 10)};

            for (int i = 0; i < 5; i++)
            {
                Label price_uah = new Label();

                if (i == 0)//litters case
                {
                    price_uah.Text = "l";
                }
                else
                    price_uah.Text = "uah";

                price_uah.Location = uah_locations[i];

                uah.Add(price_uah);

                if (i == 3)
                {
                    Calculate_Gas.Controls.Add(uah[i]);
                }
                else if (i == 4)
                {
                    Calculate_Cafe.Controls.Add(uah[i]);
                }
                else
                    GasStation.Controls.Add(uah[i]);
            }

        }

        private void Form1_Fill_Label_Calculate(GroupBox father, ref Label label, Point point)
        {
            label = new Label();
            label.Text = "0.00";
            label.Location = point;
            label.AutoSize = true;
            label.Font = new Font("Arial", 15);
            label.TextAlign = ContentAlignment.MiddleLeft;
            father.Controls.Add(label);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.AntiqueWhite;

            //GasStation part
            //
            GasStation = new GroupBox();
            GasStation.ForeColor = Color.Blue;
            GasStation.Text = "Gas Station";
            GasStation.Location = new Point(this.Width / 10, 20);
            //GasStation.AutoSize = true;
            GasStation.Size = new Size(this.GasStation.Width + 50, 300);
            this.Controls.Add(GasStation);

            //Gas label
            gas = new Label();
            gas.ForeColor = Color.Black;
            gas.Text = "Gas";
            gas.AutoSize = true;
            gas.Location = new Point(10, 50);
            gas.BorderStyle = BorderStyle.None;
            GasStation.Controls.Add(gas);

            //Gas options or Drop Down List with differernt kinds of gas

            gas_options = new ComboBox();
            gas_options.Items.AddRange(list);
            // event connecting tuype of gas and its price need to be added
            gas_options.TextChanged += Form1_GasPrices;
            gas_options.AutoSize = true;
            gas_options.Location = new Point(gas.Location.X + 50, gas.Location.Y);
            GasStation.Controls.Add(gas_options);

            //Price label
            price = new Label();
            price.ForeColor = Color.Black;
            price.AutoSize = true;
            price.Text = "Price";
            price.Location = new Point(10, 100);
            price.BorderStyle = BorderStyle.None;

            GasStation.Controls.Add(price);

            //prices of gas variations
            different_prices = new Label();
            different_prices.AutoSize = true;
            different_prices.Font = new Font("Arial", 11);
            different_prices.Text = "0.00";
            different_prices.Location = new Point(price.Location.X + 50, price.Location.Y - 2);
            GasStation.Controls.Add(different_prices);


            //GroupBox for sum and price
            sum_and_price = new GroupBox();
            //sum_and_price.AutoSize = true;
            sum_and_price.Size = new Size(100, 60);
            sum_and_price.Location = new Point(price.Location.X, price.Location.Y + 20);
            GasStation.Controls.Add(sum_and_price);
            //quantity
            quantity = new RadioButton();
            quantity.Text = "Quantity";
            quantity.AutoSize = true;
            quantity.Checked = true;
            quantity.Location = new Point(5, 10);
            quantity.CheckedChanged += Form1_Sum_and_Quantity_Active;
            sum_and_price.Controls.Add(quantity);
            //Input for Quantity
            Quantity_Input_Field_Load(ref GasStation, ref sum_and_price);


            //sum
            sum = new RadioButton();
            sum.Checked = false;
            sum.AutoSize = true;
            sum.Text = "Sum";
            sum.Location = new Point(quantity.Location.X, quantity.Location.Y + 30);
            sum.CheckedChanged += Form1_Sum_and_Quantity_Active;
            sum_and_price.Controls.Add(sum);
            //Input for Sum
            Sum_Input_Field_Load(ref GasStation, ref sum_and_price);
            //
            //GroupBox Calculate Gas
            Fill_CalculateItem(ref GasStation, ref Calculate_Gas, new Point(sum_and_price.Location.X, sum_and_price.Location.Y + sum_and_price.Height + 30));
            //
            //Overall price for gas
            Form1_Fill_Label_Calculate(Calculate_Gas, ref l_calc_gas, new Point(100, 40));

            //Add uah where it is needed


            //
            //MiniCafe part
            //MiniCafe part
            MiniCafe = new GroupBox();
            MiniCafe.ForeColor = Color.Blue;
            MiniCafe.Text = "Mini Cafe";
            MiniCafe.Location = new Point(this.Width / 2, 20);
            MiniCafe.Size = new Size(this.MiniCafe.Width + 100, 300);
            this.Controls.Add(MiniCafe);

            FillFood(ref MiniCafe, ref food_checkboxes, food_strings, new Point(10, 20));
            Prices_For_Food(ref MiniCafe, ref food_prices, food_prices_strings, new Point(100, 20));

            Inputs_For_Food(ref MiniCafe, ref food_inputs, food_inputs_strings, new Point(150, 20));
            //Calculate Cafe 
            Fill_CalculateItem(ref MiniCafe, ref Calculate_Cafe, new Point(sum_and_price.Location.X, sum_and_price.Location.Y + sum_and_price.Height + 30));

            //Overall price for Cafe
            Form1_Fill_Label_Calculate(Calculate_Cafe, ref l_calc_cafe, new Point(100, 40));

            //

            TotalCost = new GroupBox();
            TotalCost.Text = "Всього до сплати";
            TotalCost.Location = new Point(this.Width / 3, this.Height - this.Height / 3);
            TotalCost.Size = new Size(300, 100);
            this.Controls.Add(TotalCost);

            Form1_Final_Calculation(ref TotalCost, ref final_calculation, new Point(TotalCost.Width/4, TotalCost.Height/4));
            Form1_Fill_Label_Calculate(TotalCost, ref final_calc, new Point(final_calculation.Location.X+ final_calculation.Width+10, final_calculation.Location.Y+10));
            Form1_GasStation_UAH();
        }

        private void Form1_GasPrices(object sender, EventArgs e)
        {
            switch (gas_options.SelectedIndex)
            {
                case 0:
                    {
                        different_prices.Text = prices[0].ToString();
                    }
                    break;
                case 1:
                    {
                        different_prices.Text = prices[1].ToString();
                    }
                    break;
                case 2:
                    {
                        different_prices.Text = prices[2].ToString();
                    }
                    break;
                case 3:
                    {
                        different_prices.Text = prices[3].ToString();
                    }
                    break;
                case 4:
                    {
                        different_prices.Text = prices[4].ToString();
                    }
                    break;
                case 5:
                    {
                        different_prices.Text = prices[5].ToString();
                    }
                    break;
                case -1:
                    {
                        MessageBox.Show("Gas type wasnt selected");
                    }
                    break;
            }

        }

        private void Form1_Calculate_Prices_for_Gas(object sender, EventArgs e)
        {
            if (quantity.Checked)
            {
                try
                {
                    l_calc_gas.Text = (float.Parse(different_prices.Text) * float.Parse(quantity_input_field.Text)).ToString();
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
            else if (sum.Checked)
            {
                try
                {
                    l_calc_gas.Text = (float.Parse(sum_input_field.Text) / float.Parse(different_prices.Text)).ToString();

                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
            }
        }

        private void Form1_Sum_and_Quantity_Active(object sender, EventArgs e)
        {
            if (quantity.Checked)
            {
                sum_input_field.Enabled = false;
                quantity_input_field.Enabled = true;
            }
            else if (sum.Checked)
            {
                sum_input_field.Enabled = true;
                quantity_input_field.Enabled = false;
                uah[uah.Count - 1].Text = "l";
            }

        }

        private void Form1_Food_CheckBoxes_Checked(object sender, EventArgs e)
        {

            for (int i = 0; i < 4; i++)
            {
                if (food_checkboxes[i].Checked)
                {
                    food_inputs[i].Enabled = true;
                }
                else if (!food_checkboxes[i].Checked)
                {
                    food_inputs[i].Enabled = false;
                }
            }
        }

        private void Form1_Food_Calculate_Prices(object sender, EventArgs e)
        {
            List<float> list = new List<float>();
            string str = "";
            float num = 0;
            int j = 0;
            for (int i = 0; i < food_inputs.Count; i++)
            {
                if (food_inputs[i].Enabled == true)
                {
                    try
                    {
                        list.Add(float.Parse(food_inputs[i].Text)*float.Parse(food_prices_strings[i]));
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                        return;
                    }
                    num += list[j];
                    j++;
                }

                
            }

            l_calc_cafe.Text = num.ToString();
        }

        private void Form1_Total_Cost(object sender, EventArgs e)
        {
            try
            {
                final_calc.Text = (float.Parse(l_calc_gas.Text) + float.Parse(l_calc_cafe.Text)).ToString();
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

    }
}