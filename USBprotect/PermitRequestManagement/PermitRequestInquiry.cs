using System.Collections.Generic;

namespace UsbSecurity
{
    internal class PermitRequestInquiry
    {
        private List<PermitRequestEnt> requests;
        private PermitRequestEnt permitrequestent = new PermitRequestEnt(); 

        public PermitRequestInquiry()
        {
            requests = permitrequestent.LoadRequests();
        }

        public List<PermitRequestEnt> GetRequests()
        {
            return requests;
        }
    }
}
