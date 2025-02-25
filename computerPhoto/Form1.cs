namespace computerPhoto
{
    public partial class 计算器 : Form
    {
        public double a;
        public 计算器()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void 运算_Click(object sender, EventArgs e)
        {

        }

        private void Button1_MouseClick(object sender, MouseEventArgs e)
        {
            double num1 = double.Parse(textBox1.Text);
            double num2 = double.Parse(textBox2.Text);
            a = num1 + num2;
        }

        private void Button2_MouseClick(object sender, MouseEventArgs e)
        {
            double num1 = double.Parse(textBox1.Text);
            double num2 = double.Parse(textBox2.Text);
            a = num1 - num2;
        }

        private void Button3_MouseClick(object sender, MouseEventArgs e)
        {
            double num1 = double.Parse(textBox1.Text);
            double num2 = double.Parse(textBox2.Text);
            a = num1 * num2;
        }

        private void Button4_MouseClick(object sender, MouseEventArgs e)
        {
            double num1 = double.Parse(textBox1.Text);
            double num2 = double.Parse(textBox2.Text);
            a = num1 / num2;
        }

        private void 运算_MouseClick(object sender, MouseEventArgs e)
        {
            textBox3.Text = a.ToString();
        }
    }
}
