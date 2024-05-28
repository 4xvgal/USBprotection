using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace USBprotect.PermitRequest
{
    public class PermitRequester
    {
        private readonly string filePath = "PermitRequests.xml"; // 요청 목록 XML 파일 경로

        // 요청을 보내는 메서드
        public void SendRequest(PermitRequestEnt request)
        {
            try
            {
                List<PermitRequestEnt> requests;

                if (File.Exists(filePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<PermitRequestEnt>));
                    using (FileStream stream = new FileStream(filePath, FileMode.Open))
                    {
                        requests = (List<PermitRequestEnt>)serializer.Deserialize(stream);
                    }
                }
                else
                {
                    requests = new List<PermitRequestEnt>();
                }

                requests.Add(request);

                XmlSerializer requestSerializer = new XmlSerializer(typeof(List<PermitRequestEnt>));
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    requestSerializer.Serialize(stream, requests);
                }

                MessageBox.Show("요청이 전송되었습니다.");
            }
            catch (Exception ex)
            {
                throw new Exception("요청을 저장하는 중 오류 발생: " + ex.Message);
            }
        }
    }
}
