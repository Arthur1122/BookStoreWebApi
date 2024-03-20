namespace BookStoreApi.DTOs
{
    public class BookDto
    {
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public string Category { get; set; } = null!;

        public string AuthorId { get; set; } = null!;
    }
}
