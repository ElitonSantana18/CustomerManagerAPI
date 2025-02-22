using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Entities.DTO
{
    public class CustomerRequestDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        
        public List<AddressDto> Addresses { get; set; }
    }
}
