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
using USBprotect.InternalFunction;
using UsbSecurity;
using USBsecurity;


namespace USBprotect
{
    public partial class AllowBlockForm : Form
    {
        ManageAllowList manageAllowList = new ManageAllowList();
        ManageBlackList manageBlockList = new ManageBlackList();

        ParsingUsbDevice usbDevice = new ParsingUsbDevice();


        public AllowBlockForm()
        {

            DeviceMonitor watcher = new DeviceMonitor();
            watcher.Start();

            InitializeComponent();


            listBox1.AllowDrop = true;
            listBox2.AllowDrop = true;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // UsbDeviceWatcher 인스턴스 생성 및 시작
            DeviceMonitor watcher = new DeviceMonitor();
            watcher.Start();

            // FormEventBase에 Form1 인스턴스 설정
            UnauthorizedUsbFormEvent feb = new UnauthorizedUsbFormEvent();
            feb.GetForm(this);

        }

        public void OpenForm2(string message)
        {
            Form2 form2 = Form2.GetInstance(message);
            form2.Show();
        }

        //============  버튼 액션  ============//

        private void button3_Click(object sender, EventArgs e) // 새로 고침 버튼
        {

            // 버튼 누르면 리스트 전체 순회 해서 데이터 다시 로드
            if (USBinfo.BlackListDevices != null && USBinfo.BlackListDevices.Count > 0)
            {
                listBox1.Items.Clear();
                foreach (var device in USBinfo.BlackListDevices)
                {
                    listBox1.Items.Add($"{device.DeviceName} / {device.PnpDeviceId}");
                }
            }
            else
            {
                // 리스트가 null이거나 비어있을 경우
                MessageBox.Show("블랙리스트 장치가 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (USBinfo.WhiteListDevices != null && USBinfo.WhiteListDevices.Count > 0)
            {
                listBox2.Items.Clear();
                foreach (var device in USBinfo.WhiteListDevices)
                {
                    listBox2.Items.Add($"{device.DeviceName} / {device.PnpDeviceId}");
                }
            }
            else
            {
                // 리스트가 null이거나 비어있을 경우
                MessageBox.Show("화이트리스트 장치가 없습니다.", "에러", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e) // 승인버튼
        {
            if (listBox1.SelectedIndex != -1) // 아이템이 선택되었는지 확인
            {
                string selectedDevice = listBox1.SelectedItem.ToString(); // 선택된 디바이스
                listBox2.Items.Add(selectedDevice); // 승인된 디바이스 목록에 추가
                listBox1.Items.RemoveAt(listBox1.SelectedIndex); // 첫 번째 ListBox에서 선택된 디바이스 제거

                string[] parts = selectedDevice.Split('/'); // 선택된 디바이스의 정보를 가져옴 ( 앞에 장치 이름 제외하고 뒤에 id 정보만 가져옴)
                string result = parts[parts.Length - 1];

                manageAllowList.BlacktoWhite(result); // 선택된 디바이스를 블랙리스트에서화이트 리스트로 이동
                MessageBox.Show(selectedDevice + "가 승인되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("USB 디바이스를 선택하세요.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            //1. 버튼 클릭시 승인과정 거침
            //2. 승인되지 않을 시 메시지 박스로 승인되지 않았다고 알림
            //3. 어떤 정보를 불러와서 리스트 박스에 보이게할지 결정


            // Form1의 Load 이벤트에 이벤트 핸들러 추가
            this.Load += Form1_Load;
        }

        private void button2_Click(object sender, EventArgs e) // 차단 버튼
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


            // Form1의 Load 이벤트에 이벤트 핸들러 추가
            this.Load += Form1_Load;
        }

        //======================================================

        private void listBox1_MouseDown(object sender, MouseEventArgs e)
        {
            DragDropEffects effect;

            int index = listBox1.IndexFromPoint(e.X, e.Y);
            // listBox1.SelectedIndex를 사용하지 않으므로
            // 오른쪽 마우스 버튼의 드래그&드롭도 처리 가능

            if (index != ListBox.NoMatches)
            {
                string item = (string)listBox1.Items[index];
                effect = DoDragDrop(item, DragDropEffects.Copy | DragDropEffects.Move);
                if (effect == DragDropEffects.Move)
                {
                    listBox1.Items.RemoveAt(index);
                }
            }
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                listBox1.Items.Add(e.Data.GetData(DataFormats.StringFormat));
            }
        }

        private void listBox1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                // StringFormat 형식을 허용하기 때문에 다른 프로그램(노트패드 등)
                // 에서 문자열을 선택하고 드래그&드롭도 가능하다.

                if ((e.KeyState & 8) != 0)
                {
                    e.Effect = DragDropEffects.Copy;
          
                }
                else
                {
                    e.Effect = DragDropEffects.Move;
                }
            }
        }

        private void listBox2_MouseDown(object sender, MouseEventArgs e)
        {
            DragDropEffects effect;

            int index = listBox2.IndexFromPoint(e.X, e.Y);
            // listBox1.SelectedIndex를 사용하지 않으므로
            // 오른쪽 마우스 버튼의 드래그&드롭도 처리 가능

            if (index != ListBox.NoMatches)
            {
                string item = (string)listBox2.Items[index];
                effect = DoDragDrop(item, DragDropEffects.Copy | DragDropEffects.Move);
                if (effect == DragDropEffects.Move)
                {
                    listBox2.Items.RemoveAt(index);
                }
            }
        }
        

        private void listBox2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                listBox2.Items.Add(e.Data.GetData(DataFormats.StringFormat));

                string selectedDevice = listBox2.SelectedItem.ToString(); // 선택된 디바이스
                listBox1.Items.Add(selectedDevice); // 차단된 디바이스 목록에 추가
                listBox2.Items.RemoveAt(listBox2.SelectedIndex); // 첫 번째 ListBox에서 선택된 디바이스 제거

                string[] parts = selectedDevice.Split('/');
                string result = parts[parts.Length - 1];

                manageBlockList.WhiteToBlack(result); // 선택된 디바이스를 화이트리스트에서 블랙리스트로 이동
            }
        }

        private void listBox2_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                // StringFormat 형식을 허용하기 때문에 다른 프로그램(노트패드 등)
                // 에서 문자열을 선택하고 드래그&드롭도 가능하다.

                if ((e.KeyState & 8) != 0)
                {
                    e.Effect = DragDropEffects.Copy;

                }
                else
                {
                    e.Effect = DragDropEffects.Move;
                }
            }
        }


            //============  드래그 액션  ============//

            //lisbox1 과 listbox2 의 요소를 각각 선택 및 드래그 하여 양쪽 리스트에 추가할 수 있도록 한다
            // 이동할떄는 원래 있던 리스트에서 해당 요소를 제거하며, 동일 리스트에서 동일한 리스로 이동은 불가능하다


        }
}


