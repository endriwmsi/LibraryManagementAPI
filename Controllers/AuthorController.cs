using AutoMapper;
using LibraryManagementAPI.Domain.DTOs;
using LibraryManagementAPI.Domain.Interfaces;
using LibraryManagementAPI.Domain.Entities;
using LibraryManagementAPI.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/author")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            var authors = _authorRepository.GetAllAuthors();
            var authorDTOs = _mapper.Map<List<AuthorDTO>>(authors);

            return Ok(authorDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var author = _authorRepository.GetAuthorById(id);

            if (author == null)
            {
                return NotFound();
            }

            var authorDTO = _mapper.Map<AuthorDTO>(author);

            return Ok(authorDTO);
        }

        [HttpPost]
        public IActionResult CreateAuthor(AuthorViewModel authorViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var author = _mapper.Map<Author>(authorViewModel);
            _authorRepository.AddAuthor(author);

            var authorDTO = _mapper.Map<AuthorDTO>(author);

            return CreatedAtAction(nameof(GetAuthorById), new { id = authorDTO.Id }, authorDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, AuthorViewModel authorViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var author = _authorRepository.GetAuthorById(id);

            if (author == null)
            {
                return NotFound();
            }

            _mapper.Map(authorViewModel, author);
            _authorRepository.UpdateAuthor(author);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = _authorRepository.GetAuthorById(id);

            if (author == null)
            {
                return NotFound();
            }

            _authorRepository.DeleteAuthor(author);

            return NoContent();
        }
    }
}
