using ParkingApp;

var builder = WebApplication.CreateBuilder(args);
var parkingService = new ParkingService();
var app = builder.Build();

app.MapPost("/registeruser", async (HttpRequest request) =>
{
    var form = await request.ReadFormAsync();
    var user = new User
    {
        Name = form["Name"],
        UserId = new Random().Next(1, 1000)
    };
    try
    {
        parkingService.RegisterUser(user);
        return Results.Ok(new
        {
            Message = "User registered successfully!",
            User = user
        });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { Error = ex.Message });
    }
});
app.MapPost("/registercar/{userId}", (int userId, Car car) =>
{
    try
    {
        parkingService.RegisterCar(userId, car);
        return Results.Ok(new { Message = "Car registered successfully!", Car = car });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { Error = ex.Message });
    }
});
app.MapPost("/startparking/{carId}", (int carId) =>
{
    try
    {
        parkingService.StartParking(carId);
        return Results.Ok(new { Message = "Parking started successfully!" });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { Error = ex.Message });
    }
});
app.MapPost("/stopparking/{carId}", (int carId) =>
{
    try
    {
        parkingService.StopParking(carId);
        return Results.Ok(new { Message = "Parking stopped successfully!" });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { Error = ex.Message });
    }
});
app.MapGet("/user/{userId}", (int userId) =>
{
    var user = parkingService.GetUserDetails(userId);
    return user != null ? Results.Ok(user) : Results.NotFound(new { Error = "User not found." });
});
app.MapGet("/cost/{userId}", (int userId) =>
{
    try
    {
        var balance = parkingService.GetAccountBalance(userId);
        return Results.Ok(new { UserId = userId, TotalBalance = balance });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { Error = ex.Message });
    }
});
app.MapGet("/parkingperiods/{carId}", (int carId) =>
{
    var period = parkingService.GetCurrentPeriod(carId);
    return period != null ? Results.Ok(period) : Results.NotFound(new { Error = "No active parking period found." });
});

app.Run();