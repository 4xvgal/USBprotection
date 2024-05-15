using System;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using USBprotect.USBmanagement;
using UsbSecurity;
using NUnit.Framework.Legacy; // ClassicAssert를 위한 네임스페이스 추가

namespace USBprotect.Tests
{
    [TestFixture]
    public class UsBxmlSerializerTests
    {
        private string _testFilePath = "testDevices.xml";

        [SetUp]
        public void Setup()
        {
            if (!File.Exists(_testFilePath))
            {
                List<USBinfo> initialList = new List<USBinfo>
                {
                    new USBinfo { DeviceId = "USB001", IsWhiteListed = true },
                    new USBinfo { DeviceId = "USB002", IsWhiteListed = false }
                };
                UsBxmlSerializer.SerializeToDeviceXml(initialList, _testFilePath);
                PrintXmlContent(_testFilePath); // XML 파일의 내용을 출력
                PrintObjectDetails(initialList); // 초기 객체 리스트의 상세 정보 출력
            }
        }

        [Test]
        public void SerializeDeserializeTest()
        {
            var deserializedList = UsBxmlSerializer.DeserializeFromDeviceXml(_testFilePath);
            PrintObjectDetails(deserializedList); // 역직렬화된 객체의 상세 정보 출력

            deserializedList.Add(new USBinfo { DeviceId = "USB003", IsWhiteListed = true });
            UsBxmlSerializer.SerializeToDeviceXml(deserializedList, _testFilePath);

            var updatedList = UsBxmlSerializer.DeserializeFromDeviceXml(_testFilePath);
            PrintXmlContent(_testFilePath); // 업데이트된 XML 파일의 내용을 출력
            PrintObjectDetails(updatedList); // 업데이트된 객체 리스트의 상세 정보 출력

            ClassicAssert.AreEqual(3, updatedList.Count);
            ClassicAssert.IsTrue(updatedList.Exists(usb => usb.DeviceId == "USB003" && usb.IsWhiteListed));
        }

        [TearDown]
        public void Teardown()
        {
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        private void PrintXmlContent(string filePath)
        {
            Console.WriteLine($"--- XML Content of {filePath} ---");
            Console.WriteLine(File.ReadAllText(filePath));
        }

        private void PrintObjectDetails(List<USBinfo> usbList)
        {
            Console.WriteLine("--- USB List Details ---");
            foreach (var usb in usbList)
            {
                Console.WriteLine($"DeviceId: {usb.DeviceId}, IsWhiteListed: {usb.IsWhiteListed}");
            }
        }
    }
}
