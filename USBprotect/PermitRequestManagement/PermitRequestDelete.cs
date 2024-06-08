using System;
using System.Collections.Generic;

namespace UsbSecurity
{
    internal class PermitRequestDelete
    {
        private List<PermitRequestEnt> requests;

        public PermitRequestDelete()
        {
            requests = PermitRequestEnt.LoadRequests();
        }

        public void RemoveRequest(int index)
        {
            if (index >= 0 && index < requests.Count)
            {
                requests.RemoveAt(index);
                PermitRequestEnt.SaveRequests(requests);
            }
            else
            {
                throw new ArgumentOutOfRangeException("index", "Index is out of range.");
            }
        }
    }
}
