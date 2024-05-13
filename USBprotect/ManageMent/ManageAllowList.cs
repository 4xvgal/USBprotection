using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbSecurity;

namespace USBsecurity
{
    class ManageAllowList
    {
        DevconCMD devconCMD = new DevconCMD(); // devconCMD 클래스 인스턴스 생성 

        public ManageAllowList()
        {
           // 화이트 리스트 컬렉션 변경 이벤트 헨들러 등록
           USBinfo.WhiteListDevices.CollectionChanged += WhitekList_CollectionChanged;
        }

        // blackList 컬렉션에 변경이 발생할 때 호출되는 메서드
        private void WhitekList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // 변경 유형이 추가일 경우 실행 (ㄹㅇ 이게 뭐냐면 새로운 항목이 추가되었을 때 실행)
            if (e.Action == NotifyCollectionChangedAction.Add)
            {

                foreach (var Item in e.NewItems) // 새로 추가된 항목들을 가져옴
                {
                    USBinfo newItem = Item as USBinfo; // USBinfo 클래스로 캐스팅
                    disableEveryDevice(newItem.DeviceId); // 장치 활성화 해버리기
                }
            }
        }
        public void disableEveryDevice(string DeviceId) // 장치 활성화 메서드
        {
            devconCMD.DevconCommand($"enable \"{DeviceId}\""); //명령어 실행   
        }

        public void BlacktoWhite(string deviceid)
        {
           // 블랙리스트 디바이스 중 특정 장치 ID 를 가지는 객체를 화이트 리스트로 이동 (LINQ) 로 구현
           USBinfo.WhiteListDevices.Add(USBinfo.BlackListDevices.Where(x => x.DeviceId == deviceid).FirstOrDefault());

        }

    }
}
