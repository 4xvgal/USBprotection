using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace USBprotect.PermitRequest
{
    public class PermitRequestList
    {
        private List<PermitRequestManagement.PermitRequest> requests;
        private readonly string filePath = "PermitRequests.xml";

        public PermitRequestList()
        {
            requests = new List<PermitRequestManagement.PermitRequest>();
            LoadRequests();
        }

        public void AddRequest(string deviceName, string requester, string reason, DateTime requestTime)
        {
            var request = new PermitRequestManagement.PermitRequest(deviceName, requester, reason, requestTime);
            requests.Add(request);
            SaveRequests();
        }

        private void SaveRequests()
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<PermitRequestManagement.PermitRequest>));
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    serializer.Serialize(stream, requests);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving requests: " + ex.Message);
            }
        }

        private void LoadRequests()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(List<PermitRequestManagement.PermitRequest>));
                    using (FileStream stream = new FileStream(filePath, FileMode.Open))
                    {
                        requests = (List<PermitRequestManagement.PermitRequest>)serializer.Deserialize(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading requests: " + ex.Message);
            }
        }

        public List<PermitRequestManagement.PermitRequest> GetRequests()
        {
            return requests;
        }
    }
}
