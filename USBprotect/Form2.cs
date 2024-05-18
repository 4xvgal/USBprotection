using System;
using System.Linq;
using System.Windows.Forms;
using UsbSecurity;
using USBprotect.PermitRequest;

namespace USBprotect
{
    public partial class Form2 : Form
    {
        private string hintText = "요청 사유를 입력하세요";
        private string message;
        private static Form2 instance;
        private PermitRequestList requestList;

        public Form2(string message)
        {
            InitializeComponent();
            this.message = message;
            this.Load += Form2_Load;
            requestList = new PermitRequestList();

            // USB 장치 정보를 가져와 label에 추가
            foreach (var id in ParsingUsbDevice.saveDeviceID)
            {
                label6.Text += id + "\n";
            }
        }

        public static Form2 GetInstance(string message)
        {
            if (instance != null && !instance.IsDisposed)
            {
                instance.Close();
            }

            instance = new Form2(message);
            return instance;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MessageBox.Show(message);
            SetHintText();
        }

        private void SetHintText()
        {
            textBox1.Text = hintText;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string deviceName = label6.Text.Trim();
                if (string.IsNullOrEmpty(deviceName))
                {
                    MessageBox.Show("USB 장치가 없습니다.");
                    return;
                }

                string requester = textBox2.Text;
                string reason = textBox1.Text;
                DateTime requestTime = DateTime.Now;

                requestList.AddRequest(deviceName, requester, reason, requestTime);

                MessageBox.Show("요청이 전송되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == hintText)
            {
                textBox1.Text = "";
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                SetHintText();
            }
        }
    }
}
