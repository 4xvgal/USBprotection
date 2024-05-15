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
            // 테스트를 위한 USB 관리 시스템 초기화
            _usbManagementSystem = new UsbManagementSystem(_testFilePath, _testFilePath, _testFilePath);

            // 테스트를 위해 USB 정보를 초기화하고, 기본 데이터를 제공
            var whiteUsb = new USBinfo { DeviceId = "WHITE1", IsWhiteListed = true };
            var blackUsb = new USBinfo { DeviceId = "BLACK1", IsWhiteListed = false };

            _usbManagementSystem.AddWhiteListedUsb(whiteUsb);
            _usbManagementSystem.AddBlackListedUsb(blackUsb);

            // USB 정보를 XML 파일로 저장
            _usbManagementSystem.SaveAllUsb();
        }

        [TearDown]
        public void Teardown()
        {
            // 테스트가 끝난 후 파일 제거
            if (File.Exists(_testFilePath))
            {
                File.Delete(_testFilePath);
            }
        }

        [Test]
        public void AddWhiteListedUsb_ShouldAddUsbToWhiteList()
        {
            var newUsb = new USBinfo { DeviceId = "WHITE2", IsWhiteListed = true };
            _usbManagementSystem.AddWhiteListedUsb(newUsb);

            var result = _usbManagementSystem.GetWhiteListedUsb();

            CollectionAssert.Contains(result, newUsb);
        }

        [Test]
        public void AddBlackListedUsb_ShouldAddUsbToBlackList()
        {
            var newUsb = new USBinfo { DeviceId = "BLACK2", IsWhiteListed = false };
            _usbManagementSystem.AddBlackListedUsb(newUsb);

            var result = _usbManagementSystem.GetBlackListedUsb();

            CollectionAssert.Contains(result, newUsb);
        }

        [Test]
        public void RemoveWhiteListedUsb_ShouldRemoveUsbFromWhiteList()
        {
            var usbToRemove = new USBinfo { DeviceId = "WHITE1", IsWhiteListed = true };
            _usbManagementSystem.RemoveWhiteListedUsb(usbToRemove);

            var result = _usbManagementSystem.GetWhiteListedUsb();

            ClassicAssert.IsFalse(result.Contains(usbToRemove));
        }

        [Test]
        public void RemoveBlackListedUsb_ShouldRemoveUsbFromBlackList()
        {
            var usbToRemove = new USBinfo { DeviceId = "BLACK1", IsWhiteListed = false };
            _usbManagementSystem.RemoveBlackListedUsb(usbToRemove);

            var result = _usbManagementSystem.GetBlackListedUsb();

            ClassicAssert.IsFalse(result.Contains(usbToRemove));
        }

        [Test]
        public void LoadAllUsb_ShouldLoadUsbInfoFromXml()
        {
            _usbManagementSystem.LoadAllUsb();

            ClassicAssert.IsTrue(_usbManagementSystem.GetWhiteListedUsb().Count > 0);
            ClassicAssert.IsTrue(_usbManagementSystem.GetBlackListedUsb().Count > 0);
        }

        [Test]
        public void DeleteAllUsb_ShouldClearAllUsbData()
        {
            _usbManagementSystem.DeleteAllUsb();

            ClassicAssert.IsEmpty(_usbManagementSystem.GetWhiteListedUsb());
            ClassicAssert.IsEmpty(_usbManagementSystem.GetBlackListedUsb());
            ClassicAssert.IsFalse(File.Exists(_testFilePath));
        }
    }
}
