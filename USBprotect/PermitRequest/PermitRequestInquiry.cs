﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace USBprotect.PermitRequest
{
    internal class PermitRequestInquiry
    {
        private List<PermitRequest> requests; // 허용 요청을 저장하는 리스트
        private readonly string filePath = "PermitRequests.xml"; // XML 파일 경로

        public PermitRequestInquiry()   // 생성자
        {
            requests = new List<PermitRequest>(); // 리스트 초기화
            LoadRequests(); // 허용 요청 로드
        }

        public List<PermitRequest> GetRequests()     // 현재 저장된 허용 요청 리스트를 반환하는 메서드
        {
            return requests; // 요청 리스트 반환
        }

        private void LoadRequests()  // 파일에서 허용 요청을 로드하는 메서드
        {
            try
            {
                if (File.Exists(filePath)) // 파일이 존재하는지 확인
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<PermitRequest>)); // 역직렬화 객체 생성
                    using (FileStream stream = new FileStream(filePath, FileMode.Open)) // 파일 스트림 열기
                    {
                        requests = (List<PermitRequest>)serializer.Deserialize(stream); // XML을 역직렬화하여 요청 리스트에 할당
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("요청을 불러오는 중 오류 발생: " + ex.Message); // 로딩 실패시 예외 발생
            }
        }

    }
}