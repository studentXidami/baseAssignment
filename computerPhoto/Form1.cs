namespace computerPhoto
{
    public partial class ������ : Form
    {
        public double a;
        public ������()
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

        private void ����_Click(object sender, EventArgs e)
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

        private void ����_MouseClick(object sender, MouseEventArgs e)
        {
            textBox3.Text = a.ToString();
        }
    }
}
