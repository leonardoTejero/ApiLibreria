using Common.Utils.Resources;
using Infraestructure.Core.UnitOfWork.Interface;
using Infraestructure.Entity.Model.Library;
using MyLibrary.Domain.Dto;
using MyLibrary.Domain.Dto.Book;
using MyLibrary.Domain.Services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.Domain.Services
{
    public class BookServices : IBookServices
    {
        #region Attributes
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Builder
        public BookServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region Methods

        public List<ConsultBookDto> GetAllBooks()
        {
            //var book = _unitOfWork.BookRepository.GetAll(p => p.EditorialEntity);
            var book = _unitOfWork.AuthorBookRepository.GetAll(p => p.BookEntity,
                p => p.BookEntity.EditorialEntity, p => p.AuthorEntity);


            List<ConsultBookDto> listBooks = book.Select(x => new ConsultBookDto
            {
                Id = x.BookEntity.IdBook,
                Name = x.BookEntity.Name,
                Synopsis = x.BookEntity.Synopsis,
                NumberPages = x.BookEntity.NumberPages,
                IdEditorial = x.BookEntity.IdEditorial,
                Editorial = x.BookEntity.EditorialEntity.Name,
                IdAuthor = x.AuthorEntity.IdAuthor,
                IdAuthorBook = x.Id,
                Author = x.AuthorEntity.Name +" "+ x.AuthorEntity.LastName,

            }).ToList();

            return listBooks;
        }

        public ResponseDto GetOneBook(int idBook)
        {
            ResponseDto response = new ResponseDto();

            var book = _unitOfWork.AuthorBookRepository.FirstOrDefault(x => x.IdBook == idBook, p => p.BookEntity,
                                                                       p => p.BookEntity.EditorialEntity, p => p.AuthorEntity);

            if (book == null)
            {
                response.Message = "Libro no encontrado";
                response.IsSuccess = false;
                return response;
            }

            ConsultBookDto bookDto = new ConsultBookDto
            {
                Id = book.BookEntity.IdBook,
                Name = book.BookEntity.Name,
                Synopsis = book.BookEntity.Synopsis,
                NumberPages = book.BookEntity.NumberPages,
                IdEditorial = book.BookEntity.IdEditorial,
                Editorial = book.BookEntity.EditorialEntity.Name,
                IdAuthor = book.AuthorEntity.IdAuthor,
                IdAuthorBook = book.Id,
                Author = book.AuthorEntity.Name,
            };
            response.Result = bookDto;
            response.IsSuccess = true;

            return response;
        }

        public ResponseDto GetBooksByName(string name)
        {

            var response = new ResponseDto();


            IEnumerable<AuthorBookEntity> books = _unitOfWork.AuthorBookRepository.FindAll(x => x.BookEntity.Name.Contains(name),
                                                                  p => p.BookEntity.EditorialEntity, p => p.AuthorEntity);

            if (books != null && books.Any())
            {
                List<ConsultBookDto> bookList = new List<ConsultBookDto>();

                foreach (var book in books)
                {
                    bookList.Add(new ConsultBookDto
                    {
                        Id = book.BookEntity.IdBook,
                        Name = book.BookEntity.Name,
                        Synopsis = book.BookEntity.Synopsis,
                        NumberPages = book.BookEntity.NumberPages,
                        IdEditorial = book.BookEntity.IdEditorial,
                        Editorial = book.BookEntity.EditorialEntity.Name,
                        IdAuthor = book.AuthorEntity.IdAuthor,
                        IdAuthorBook = book.Id,
                        Author = book.AuthorEntity.Name,
                    });
                }

                response.IsSuccess = true;
                response.Result = bookList;

                return response;
            }

            response.Message = "Libro no encontrado"; // GeneralMessages.ItemNotFound;
            response.IsSuccess = false;
            return response;
        }


        //Insertar un libro y la tabla intermedia AutoresLibros
        public async Task<bool> InsertBookAsync(InsertBookDto book)
        {
            AuthorBookEntity authorBookEntity = new AuthorBookEntity()
            {
                IdAuthor = book.IdAuthor,
                BookEntity = new BookEntity()
                {
                    Name = book.Name,
                    Synopsis = book.Synopsis,
                    NumberPages = book.NumberPages,
                    IdEditorial = book.IdEditorial,
                }
            };

            _unitOfWork.AuthorBookRepository.Insert(authorBookEntity);

            return await _unitOfWork.Save() > 0;
        }

        public async Task<ResponseDto> DeleteBookAsync(int idBook)
        {
            ResponseDto response = new ResponseDto();
            // TODO Validar que exista primero
            _unitOfWork.BookRepository.Delete(idBook);

            response.IsSuccess = await _unitOfWork.Save() > 0;

            if (response.IsSuccess)
                response.Message = GeneralMessages.ItemDeleted; //"Se elminó correctamente el libro";
            else
                response.Message = GeneralMessages.ItemNoDeleted; //"Hubo un error al eliminar el libro, por favor vuelva a intentalo";

            return response;
        }

        public async Task<bool> UpdateBookAsync(ConsultBookDto book)
        {
            bool result = false;

            AuthorBookEntity books = _unitOfWork.AuthorBookRepository.FirstOrDefault(x => x.Id == book.IdAuthorBook,
                                                                                     x => x.BookEntity);

            if (books != null)
            {
                books.BookEntity.Name = book.Name;
                books.BookEntity.Synopsis = book.Synopsis;
                books.BookEntity.NumberPages = book.NumberPages;
                books.BookEntity.IdEditorial = book.IdEditorial;
                books.IdAuthor = book.IdAuthor;

                _unitOfWork.AuthorBookRepository.Update(books);
                result = await _unitOfWork.Save() > 0;
            }

            return result;
        }

        #endregion
    }
}
