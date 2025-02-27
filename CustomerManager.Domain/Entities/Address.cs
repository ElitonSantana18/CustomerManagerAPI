﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public Customer Customer { get; set; }
    }
}
