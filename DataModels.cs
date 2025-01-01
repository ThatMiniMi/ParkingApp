namespace ParkingApp
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();
        public decimal AccountBalance { get; set; } = 0;
    }
    public class Car
    {
        public int CarId { get; set; }
        public string LicensePlate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ParkingPeriod CurrentParkingPeriod { get; set; }
    }
    public class ParkingPeriod
    {
        public int ParkingPeriodId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
        public decimal Cost { get; set; }
    }
}
