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
        private readonly IAuthorBookRepository _authorBookRepository;
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

            return Ok();
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

            return Ok();
        }

        [HttpPost]
        public IActionResult AddBook(BookViewModel bookViewModel)
        {
            var book = _mapper.Map<Book>(bookViewModel);

            // Adiciona o autor ao livro
            foreach (var authorId in bookViewModel.AuthorsId)
            {
                var author = _authorRepository.GetAuthorById(authorId);
                if (author == null)
                {
                    author = new Author { Id = authorId };
                    _authorRepository.AddAuthor(author);
                }
                book.Authors.Add(author);
            }
            _bookRepository.AddBook(book);

            var BookDTO = _mapper.Map<BookDTO>(book);

            return CreatedAtAction(nameof(GetBookById), new { id = BookDTO.Id }, BookDTO);
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
