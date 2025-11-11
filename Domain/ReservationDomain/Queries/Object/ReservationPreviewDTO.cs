namespace Domain.ReservationDomain.Queries.Object
{
    public class ReservationPreviewDTO
    {
        public int Id { get; set; }
        public DateTime PickupDateTime { get; set; }
        public string? Comment { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}