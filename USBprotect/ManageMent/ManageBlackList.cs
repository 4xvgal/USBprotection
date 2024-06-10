using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace UsbSecurity
{
    class ManageBlackList
    {
        private System.Timers.Timer checkTimer;
        string devconPath = @"C:\Program Files (x86)\Windows Kits\10\Tools\10.0.22621.0\x64\devcon.exe";

        public ManageBlackList()
        {
            USBinfo.BlackListDevices.CollectionChanged += BlackList_CollectionChanged;
            StartTimer();
            StartUsbDeviceWatcher();
        }

        private void StartTimer()
        {
            checkTimer = new System.Timers.Timer(1500); // 1초마다 확인
            checkTimer.Elapsed += OnTimedEvent;
            checkTimer.AutoReset = true;
            checkTimer.Enabled = true;
        }

        private void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            if (USBinfo.BlackListDevices.Count > 0)
            {
                foreach (var device in USBinfo.BlackListDevices)
                {
                    disableEveryDevice(device.DeviceId); // 블랙리스트에 있는 장치 비활성화
                }
            }
        }

        public string BlockDevconCommand(string command)
        {
            ProcessStartInfo psi = new ProcessStartInfo()
            {
                FileName = devconPath,
                Arguments = command,
                UseShellExecute = false,
                Verb = "runas",
                CreateNoWindow = true
            };

            try
            {
                using (Process process = Process.Start(psi))
                {
                    process.WaitForExit();
                    return process.ExitCode == 0 ? "Success" : $"Failed with error code: {process.ExitCode}";
                }
            }
            catch (Exception ex)
            {
                return $"Error executing Devcon command: {ex.Message}";
            }
        }

        public bool SetDevconPath(string path)
        {
            devconPath = path;
            return checkDevconExist();
        }

        private bool checkDevconExist()
        {
            string result = BlockDevconCommand("help");
            return result.Contains("Device Console Help");
        }

        private void BlackList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    USBinfo newItem = item as USBinfo;
                    disableEveryDevice(newItem.DeviceId);
                }
            }
        }

        public void disableEveryDevice(string deviceId)
        {
            BlockDevconCommand($"disable \"@{deviceId}\"");
        }

        public void WhiteToBlack(string deviceId)
        {
            var device = USBinfo.WhiteListDevices.FirstOrDefault(x => x.PnpDeviceId.Trim().Equals(deviceId.Trim(), StringComparison.OrdinalIgnoreCase));

            if (device != null)
            {
                Task.Run(() => {
                    USBinfo.BlackListDevices.Add(device);
                    USBinfo.WhiteListDevices.Remove(device);
                    USBinfo.SaveWhiteList();
                    USBinfo.SaveBlackList();
                    MessageBox.Show(deviceId + "가 차단되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                });
            }
            else
            {
                MessageBox.Show($"Device with ID {deviceId} not found in whitelist.");
            }
        }

        private void StartUsbDeviceWatcher()
        {
            ManagementEventWatcher insertWatcher = new ManagementEventWatcher(
            new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_PnPEntity' AND TargetInstance.DeviceID LIKE 'USB%'"));
            insertWatcher.EventArrived += (sender, e) => OnUsbDeviceChanged(e.NewEvent["TargetInstance"] as ManagementBaseObject);
            insertWatcher.Start();

            ManagementEventWatcher removeWatcher = new ManagementEventWatcher(
                new WqlEventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_PnPEntity' AND TargetInstance.DeviceID LIKE 'USB%'"));
            removeWatcher.EventArrived += (sender, e) => OnUsbDeviceChanged(e.NewEvent["TargetInstance"] as ManagementBaseObject);
            removeWatcher.Start();
        }

        private void OnUsbDeviceChanged(ManagementBaseObject device)
        {
            string deviceId = device["DeviceID"].ToString();
            if (USBinfo.BlackListDevices.Any(x => x.DeviceId == deviceId))
            {
                disableEveryDevice(deviceId);
            }
        }
    }
}