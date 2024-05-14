    using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsbSecurity
{ 

    internal class USBinfo
    {
        public static readonly object _lock = new object();
        public static ObservableCollection<USBinfo> BlackListDevices = new ObservableCollection<USBinfo>();
        public static ObservableCollection<USBinfo> WhiteListDevices = new ObservableCollection<USBinfo>();

        public string DeviceName { get; set; }
        public string Status { get; set; }
        public string DeviceId { get; set; }
        public string PnpDeviceId { get; set; }
        public string Description { get; set; }

        public static void AddBlackListDevice(USBinfo device)
        {
            lock (_lock)
            {
                BlackListDevices.Add(device);
            }
        }

        public static void RemoveBlackListDevice(USBinfo device)
        {
            lock (_lock)
            {
                BlackListDevices.Remove(device);
            }
        }
    }
}
