using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using UsbSecurity;

// ClassicAssert를 위한 네임스페이스 추가
// USBprotect.USBmanagement.test.UsBxmlSerializerTests 클래스:
// 이 클래스는 USB 장치 정보의 직렬화 및 역직렬화를 테스트하기 위한 NUnit 테스트 클래스입니다.
// XML 파일을 사용하여 USB 장치 정보를 저장하고 불러오는 기능의 정확성을 검증합니다.

// [SetUp] Setup() 메서드:
// 테스트를 시작하기 전에 필요한 사전 준비를 수행합니다.
// 지정된 파일 경로에 XML 파일이 존재하지 않는 경우, 초기 USB 정보 리스트를 생성하여 XML 파일로 직렬화하고,
// 생성된 파일의 내용과 초기 객체 리스트의 상세 정보를 콘솔에 출력합니다.

// [Test] SerializeDeserializeTest() 메서드:
// USB 정보의 직렬화 및 역직렬화 기능을 테스트합니다.
// 먼저 XML 파일에서 USB 정보를 역직렬화하여 리스트로 가져옵니다.
// 리스트에 새로운 USB 정보를 추가한 후, 다시 XML 파일로 직렬화합니다.
// 업데이트된 XML 파일과 객체 리스트의 내용을 콘솔에 출력하고,
// 리스트에 정상적으로 정보가 추가되었는지, 리스트의 개수가 기대하는 값(3)과 일치하는지 검증합니다.

// [TearDown] Teardown() 메서드:
// 각 테스트 실행 후 호출됩니다.
// 테스트에 사용된 XML 파일을 삭제하여 테스트 간의 상호 영향을 방지하고 다음 테스트가 깨끗한 환경에서 시작할 수 있도록 합니다.

// PrintXmlContent(string filePath) 메서드:
// 지정된 XML 파일의 내용을 콘솔에 출력합니다.
// 파일의 전체 텍스트를 읽어 콘솔에 출력하여 XML 파일의 현재 상태를 검토할 수 있게 합니다.

// PrintObjectDetails(List<USBinfo> usbList) 메서드:
// USB 정보 객체 리스트의 상세 정보를 콘솔에 출력합니다.
// 각 객체의 DeviceId와 IsWhiteListed 속성을 출력하여,
// 리스트의 각 항목이 올바르게 직렬화 및 역직렬화되었는지 확인할 수 있습니다.


namespace USBprotect.USBmanagement.test
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
