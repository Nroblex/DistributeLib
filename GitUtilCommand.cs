using System;
using System.Runtime.Serialization;

namespace DistributeLib
{
    [DataContract]
    public class GitUtilCommnad
    {
        
        [DataMember]
        public string ExternalFTPPath { get; set; }

        [DataMember]
        public string ExternalFTPUser { get; set; }

        [DataMember]
        public string ExternalFTPPassword { get; set; }

        [DataMember]
        public string BuildNumber { get; set; }

        [DataMember]
        public string PathToBuild { get; set; }

        [DataMember]
        public DateTime CreatedAt { get; set; }


    }
}