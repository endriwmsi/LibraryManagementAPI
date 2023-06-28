namespace LibraryManagementAPI.Domain.ViewModels
{
    public class AuthorViewModel
    {
        public string Name { get; set; }
        public IList<int> BooksId { get; set; }
    }
}