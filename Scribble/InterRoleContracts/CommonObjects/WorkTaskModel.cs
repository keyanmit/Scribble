using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterRoleContracts.Enums;

namespace InterRoleContracts.CommonObjects
{
    public class WorkTaskModel
    {
        public TaskListEnumeration RequestType { get; set; }
        public UInt64 Id { get; set; }
        public string RequestData { get; set; }
        public Guid RequestId { get; set; }

        public void Validate()
        {
            switch (this.RequestType)
            {
                case TaskListEnumeration.PersistNewPaste:
                    if (Id == 0 || Id > (1L << 62))
                    {
                        throw new Exception("Work Task Id out of bound exception. " + this);
                    }
                    if (string.IsNullOrEmpty(RequestData))
                    {
                        throw new Exception("Request data is not valid. " + this);
                    }
                    break;
                default:
                    // should not reach this path
                    throw new Exception("Request type is in valid." + this);
            }
        }

        public override string ToString()
        {
            return "Request Type: " + RequestType +
                   "Id: " + Id +
                   "Request Id: " + RequestId +
                   "Request Data: " + RequestData;
        }
    }
}
