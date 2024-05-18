using System;
using System.Management;
using System.Windows.Forms;
using UsbSecurity;

namespace USBprotect.USBmanagement
{
    public class UsbDeviceMonitor
    {
        private ManagementEventWatcher _insertWatcher;
        private ManagementEventWatcher _removeWatcher;
        private DevconCMD _devcon;
        private ParsingUsbDevice _parsingUsbDevice;

        // USB 장치 정보 처리를 위한 Action 델리게이트 추가
        public event Action<USBinfo> OnUsbDeviceInserted;

        public UsbDeviceMonitor() // 생성자
        {
            this._devcon = new DevconCMD();
            _parsingUsbDevice = new ParsingUsbDevice();

            var insertQuery = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_PnPEntity' AND TargetInstance.DeviceID LIKE 'USB%'");
            _insertWatcher = new ManagementEventWatcher(insertQuery);
            _insertWatcher.EventArrived += new EventArrivedEventHandler(DeviceInsertedEvent);

            var removeQuery = new WqlEventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_PnPEntity' AND TargetInstance.DeviceID LIKE 'USB%'");
            _removeWatcher = new ManagementEventWatcher(removeQuery);
            _removeWatcher.EventArrived += new EventArrivedEventHandler(DeviceRemovedEvent);
        }
        // USB 장치 감시 시작
        public void Start()
        {
            _insertWatcher.Start();
            _removeWatcher.Start();
        }
        // USB 장치 감시 중지
        public void Stop()
        {
            _insertWatcher.Stop();
            _removeWatcher.Stop();
        }

        private void DeviceInsertedEvent(object sender, EventArrivedEventArgs e)
        {
            var device = (ManagementBaseObject)e.NewEvent["TargetInstance"];

            // 장치 정보를 파싱하여 USBinfo 객체 생성
            USBinfo usbInfo = new USBinfo
            {
                DeviceId = device["DeviceID"].ToString(),
                Description = device["Description"].ToString(),
                DeviceName = device["Name"].ToString()
            };

            MessageBox.Show("새 장치가 감지됨 : " + usbInfo.DeviceName);

            OnUsbDeviceInserted?.Invoke(usbInfo); // 콜백을 통해 USBinfo 객체 전달
        }

        private void DeviceRemovedEvent(object sender, EventArrivedEventArgs e)
        {
            MessageBox.Show("장치가 제거됨.");
        }
    }
}
