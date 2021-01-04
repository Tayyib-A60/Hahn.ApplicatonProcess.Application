using System;

namespace Hahn.ApplicatonProcess.December2020.Data.Models
{
    [Serializable]
    public class BaseEntity
    {
        public Guid ID { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}