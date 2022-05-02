using Microsoft.AspNetCore.Mvc;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Validators;
using System;

namespace Snacker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private IBaseService<Address> _baseAddressService;

        public AddressController(IBaseService<Address> baseAddressService)
        {
            _baseAddressService = baseAddressService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Address address)
        {
            if (address == null)
                return NotFound();

            return Execute(() => _baseAddressService.Add<AddressValidator>(address).Id);
        }

        [HttpPut]
        public IActionResult Update([FromBody] Address address)
        {
            if (address == null)
                return NotFound();

            return Execute(() => _baseAddressService.Update<AddressValidator>(address));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseAddressService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _baseAddressService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _baseAddressService.GetById(id));
        }

        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
