using System.ComponentModel.DataAnnotations;

namespace SimpleBookstoreAPI.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearofPublication { get; set; }
        public string AuthorName { get; set; }
    }
}
