namespace Entities.Models
{
    public class Club
    {
        public int ClubId { get; set; }

        public string? Name { get; set; }


        public int StudentId { get; set; }

        public required Student Student { get; set; }

        public required ICollection<ClubReservation> ClubReservations { get; set; }


    }
}