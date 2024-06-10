using System.Collections.Generic;

namespace UsbSecurity
{
    internal class PermitRequestInquiry
    {
        private List<PermitRequestEnt> requests;

        public PermitRequestInquiry()
        {
            requests = PermitRequestEnt.LoadRequests();
        }

        public List<PermitRequestEnt> GetRequests()
        {
            return requests;
        }
    }
}
