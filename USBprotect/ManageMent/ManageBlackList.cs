
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace UsbSecurity
{
    class ManageBlackList
    {

        DevconCMD devconCMD = new DevconCMD(); // DevconCMD 클래스의 인스턴스 생성

        public ManageBlackList()
        {
            USBinfo.BlackListDevices.CollectionChanged += BlackList_CollectionChanged; // 블랙리스트 컬렉션 변경 이벤트 핸들러 추가 (블랙리스트 컬렉션에 변경이 발생할 때 호출되는 메서드)
        }

        // blackList 컬렉션에 변경이 발생할 때 호출되는 메서드
        private void BlackList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // 변경 유형이 추가일 경우 실행 (ㄹㅇ 이게 뭐냐면 새로운 항목이 추가되었을 때 실행)
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                
                foreach (var Item in e.NewItems) // 새로 추가된 항목들을 가져옴
                {
                    USBinfo newItem = Item as USBinfo; // USBinfo 클래스로 캐스팅
                    disableEveryDevice(newItem.DeviceId); // 장치 비활성화 해버리기
                }
            }
        }
        public void disableEveryDevice(string DeviceId) // 블랙리스트 장치 비활성화 메서드
        {
                devconCMD.DevconCommand($"disable \"{DeviceId}\""); //명령어 실행   
        }
       
    }
}