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
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorBookRepository _authorBookRepository;
        private readonly IMapper _mapper;

        public AuthorController(IAuthorRepository authorRepository, IBookRepository bookRepository, IAuthorBookRepository authorBookRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _bookRepository = bookRepository;
            _authorBookRepository = authorBookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllAuthors()
        {
            var authors = _authorRepository.GetAllAuthors();
            var authorDTO = _mapper.Map<List<AuthorDTO>>(authors);

            return Ok(authorDTO);
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
            var author = _mapper.Map<Author>(authorViewModel);

            // Adiciona o livro ao autor
            foreach (var bookId in authorViewModel.BooksId)
            {
               var book = _bookRepository.GetBookById(bookId);
               if (book == null)
               {
                    book = new Book { Id = bookId};
                    _bookRepository.AddBook(book);

                    var authorBook = new AuthorBook
                    {
                        AuthorId = author.Id,
                        BookId = book.Id
                    };

                    _authorBookRepository.AddBook(book);
               }
            }
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

            return Ok(new
            {
                StatusCode = 200,
                Message = "Autor atualizado com sucesso!"
            });
            
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

            return Ok(new
            {
                StatusCode = 200,
                Message = "Autor deletado com sucesso!"
            });
        }
    }
}
