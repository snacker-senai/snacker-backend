using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create([FromBody] UserType userType)
        {
            if (userType == null)
                return NotFound();

            return Execute(() => _baseUserTypeService.Add<UserTypeValidator>(userType).Id);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public IActionResult Update([FromBody] UserType userType)
        {
            if (userType == null)
                return NotFound();

            return Execute(() => _baseUserTypeService.Update<UserTypeValidator>(userType));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _baseUserTypeService.Get());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult Get(long id)
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
