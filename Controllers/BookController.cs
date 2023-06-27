using AutoMapper;
using LibraryManagementAPI.Domain.DTOs;
using LibraryManagementAPI.Domain.Interfaces;
using LibraryManagementAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LibraryManagementAPI.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks();
            var bookDTOs = _mapper.Map<List<BookDTO>>(books);

            return Ok(bookDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _bookRepository.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            var bookDTO = _mapper.Map<BookDTO>(book);

            return Ok(bookDTO);
        }

        [HttpPost]
        public IActionResult CreateBook(BookDTO bookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var book = _mapper.Map<Book>(bookDTO);
            _bookRepository.AddBook(book);

            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, BookDTO bookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var book = _bookRepository.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            _mapper.Map(bookDTO, book);
            _bookRepository.UpdateBook(book);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _bookRepository.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookRepository.DeleteBook(book);

            return NoContent();
        }
    }
}