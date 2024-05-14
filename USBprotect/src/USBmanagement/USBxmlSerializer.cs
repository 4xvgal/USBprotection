using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using UsbSecurity;
using static UsbSecurity.USBinfo;


namespace USBprotect.src.USBmanagement
{
    internal class USBxmlSerializer
    {
        public static void SerializeToDeviceXml(List<USBinfo> devices, string filepath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<USBinfo>));
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                serializer.Serialize(writer, devices);
            }
        }
        public static void DeserializeFromDeviceXml(List<USBinfo> devices, string filepath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<USBinfo>));
            using (StreamReader reader = new StreamReader(filepath))
            {
                devices = (List<USBinfo>)serializer.Deserialize(reader);
            }
        }
    }
}
