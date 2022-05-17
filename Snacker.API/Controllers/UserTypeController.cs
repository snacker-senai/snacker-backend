using Microsoft.AspNetCore.Mvc;
using Snacker.Domain.Entities;
using Snacker.Domain.Interfaces;
using Snacker.Domain.Validators;
using System;

namespace Snacker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTypeController : ControllerBase
    {
        private readonly IBaseService<UserType> _baseUserTypeService;

        public UserTypeController(IBaseService<UserType> baseUserTypeService)
        {
            _baseUserTypeService = baseUserTypeService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] UserType userType)
        {
            if (userType == null)
                return NotFound();

            return Execute(() => _baseUserTypeService.Add<UserTypeValidator>(userType).Id);
        }

        [HttpPut]
        public IActionResult Update([FromBody] UserType userType)
        {
            if (userType == null)
                return NotFound();

            return Execute(() => _baseUserTypeService.Update<UserTypeValidator>(userType));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _baseUserTypeService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _baseUserTypeService.Get());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _baseUserTypeService.GetById(id));
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
