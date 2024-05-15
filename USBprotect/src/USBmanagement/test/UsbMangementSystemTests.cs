using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using UsbSecurity;

namespace USBprotect.USBmanagement.test
{
    [TestFixture]
    public class UsbManagementSystemTests
    {
        private UsbManagementSystem _usbManagementSystem;
        private string _testFilePath = "testUsbData.xml";

        [SetUp]
        public void Setup()
        {
            // 테스트용 파일 경로 설정
            _usbManagementSystem = new UsbManagementSystem(_testFilePath);

            // 테스트 전에 파일이 존재하는 경우 삭제
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        [Test]
        public void AddWhiteListedUsb_AddsToWhiteList()
        {
            var usbInfo = new USBinfo { DeviceId = "USB001", IsWhiteListed = true };
            var result = _usbManagementSystem.AddWhiteListedUsb(usbInfo);

            ClassicAssert.IsTrue(result);
            ClassicAssert.Contains(usbInfo, _usbManagementSystem.GetWhiteListedUsb());
        }

        [Test]
        public void AddBlackListedUsb_AddsToBlackList()
        {
            var usbInfo = new USBinfo { DeviceId = "USB002", IsWhiteListed = false };
            var result = _usbManagementSystem.AddBlackListedUsb(usbInfo);

            ClassicAssert.IsTrue(result);
            ClassicAssert.Contains(usbInfo, _usbManagementSystem.GetBlackListedUsb());
        }

        [Test]
        public void RemoveWhiteListedUsb_RemovesFromWhiteList()
        {
            var usbInfo = new USBinfo { DeviceId = "USB003", IsWhiteListed = true };
            _usbManagementSystem.AddWhiteListedUsb(usbInfo);
            var result = _usbManagementSystem.RemoveWhiteListedUsb(usbInfo);

           ClassicAssert.IsTrue(result);
            ClassicAssert.IsFalse(_usbManagementSystem.GetWhiteListedUsb().Contains(usbInfo));
            
        }

        [Test]
        public void RemoveBlackListedUsb_RemovesFromBlackList()
        {
            var usbInfo = new USBinfo { DeviceId = "USB004", IsWhiteListed = false };
            _usbManagementSystem.AddBlackListedUsb(usbInfo);
            var result = _usbManagementSystem.RemoveBlackListedUsb(usbInfo);

            ClassicAssert.IsTrue(result);
            ClassicAssert.IsFalse(_usbManagementSystem.GetBlackListedUsb().Contains(usbInfo));
        }

        [Test]
        public void LoadAllUsb_LoadsDataCorrectly()
        {
            // Prepare test data and serialize it
            var usbList = new List<USBinfo>
            {
                new USBinfo { DeviceId = "USB005", IsWhiteListed = true },
                new USBinfo { DeviceId = "USB006", IsWhiteListed = false }
            };
            UsBxmlSerializer.SerializeToDeviceXml(usbList, _testFilePath);

            // Test loading function
            _usbManagementSystem.LoadAllUsb();
            ClassicAssert.IsTrue(_usbManagementSystem.GetWhiteListedUsb().Exists(u => u.DeviceId == "USB005"));
            ClassicAssert.IsTrue(_usbManagementSystem.GetBlackListedUsb().Exists(u => u.DeviceId == "USB006"));
        }

        [Test]
        public void DeleteAllUsb_DeletesFile()
        {
            // Create a file to delete
            File.WriteAllText(_testFilePath, "Dummy content");
            _usbManagementSystem.DeleteAllUsb();

            ClassicAssert.IsFalse(File.Exists(_testFilePath));
        }

        [TearDown]
        public void Teardown()
        {
            // Clean up test data file after each test
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }
    }
}
