namespace Entities.Models
{
    public class Building
    {
        public int BuildingId { get; set; }

        public string? Name { get; set; }

        public required ICollection<Room> Rooms { get; set; }
    }
}