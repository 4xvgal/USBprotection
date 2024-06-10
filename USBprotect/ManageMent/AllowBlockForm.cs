using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace UsbSecurity
{
    public partial class AllowBlockForm : Form
    {
        ManageAllowList manageAllowList = new ManageAllowList();
        ManageBlackList manageBlockList = new ManageBlackList();
        ParsingUsbDevice usbDevice = new ParsingUsbDevice();
        InquaryUSBList inquaryUSBList;

        public AllowBlockForm()
        {
            InitializeComponent();
            this.inquaryUSBList = new InquaryUSBList(this);
        }


        //============  버튼 액션  ============//
        private void AllowBlockForm_Load(object sender, EventArgs e)
        {
            inquaryUSBList.inquaryList();
            USBinfo.SaveWhiteList();
            USBinfo.SaveBlackList();
        }


        private void Allow_Button_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1) // 아이템이 선택되었는지 확인
            {
                string selectedDevice = listBox1.SelectedItem.ToString(); // 선택된 디바이스
                listBox2.Items.Add(selectedDevice); // 승인된 디바이스 목록에 추가
                listBox1.Items.RemoveAt(listBox1.SelectedIndex); // 첫 번째 ListBox에서 선택된 디바이스 제거

                string[] parts = selectedDevice.Split('/'); // 선택된 디바이스의 정보를 가져옴 ( 앞에 장치 이름 제외하고 뒤에 id 정보만 가져옴)
                string result = parts[parts.Length - 1];

                manageAllowList.BlackToWhite(result); // 선택된 디바이스를 블랙리스트에서화이트 리스트로 이동
                MessageBox.Show(selectedDevice + "가 승인되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("USB 디바이스를 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //1. 버튼 클릭시 승인과정 거침
            //2. 승인되지 않을 시 메시지 박스로 승인되지 않았다고 알림
            //3. 어떤 정보를 불러와서 리스트 박스에 보이게할지 결정

        }
        private void block_button_Click(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex != -1) // 아이템이 선택되었는지 확인
            {
                string selectedDevice = listBox2.SelectedItem.ToString(); // 선택된 디바이스
                listBox1.Items.Add(selectedDevice); // 차단된 디바이스 목록에 추가
                listBox2.Items.RemoveAt(listBox2.SelectedIndex); // 첫 번째 ListBox에서 선택된 디바이스 제거

                string[] parts = selectedDevice.Split('/');
                string result = parts[parts.Length - 1];

                manageBlockList.WhiteToBlack(result); // 선택된 디바이스를 화이트리스트에서 블랙리스트로 이동
                MessageBox.Show(selectedDevice + "가 차단되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("USB 디바이스를 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //1. 버튼 클릭시 차단과정 거침
            //2. 승인되지 않을 시 메시지 박스로 승인되지 않았다고 알림
            //3. 어떤 정보를 불러와서 리스트 박스에 보이게할지 결정
        }



        private void Home_Button_Click(object sender, EventArgs e)
        {
            this.Hide(); // 현재 폼 (Form2) 숨기기
            MainForm.Instance.Show();
        }

        private void Refresh_Button_Click(object sender, EventArgs e)
        {
            inquaryUSBList.inquaryList();
        }
    }
}