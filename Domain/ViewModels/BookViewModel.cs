namespace LibraryManagementAPI.Domain.ViewModels
{
    public class BookViewModel
    {
        public string Title { get; set; }
        public IList<int> AuthorsId { get; set; }
    }
}