using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace UsbSecurity
{
    public class PermitRequestEnt
    {
        // USB 장치 이름
        public string DeviceName { get; set; }

        // 요청자
        public string Requester { get; set; }

        // 요청 사유
        public string Reason { get; set; }

        // 요청 일시
        public DateTime RequestTime { get; set; }

        // 장치 ID
        public string DeviceId { get; set; }

        // 파일 경로
        public static string FilePath { get; } = "PermitRequests.xml";
        private static readonly string ApprovedFilePath = "ApprovedRequests.xml";

        // 기본 생성자
        public PermitRequestEnt() { }

        // 매개변수를 받는 생성자
        public PermitRequestEnt(string deviceName, string requester, string reason, DateTime requestTime, string deviceId)
        {
            DeviceName = deviceName;
            Requester = requester;
            Reason = reason;
            RequestTime = requestTime;
            DeviceId = deviceId;
        }

        public static List<PermitRequestEnt> LoadRequests()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<PermitRequestEnt>));
                    using (FileStream stream = new FileStream(FilePath, FileMode.Open))
                    {
                        return (List<PermitRequestEnt>)serializer.Deserialize(stream);
                    }
                }
                return new List<PermitRequestEnt>();
            }
            catch (Exception ex)
            {
                throw new Exception("요청을 불러오는 중 오류 발생: " + ex.Message);
            }
        }

        public static void SaveRequests(List<PermitRequestEnt> requests)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<PermitRequestEnt>));
                using (FileStream stream = new FileStream(FilePath, FileMode.Create))
                {
                    serializer.Serialize(stream, requests);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("요청을 저장하는 중 오류 발생: " + ex.Message);
            }
        }

        public static void SaveApprovedRequest(PermitRequestEnt request)
        {
            try
            {
                List<PermitRequestEnt> approvedRequests;

                if (File.Exists(ApprovedFilePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<PermitRequestEnt>));
                    using (FileStream stream = new FileStream(ApprovedFilePath, FileMode.Open))
                    {
                        approvedRequests = (List<PermitRequestEnt>)serializer.Deserialize(stream);
                    }
                }
                else
                {
                    approvedRequests = new List<PermitRequestEnt>();
                }

                approvedRequests.Add(request);

                XmlSerializer approveSerializer = new XmlSerializer(typeof(List<PermitRequestEnt>));
                using (FileStream stream = new FileStream(ApprovedFilePath, FileMode.Create))
                {
                    approveSerializer.Serialize(stream, approvedRequests);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("승인된 요청을 저장하는 중 오류 발생: " + ex.Message);
            }
        }
    }
}
