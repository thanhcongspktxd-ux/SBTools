using System;
using System.Windows.Forms;

namespace SBTools
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        // Hàm sự kiện khi bấm nút Close
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); // Lệnh đóng form hiện tại
        }

        // Bổ sung 2 hàm này để khớp với các sự kiện lỡ bấm nhầm trên giao diện thiết kế
        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void AboutForm_Load(object sender, EventArgs e)
        {

        }
    }
}