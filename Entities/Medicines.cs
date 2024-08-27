using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace awebapi.Entities
{
    public class Medicines
    {
        [Key]
        public Guid Med_id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }    
        public required string Description { get; set; }
        public string? Generic_Name { get; set; }
        public DateTime Created_at { get; set; } = DateTime.UtcNow;
         public DateTime Last_edit { get; set; } = DateTime.UtcNow;
         public DateOnly Expiry_date {get; set;}
        public int Stock { get; set; }
        public string? Type { get; set; }
        public bool Is_active { get; set; } = true;
        [ForeignKey("Category_id")]
        public Guid Category_id { get; set; }
        public string? Notes { get; set; }
        public string? Cost_Price { get; set; }
        public string? Selling_Price { get; set; }
        public string? Currency { get; set; }
        public string? Batch_Number { get; set; }
        public string? Image { get; set; }
    }
}
