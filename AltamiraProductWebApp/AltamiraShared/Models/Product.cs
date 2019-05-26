using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace AltamiraShared.Models
{
    [DataContract]
    public class Product
    {
        [DataMember(Name = "Id")]
        public int Id { get; set; }
        [DataMember(Name = "Name")]
        public string Name { get; set; }
        [DataMember(Name = "Brand")]
        public string Brand { get; set; }
        [DataMember(Name = "Price")]
        public double Price { get; set; }
    }
}
