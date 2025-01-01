using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ParkingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingController : ControllerBase
    {
        private readonly IParkingService _parkingService;
        public ParkingController(IParkingService parkingService)
        {
            _parkingService = parkingService;
        }
        [HttpPost("registerUser")]
        public IActionResult RegisterUser([FromBody] User user)
        {
            _parkingService.RegisterUser(user);
            return Ok();
        }
        [HttpPost("registerCar/{userId}")]
        public IActionResult RegisterCar(int userId, [FromBody] Car car)
        {
            _parkingService.RegisterCar(userId, car);
            return Ok();
        }
        [HttpPost("startParking/{carId}")]
        public IActionResult StartParking(int carId)
        {
            _parkingService.StartParking(carId);
            return Ok();
        }
        [HttpPost("stopParking/{carId}")]
        public IActionResult StopParking(int carId)
        {
            _parkingService.StopParking(carId);
            return Ok();
        }
        [HttpGet("currentPeriod/{carId}")]
        public IActionResult GetCurrentPeriod(int carId)
        {
            var period = _parkingService.GetCurrentPeriod(carId);
            return period != null ? Ok(period) : NotFound();
        }
        [HttpGet("accountBalance/{userId}")]
        public IActionResult GetAccountBalance(int userId)
        {
            var balance = _parkingService.GetAccountBalance(userId);
            return Ok(balance);
        }
        [HttpGet("userDetails/{userId}")]
        public IActionResult GetUserDetails(int userId)
        {
            var user = _parkingService.GetUserDetails(userId);
            return user != null ? Ok(user) : NotFound();
        }
    }
}
