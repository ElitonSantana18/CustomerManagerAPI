using CustomerManager.Domain.Entities.DTO;
using CustomerManager.Domain.Entities.Generic;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerManager.Domain.Interface.Service
{
    public interface ICustomerService
    {
        #region :: Customer ::

        Task<MessageResponse<IEnumerable<CustomerDto>>> GetAllAsync();
        Task<MessageResponse<CustomerDto>> GetByIdAsync(int id);
        Task<MessageResponse<CustomerDto>> CreateAsync(CustomerRequestDto customerRequest, IFormFile companyLogo);
        Task<MessageResponse<CustomerDto>> UpdateAsync(int id, CustomerRequestDto customerRequest, IFormFile companyLogo);
        Task<MessageResponse<object>> DeleteAsync(int id);

        #endregion

        #region :: Address ::

        Task<MessageResponse<IEnumerable<AddressDto>>> GetAllAddressAsync();
        Task<MessageResponse<AddressDto>> GetAddressByIdAsync(int id);
        Task<MessageResponse<AddressDto>> CreateAddressAsync(AddressDto customerRequest);
        Task<MessageResponse<AddressDto>> UpdateAddressAsync(AddressDto customerRequest);
        Task<MessageResponse<object>> DeleteAddressAsync(int id);

        #endregion

    }
}
