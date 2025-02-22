using Azure;
using CustomerManager.Domain.Entities;
using CustomerManager.Domain.Entities.DTO;
using CustomerManager.Domain.Interface.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CustomerManager.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IImageService _imageService;
        public CustomerController(ICustomerService customerService, IImageService imageService)
        {
            _customerService = customerService;
            _imageService = imageService;
        }

        #region :: Customer ::

        /// <summary>
        /// Obtém todos os clientes cadastrados.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customersResponse = await _customerService.GetAllAsync();
            if (customersResponse.Success)
                return Ok(customersResponse);
            else
                return BadRequest(customersResponse);
        }

        /// <summary>
        /// Obtém um cliente específico pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customerResponse = await _customerService.GetByIdAsync(id);

            if (customerResponse.Success)
                return Ok(customerResponse);
            else
                return BadRequest(customerResponse);
        }

        /// <summary>
        /// Cria um novo cliente com upload de logotipo.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] string customerRequest,
            [FromForm] IFormFile companyLogo)
        {
            if (companyLogo == null || companyLogo.Length == 0)
                return BadRequest("O logotipo é obrigatório.");

            var customerRequestDto = JsonConvert.DeserializeObject<CustomerRequestDto>(customerRequest);

            var createdCustomerResponse = await _customerService.CreateAsync(customerRequestDto, companyLogo);
            if (createdCustomerResponse.Success)
                return Ok(createdCustomerResponse);
            else
                return BadRequest(createdCustomerResponse);
            
        }

        /// <summary>
        /// Atualiza um cliente existente pelo ID (opcionalmente, pode alterar a imagem).
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            [FromForm] string customerRequest, 
            [FromForm] IFormFile? companyLogo)
        {
            var customerRequestDto = JsonConvert.DeserializeObject<CustomerRequestDto>(customerRequest);

            var updatedCustomerResponse = await _customerService.UpdateAsync(customerRequestDto.Id, customerRequestDto, companyLogo);

            if (updatedCustomerResponse.Success)
                return Ok(updatedCustomerResponse);
            else
                return BadRequest(updatedCustomerResponse);
        }

        /// <summary>
        /// Remove um cliente pelo ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _customerService.DeleteAsync(id);
            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        #endregion

        #region :: Address ::

        /// <summary>
        /// Obtém todos os endereços cadastrados.
        /// </summary>
        [HttpGet("Address")]
        public async Task<IActionResult> GetAllAddress()
        {
            var addressResponse = await _customerService.GetAllAddressAsync();
            if (addressResponse.Success)
                return Ok(addressResponse);
            else
                return BadRequest(addressResponse);
        }

        /// <summary>
        /// Obtém um endereço específico pelo ID.
        /// </summary>
        [HttpGet("Address/{id}")]
        public async Task<IActionResult> GetAddressById(int id)
        {
            var addressResponse = await _customerService.GetAddressByIdAsync(id);

            if (addressResponse.Success)
                return Ok(addressResponse);
            else
                return BadRequest(addressResponse);
        }

        /// <summary>
        /// Cria um novo endereço para um cliente.
        /// </summary>
        [HttpPost("Address")]
        public async Task<IActionResult> CreateAddress(
            [FromBody] AddressDto addressRequest)
        {
            var createdAddressResponse = await _customerService.CreateAddressAsync(addressRequest);
            if (createdAddressResponse.Success)
                return Ok(createdAddressResponse);
            else
                return BadRequest(createdAddressResponse);

        }

        /// <summary>
        /// Atualiza um endereço existente pelo ID.
        /// </summary>
        [HttpPut("Address/{id}")]
        public async Task<IActionResult> UpdateAddress(
            [FromBody] AddressDto addressRequest)
        { 
            var updatedAddressResponse = await _customerService.UpdateAddressAsync(addressRequest);

            if (updatedAddressResponse.Success)
                return Ok(updatedAddressResponse);
            else
                return BadRequest(updatedAddressResponse);
        }

        /// <summary>
        /// Remove um endereço pelo ID.
        /// </summary>
        [HttpDelete("Address/{id}")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var response = await _customerService.DeleteAddressAsync(id);
            if (response.Success)
                return Ok(response);
            else
                return BadRequest(response);
        }

        #endregion
    }
}
