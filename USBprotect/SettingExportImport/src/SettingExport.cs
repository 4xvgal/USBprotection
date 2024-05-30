using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using UsbSecurity;

public class SettingExport
{
    // 지정된 파일 경로에 USB 정보를 XML 파일로 저장하는 메서드
    public void SaveToDeviceFile(string filepath, ObservableCollection<USBinfo> usbInfos)
    {
        // ObservableCollection<USBinfo> 타입의 객체를 XML로 직렬화하기 위한 XmlSerializer 인스턴스 생성
        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<USBinfo>));

        // 파일 스트림을 사용하여 지정된 파일 경로에 파일을 생성
        using (FileStream stream = new FileStream(filepath, FileMode.Create))
        {
            // USB 정보 컬렉션을 XML 형태로 직렬화하여 파일에 저장
            serializer.Serialize(stream, usbInfos);
        }
    }
}
