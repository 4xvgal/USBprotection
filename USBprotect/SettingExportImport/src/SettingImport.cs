using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml.Serialization;
using UsbSecurity;

public class SettingImport
{
    // 지정된 파일 경로에서 USB 정보를 불러오는 메서드
    public ObservableCollection<USBinfo> LoadFromDeviceFile(string filepath)
    {
        // ObservableCollection<USBinfo> 타입에 대한 XmlSerializer 인스턴스 생성
        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<USBinfo>));

        // 파일 스트림을 열고, 지정된 파일 경로의 데이터를 읽어들임
        using (FileStream stream = new FileStream(filepath, FileMode.Open))
        {
            // XML 데이터를 역직렬화하여 ObservableCollection<USBinfo> 객체로 변환
            return (ObservableCollection<USBinfo>)serializer.Deserialize(stream);
        }
    }
}
