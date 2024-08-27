using System.ComponentModel.DataAnnotations;

namespace awebapi.Entities
{
    public class Categories
    {
        [Key]
        public Guid Category_id { get; set; } = Guid.NewGuid();
        public required string CategoryName { get; set; }
        public bool Is_active { get; set; }
    }
}