using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UsbSecurity;

namespace USBprotect.USBmanagement
{
    internal static class UsBxmlSerializer
    {
        public static void SerializeToDeviceXml(List<USBinfo> devices, string filepath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<USBinfo>));
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                serializer.Serialize(writer, devices);
            }
        }

        public static List<USBinfo> DeserializeFromDeviceXml(string filepath)
        {
            if (!File.Exists(filepath))
            {
                // 파일이 존재하지 않을 때 적절한 처리 필요
                return new List<USBinfo>(); // 빈 리스트 반환
            }

            XmlSerializer serializer = new XmlSerializer(typeof(List<USBinfo>));
            using (StreamReader reader = new StreamReader(filepath))
            {
                return (List<USBinfo>)serializer.Deserialize(reader);
            }
        }
    }
}