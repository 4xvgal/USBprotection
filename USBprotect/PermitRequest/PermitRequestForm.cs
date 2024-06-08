using System;
using System.Linq;
using System.Windows.Forms;
using UsbSecurity;
using System.Management;
using System.Collections.Generic;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection.Emit;

namespace UsbSecurity
{
    // 허용 요청 보내는 폼
    public partial class PermitRequestForm : Form
    {
        private readonly string hintText = "요청 사유를 입력하세요"; // 텍스트 상자의 힌트 텍스트
        string selectedItem;
        private static PermitRequestForm instance; // Form2의 인스턴스
        private readonly PermitRequestAdd _requestAdd; // 허용 요청 추가 클래스
        public USBinfo USBinfo = new USBinfo(); // USB 장치 정보 클래스

        public PermitRequestForm()     // 생성자
        {
            InitializeComponent();
            this.Load += Form2_Load; // 폼 로드 이벤트 핸들러 등록
            _requestAdd = new PermitRequestAdd(); // 허용 요청 추가 클래스 초기화
        }

        // Form2 인스턴스 가져오는 메서드
        public static PermitRequestForm GetInstance(string message)
        {
            if (instance != null && !instance.IsDisposed)
            {
                instance.Close();
            }

            instance = new PermitRequestForm();
            return instance;
        }

        // 폼 로드 시 이벤트 핸들러
        private void Form2_Load(object sender, EventArgs e)
        {           
            SetHintText(); // 힌트 텍스트 설정
            UpdateUsbDevicesLabel(); // USB 장치 정보 업데이트

        }

        // 힌트 텍스트 설정 메서드
        private void SetHintText()
        {
            textBox1.Text = hintText;
        }

        // 취소 버튼 클릭 이벤트 핸들러
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); // 폼 닫기
        }

        // 허용 요청 버튼 클릭 이벤트 핸들러
        private void button2_Click(object sender, EventArgs e)  
        {
            try
            {
                string deviceInfo = selectedItem;
                if (string.IsNullOrEmpty(deviceInfo))
                {
                    MessageBox.Show("USB 장치가 없습니다.");
                    return;
                }

                string requester = textBox2.Text; // 요청자
                string reason = textBox1.Text; // 사유
                DateTime requestTime = DateTime.Now; // 요청 시간
                string deviceName = ExtractDeviceName(deviceInfo); // deviceName 추출
                string deviceId = ExtractDeviceId(deviceInfo); // deviceId 추출

                _requestAdd.AddRequest(deviceName, requester, reason, requestTime, deviceId); // 허용 요청 추가

                MessageBox.Show("요청이 전송되었습니다.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // 텍스트 상자에 포커스가 들어간 경우 이벤트 핸들러
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == hintText)
            {
                textBox1.Text = ""; // 힌트 텍스트 삭제
            }
        }

        // 텍스트 상자에서 포커스가 빠져나간 경우 이벤트 핸들러
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                SetHintText(); // 텍스트 상자가 비어있으면 힌트 텍스트 설정
            }
        }

        // USB 장치 정보를 가져와 label에 추가하는 메서드
        private void UpdateUsbDevicesLabel()
        {
            
            var usbDevices = GetUsbDevices(); // USB 장치 정보 가져오기

            foreach (var device in usbDevices)
            {
                comboBox1.Items.Add(device);
            
            }
        }

        // 현재 연결된 USB 장치 정보를 가져오는 메서드
        private string[] GetUsbDevices()
        {
            var deviceList = new List<string>();

            if (USBinfo.WhiteListDevices != null)
            {
                foreach (var usb in USBinfo.BlackListDevices)
                {
                    string devicename = usb.DeviceName;
                    string deviceid = usb.DeviceId;
                    deviceList.Add($"{devicename} ({deviceid})");
                }
            }
            else
            {
                MessageBox.Show("USB 장치 정보를 가져오는데 실패했습니다.");    
            }


            return deviceList.ToArray();
        }

        // USB 장치의 DeviceID를 추출하는 메서드
        private string ExtractDeviceId(string deviceInfo)
        {
            int startIndex = deviceInfo.IndexOf('(') + 1;
            int endIndex = deviceInfo.IndexOf(')');
            if (startIndex > 0 && endIndex > startIndex)
            {
                return deviceInfo.Substring(startIndex, endIndex - startIndex);
            }
            return "Unknown";
        }

        // USB 장치의 DeviceName을 추출하는 메서드
        private string ExtractDeviceName(string deviceInfo)
        {
            int startIndex = 0;
            int endIndex = deviceInfo.IndexOf('(');
            if (endIndex > startIndex)
            {
                return deviceInfo.Substring(startIndex, endIndex - 1).Trim();
            }
            return deviceInfo;
        }

        // USB 장치가 추가되거나 제거될 때 호출되는 이벤트 핸들러
        private void UsbDeviceChangeHandler(object sender, EventArrivedEventArgs e)
        {
            UpdateUsbDevicesLabel(); // USB 장치 정보 업데이트
        }

        private void button1_Click_1(object sender, EventArgs e) // 홈으로 가는 버튼
        {
            this.Hide(); // 현재 폼 (Form2) 숨기기
            MainForm.Instance.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UpdateUsbDevicesLabel();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedItem = comboBox1.SelectedItem.ToString();

        }
    }
}
