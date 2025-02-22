using CustomerManager.Domain.Entities.DTO;
using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Interface.Repository;
using CustomerManager.Domain.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CustomerManager.Domain.Entities.Generic;

namespace CustomerManager.Service.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IImageService _imageService;

        public CustomerService(ICustomerRepository customerRepository, IAddressRepository addressRepository, IImageService imageService)
        {
            _customerRepository = customerRepository;
            _addressRepository = addressRepository;
            _imageService = imageService;
        }

        #region :: Customer ::

        public async Task<MessageResponse<IEnumerable<CustomerDto>>> GetAllAsync()
        {
            try
            {
                var customers = await _customerRepository.GetAllAsync(includes: x => x.Addresses);

                return new MessageResponse<IEnumerable<CustomerDto>>
                {
                    Success = true,
                    Message = "OK",
                    Data = customers.Select(c => new CustomerDto
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Email = c.Email,
                        CompanyLogo = c.CompanyLogo,
                        Addresses = c.Addresses
                    })
                };
            }
            catch (Exception ex)
            {
                return new MessageResponse<IEnumerable<CustomerDto>>
                {
                    Success = false,
                    Message = $"Ocorreu um erro ao listar todos os clientes! {ex.Message}"
                };
            }

        }

        public async Task<MessageResponse<CustomerDto>> GetByIdAsync(int id)
        {
            try
            {
                var customer = await _customerRepository.GetByIdAsync(id, includes: x => x.Addresses);
                if (customer == null)
                    return new MessageResponse<CustomerDto> { Success = false, Message = "Cliente não encontrado." };

                return new MessageResponse<CustomerDto>
                {
                    Success = true,
                    Message = "OK",
                    Data = new CustomerDto
                    {
                        Id = customer.Id,
                        Name = customer.Name,
                        Email = customer.Email,
                        CompanyLogo = customer.CompanyLogo,
                        Addresses = customer.Addresses
                    }
                };
            }
            catch (Exception ex)
            {
                return new MessageResponse<CustomerDto>
                {
                    Success = false,
                    Message = $"Ocorreu um erro ao buscar pelo cliente! {ex.Message}"
                };
            }
        }

        public async Task<MessageResponse<CustomerDto>> CreateAsync(CustomerRequestDto customerRequest, IFormFile companyLogo)
        {
            var existingCustomer = await _customerRepository.GetByEmailAsync(customerRequest.Email);
            if (existingCustomer != null)
                return new MessageResponse<CustomerDto> { Success = false, Message = "Já existe um cliente com este e-mail." };

            var customer = new Customer
            {
                Name = customerRequest.Name,
                Email = customerRequest.Email,
                CompanyLogo = _imageService.ConvertIFormFileToBase64(companyLogo),
            };

            if (customerRequest.Addresses != null)
            {
                foreach (var address in customerRequest.Addresses)
                {
                    customer.Addresses.Add(new Address { Name = address.Name });
                }
            }

            var createdCustomer = await _customerRepository.AddAsync(customer);

            return new MessageResponse<CustomerDto>
            {
                Success = true,
                Message = "OK",
                Data = new CustomerDto
                {
                    Id = createdCustomer.Id,
                    Name = createdCustomer.Name,
                    Email = createdCustomer.Email,
                    CompanyLogo = createdCustomer.CompanyLogo,
                    Addresses = createdCustomer.Addresses
                }
            };
        }

        public async Task<MessageResponse<CustomerDto>> UpdateAsync(int id, CustomerRequestDto customerRequest, IFormFile companyLogo)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(id, includes: x => x.Addresses);
            if (existingCustomer == null)
                return new MessageResponse<CustomerDto> { Success = false, Message = "Cliente não encontrado." };

            existingCustomer.Name = customerRequest.Name;
            existingCustomer.Email = customerRequest.Email;

            if (companyLogo != null)
                existingCustomer.CompanyLogo = _imageService.ConvertIFormFileToBase64(companyLogo);

            var existingAddresses = existingCustomer.Addresses.ToList();

            var addressesToRemove = existingAddresses.Where(ea => !customerRequest.Addresses.Select(a => a.Id).Contains(ea.Id)).ToList();
            foreach (var address in addressesToRemove)
            {
                existingCustomer.Addresses.Remove(address);
            }

            foreach (var addressDto in customerRequest.Addresses)
            {
                var existingAddress = existingAddresses.FirstOrDefault(a => a.Id == addressDto.Id);

                if (existingAddress != null)
                    existingAddress.Name = addressDto.Name;
                else
                {
                    existingCustomer.Addresses.Add(new Address
                    {
                        Name = addressDto.Name,
                        CustomerId = existingCustomer.Id
                    });
                }
            }

            var updatedCustomer = await _customerRepository.UpdateAsync(existingCustomer);

            return new MessageResponse<CustomerDto>
            {
                Success = true,
                Message = "OK",
                Data = new CustomerDto
                {
                    Id = updatedCustomer.Id,
                    Name = updatedCustomer.Name,
                    Email = updatedCustomer.Email,
                    CompanyLogo = updatedCustomer.CompanyLogo,
                    Addresses = updatedCustomer.Addresses
                }
            };
        }

        public async Task<MessageResponse<object>> DeleteAsync(int id)
        {
            try
            {
                await _customerRepository.DeleteAsync(id);
                return new MessageResponse<object> { Data = true, Success = true, Message = "OK" };
            }
            catch (Exception ex)
            {
                return new MessageResponse<object> { Success = false, Message = $"Erro ao excluir um cliente. {ex.Message}" };
            }
        }

        #endregion

        #region :: Address ::

        public async Task<MessageResponse<IEnumerable<AddressDto>>> GetAllAddressAsync()
        {
            try
            {
                var customers = await _addressRepository.GetAllAsync();

                return new MessageResponse<IEnumerable<AddressDto>>
                {
                    Success = true,
                    Message = "OK",
                    Data = customers.Select(c => new AddressDto
                    {
                        Id = c.Id,
                        CustomerId = c.CustomerId,
                        Name = c.Name
                    })
                };
            }
            catch (Exception ex)
            {
                return new MessageResponse<IEnumerable<AddressDto>>
                {
                    Success = false,
                    Message = $"Ocorreu um erro ao listar todos os endereços! {ex.Message}"
                };
            }
        }

        public async Task<MessageResponse<AddressDto>> GetAddressByIdAsync(int id)
        {
            try
            {
                var address = await _addressRepository.GetByIdAsync(id);
                if (address == null)
                    return new MessageResponse<AddressDto> { Success = false, Message = "Endereço não encontrado." };

                return new MessageResponse<AddressDto>
                {
                    Success = true,
                    Message = "OK",
                    Data = new AddressDto
                    {
                        Id = address.Id,
                        CustomerId = address.CustomerId,
                        Name = address.Name
                    }
                };
            }
            catch (Exception ex)
            {
                return new MessageResponse<AddressDto>
                {
                    Success = false,
                    Message = $"Ocorreu um erro ao buscar pelo endereço! {ex.Message}"
                };
            }
        }

        public async Task<MessageResponse<AddressDto>> CreateAddressAsync(AddressDto addressRequest)
        {
            var existingCustomer = await _customerRepository.GetByIdAsync(addressRequest.CustomerId);
            if (existingCustomer == null)
                return new MessageResponse<AddressDto> { Success = false, Message = "Cliente não encontrado." };

            var address = new Address
            {
                CustomerId = addressRequest.CustomerId,
                Name = addressRequest.Name
            };

            var createdAddress = await _addressRepository.AddAsync(address);

            return new MessageResponse<AddressDto>
            {
                Success = true,
                Message = "OK",
                Data = new AddressDto
                {
                    Id = createdAddress.Id,
                    CustomerId = createdAddress.CustomerId,
                    Name = createdAddress.Name
                }
            };
        }

        public async Task<MessageResponse<AddressDto>> UpdateAddressAsync(AddressDto addressRequest)
        {
            var existingAddress = await _addressRepository.GetByIdAsync(addressRequest.Id);
            if (existingAddress == null)
                return new MessageResponse<AddressDto> { Success = false, Message = "Endereço não encontrado." };

            existingAddress.Name = addressRequest.Name;

            var updatedAddress = await _addressRepository.UpdateAsync(existingAddress);

            return new MessageResponse<AddressDto>
            {
                Success = true,
                Message = "OK",
                Data = new AddressDto
                {
                    Id = updatedAddress.Id,
                    CustomerId = updatedAddress.CustomerId,
                    Name = updatedAddress.Name
                }
            };
        }

        public async Task<MessageResponse<object>> DeleteAddressAsync(int id)
        {
            try
            {
                await _addressRepository.DeleteAsync(id);
                return new MessageResponse<object> { Data = true, Success = true, Message = "OK" };
            }
            catch (Exception ex)
            {
                return new MessageResponse<object> { Success = false, Message = $"Erro ao excluir um endereço. {ex.Message}" };
            }
        }

        #endregion
    }
}
