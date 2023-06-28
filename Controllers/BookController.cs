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

        public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository, IAuthorBookRepository authorBookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _authorBookRepository = authorBookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks();
            var bookDTO = _mapper.Map<List<BookDTO>>(books);

            return Ok(bookDTO);
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
                    
                    // Crie um objeto AuthorBook e atribua os valores apropriados
                    var authorBook = new AuthorBook
                    {
                        BookId = book.Id,
                        AuthorId = author.Id
                    };
                    
                    // Adicione o objeto AuthorBook ao repositório IAuthorBookRepository
                    _authorBookRepository.AddAuthor(author);
                }
            }
            
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

            return Ok(new
            {
                StatusCode = 200,
                Message = "Livor atualizado com sucesso!"
            });
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

            return Ok(new
            {
                StatusCode = 200,
                Message = "Livro deletado com sucesso!"
            });
        }
    }
}
