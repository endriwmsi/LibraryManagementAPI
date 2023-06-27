using AutoMapper;
using LibraryManagementAPI.Domain.DTOs;
using LibraryManagementAPI.Domain.Interfaces;
using LibraryManagementAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            var userDTOs = _mapper.Map<List<UserDTO>>(users);

            return Ok(userDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDTO = _mapper.Map<UserDTO>(user);

            return Ok(userDTO);
        }

        [HttpPost]
        public IActionResult CreateUser(UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = _mapper.Map<User>(userDTO);
            _userRepository.AddUser(user);

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = _userRepository.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            _mapper.Map(userDTO, user);
            _userRepository.UpdateUser(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _userRepository.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            _userRepository.DeleteUser(user);

            return NoContent();
        }
    }
}