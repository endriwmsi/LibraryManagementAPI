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
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
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
        public IActionResult CreateBook(BookViewModel bookViewModel)
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var book = _mapper.Map<Book>(bookViewModel);
            var author = _authorRepository.GetAuthorById(bookViewModel.AuthorId);

            if (author == null)
            {
                return NotFound("Author not found");
            }

            book.Author = author;
            _bookRepository.AddBook(book);

            var bookDTO = _mapper.Map<BookDTO>(book);

            return CreatedAtAction(nameof(GetBookById), new { id = bookDTO.Id }, bookDTO);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, BookViewModel bookViewModel)
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

            _mapper.Map(bookViewModel, book);
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
