namespace ParkingApp
{
    public interface IParkingService
    {
        void StartParking(int carId);
        void StopParking(int carId);
        ParkingPeriod GetCurrentPeriod(int carId);
        decimal GetAccountBalance(int userId);
        User GetUserDetails(int userId);
        void RegisterUser(User user);
        void RegisterCar(int userId, Car car);
    }
    public class ParkingService : IParkingService
    {
        private readonly List<User> _users = new List<User>();
        private readonly List<Car> _cars = new List<Car>();
        private readonly List<ParkingPeriod> _parkingPeriods = new List<ParkingPeriod>();
        public void StartParking(int carId)
        {
            var car = _cars.FirstOrDefault(c => c.CarId == carId);
            if (car == null || car.CurrentParkingPeriod != null)
                throw new Exception("Car not found or already parking.");
            var parkingPeriod = new ParkingPeriod
            {
                StartTime = DateTime.Now,
                CarId = carId,
                Car = car
            };
            car.CurrentParkingPeriod = parkingPeriod;
            _parkingPeriods.Add(parkingPeriod);
        }
        public void StopParking(int carId)
        {
            var car = _cars.FirstOrDefault(c => c.CarId == carId);
            if (car?.CurrentParkingPeriod == null)
                throw new Exception("Car is not currently parking.");
            var period = car.CurrentParkingPeriod;
            period.EndTime = DateTime.Now;
            decimal totalCost = CalculateCost(period.StartTime, period.EndTime.Value);
            period.Cost = totalCost;
            car.User.AccountBalance += totalCost;
            car.CurrentParkingPeriod = null;
        }
        public ParkingPeriod GetCurrentPeriod(int carId)
        {
            var car = _cars.FirstOrDefault(c => c.CarId == carId);
            return car?.CurrentParkingPeriod;
        }
        public decimal GetAccountBalance(int userId)
        {
            var user = _users.FirstOrDefault(u => u.UserId == userId);
            return user?.AccountBalance ?? 0;
        }
        public User GetUserDetails(int userId)
        {
            return _users.FirstOrDefault(u => u.UserId == userId);
        }
        public void RegisterUser(User user)
        {
            _users.Add(user);
        }
        public void RegisterCar(int userId, Car car)
        {
            var user = _users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
                throw new Exception("User not found.");
            car.UserId = userId;
            car.User = user;
            _cars.Add(car);
            user.Cars.Add(car);
        }
        private decimal CalculateCost(DateTime start, DateTime end)
        {
            decimal cost = 0;
            for (var time = start; time < end; time = time.AddHours(1))
            {
                cost += time.Hour >= 8 && time.Hour < 18 ? 14 : 6;
            }
            return cost;
        }
    }
}
