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
        public static ObservableCollection<USBinfo> BlackListDevices = new ObservableCollection<USBinfo>();
        public static ObservableCollection<USBinfo> WhiteListDevices = new ObservableCollection<USBinfo>();

        public string DeviceName { get; set; }
        public string Status { get; set; }
        public string DeviceId { get; set; }
        public string PnpDeviceId { get; set; }
        public string Description { get; set; }
    }
}
