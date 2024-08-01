using System.ComponentModel.DataAnnotations;

namespace awebapi.DTOs
{
    public class MedicineDto
    {

 public Guid Med_id { get; set; } = Guid.NewGuid();       
  public required string Name { get; set; }
        public required string Description { get; set; }

        public DateTime Created_at { get; set; } = DateTime.UtcNow;
        public DateTime Last_edit { get; set; } = DateTime.UtcNow;
        public int Stock { get; set; }
        public bool Is_active { get; set; } = true;
    }
}