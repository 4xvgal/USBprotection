﻿
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

           // 아래에 장치가 올바르게 차단되었는지 검증하는 코드 
           // 장치가 차단되었는지 확인하는 코드

        }
        public void WhiteToBlack(string deviceid) //나중에 버튼 액션
        {
            // 블랙리스트 디바이스 중 특정 장치 ID 를 가지는 객체를 화이트 리스트로 이동 (LINQ) 로 구현
            USBinfo.BlackListDevices.Add(USBinfo.WhiteListDevices.Where(x => x.DeviceId == deviceid).FirstOrDefault());
        }

    }
}