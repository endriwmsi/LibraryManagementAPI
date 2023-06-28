using AutoMapper;
using LibraryManagementAPI.Domain.DTOs;
using LibraryManagementAPI.Domain.Interfaces;
using LibraryManagementAPI.Domain.Entities;
using LibraryManagementAPI.Domain.ViewModels;
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

            return Ok();
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

            return Ok();
        }

        [HttpPost]
        public IActionResult CreateUser(UserViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = _mapper.Map<User>(userViewModel);
            _userRepository.AddUser(user);

            var userDTO = _mapper.Map<UserDTO>(user);

            return CreatedAtAction(nameof(GetUserById), new { id = userDTO.Id }, userDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, UserViewModel userViewModel)
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

            _mapper.Map(userViewModel, user);
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
