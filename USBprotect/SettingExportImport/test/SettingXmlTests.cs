using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;

namespace UsbSecurity.Tests
{
    [TestFixture]
    public class SettingXmlTests
    {
        private MemoryStream _memoryStream;
        private XmlSerializer _serializer;

        [SetUp]
        public void Setup()
        {
            _memoryStream = new MemoryStream();
            _serializer = new XmlSerializer(typeof(ObservableCollection<USBinfo>));
        }

        [TearDown]
        public void TearDown()
        {
            _memoryStream.Dispose();
        }

        [Test]
        public void TestSaveAndLoadUSBInfo()
        {
            // Arrange
            var usbInfos = new ObservableCollection<USBinfo>
            {
                new USBinfo { DeviceName = "USB Device 1", DeviceId = "001", Status = "Active" },
                new USBinfo { DeviceName = "USB Device 2", DeviceId = "002", Status = "Inactive" }
            };

            // Act - Save to MemoryStream
            _serializer.Serialize(_memoryStream, usbInfos);
            _memoryStream.Position = 0;  // Reset stream position for reading

            // Act - Load from MemoryStream
            var loadedUsbInfos = (ObservableCollection<USBinfo>)_serializer.Deserialize(_memoryStream);

            // Assert
            ClassicAssert.AreEqual(usbInfos.Count, loadedUsbInfos.Count, "The number of USB info items should match.");
            for (int i = 0; i < usbInfos.Count; i++)
            {
                ClassicAssert.AreEqual(usbInfos[i].DeviceName, loadedUsbInfos[i].DeviceName, "Device names should match.");
                ClassicAssert.AreEqual(usbInfos[i].DeviceId, loadedUsbInfos[i].DeviceId, "Device IDs should match.");
                ClassicAssert.AreEqual(usbInfos[i].Status, loadedUsbInfos[i].Status, "Device statuses should match.");
            }
        }
    }
}
