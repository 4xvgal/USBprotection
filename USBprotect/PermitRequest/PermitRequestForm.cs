using System;
using System.Linq;
using System.Windows.Forms;
using UsbSecurity;
using USBprotect.PermitRequest;
using System.Management;

namespace USBprotect
{
    // 허용 요청 보내는 폼
    public partial class PermitRequestForm : Form
    {
        private readonly string hintText = "요청 사유를 입력하세요"; // 텍스트 상자의 힌트 텍스트
        private readonly string message; // 초기 메시지
        private static PermitRequestForm instance; // Form2의 인스턴스
        private readonly PermitRequestAdd _requestAdd; // 허용 요청 추가 클래스

        public PermitRequestForm(string message)     // 생성자
        {
            InitializeComponent();
            this.message = message;
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

            instance = new PermitRequestForm(message);
            return instance;
        }

        // 폼 로드 시 이벤트 핸들러
        private void Form2_Load(object sender, EventArgs e)
        {
            MessageBox.Show(message); // 초기 메시지 표시
            SetHintText(); // 힌트 텍스트 설정
            UpdateUsbDevicesLabel(); // USB 장치 정보 업데이트

            // USB 장치 변경 이벤트 핸들러 등록
            ManagementEventWatcher watcher = new ManagementEventWatcher();
            watcher.EventArrived += UsbDeviceChangeHandler;
            watcher.Query = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2 or EventType = 3");
            watcher.Start();
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
                string deviceName = label6.Text.Trim();
                if (string.IsNullOrEmpty(deviceName))
                {
                    MessageBox.Show("USB 장치가 없습니다.");
                    return;
                }

                string requester = textBox2.Text; // 요청자
                string reason = textBox1.Text; // 사유
                DateTime requestTime = DateTime.Now; // 요청 시간
                string deviceId = GetDeviceId(deviceName); // deviceId 가져오기

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
            label6.Text = string.Empty; // 기존 텍스트 초기화
            var usbDevices = GetUsbDevices(); // USB 장치 정보 가져오기

            foreach (var device in usbDevices)
            {
                label6.Text += device + "\n"; // 장치 정보 추가
            }
        }

        // 현재 연결된 USB 장치 정보를 가져오는 메서드
        private string[] GetUsbDevices()
        {
            var deviceList = new System.Collections.Generic.List<string>();
            string wmiQuery = "SELECT Name, DeviceID FROM Win32_PnPEntity WHERE PNPDeviceID LIKE 'USB%' AND (Description LIKE '%디스크 드라이브%' OR DeviceID LIKE 'USBSTOR%')";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQuery);

            foreach (ManagementObject queryObj in searcher.Get())
            {
                var deviceName = queryObj["Name"]?.ToString() ?? "Unknown";
                var deviceId = queryObj["DeviceID"]?.ToString() ?? "Unknown";
                deviceList.Add($"{deviceName} ({deviceId})");
            }

            return deviceList.ToArray();
        }

        // USB 장치의 DeviceID를 추출하는 메서드
        private string GetDeviceId(string deviceName)
        {
            string wmiQuery = $"SELECT DeviceID FROM Win32_PnPEntity WHERE Name = '{deviceName}'";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wmiQuery);

            foreach (ManagementObject queryObj in searcher.Get())
            {
                return queryObj["DeviceID"]?.ToString() ?? "Unknown";
            }

            return "Unknown";
        }

        // USB 장치가 추가되거나 제거될 때 호출되는 이벤트 핸들러
        private void UsbDeviceChangeHandler(object sender, EventArrivedEventArgs e)
        {
            UpdateUsbDevicesLabel(); // USB 장치 정보 업데이트
        }
    }
}
