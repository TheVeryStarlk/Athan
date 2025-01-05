using Athan.Services;

Console.WriteLine("Do you want to manually specify your location? (Y/N)");

var input = Console.ReadLine()?.ToLower();

using var client = new HttpClient();

Location? location;

if (input is "y")
{
    Console.WriteLine("Please enter a valid location. As: country, city.");

    var parts = (Console.ReadLine() ?? string.Empty).Split(',');

    if (parts.Length is 0)
    {
        Console.WriteLine("Invalid input.");
        Environment.Exit(1);

        return;
    }

    location = new Location(parts[0], parts[1]);
}
else
{
    Console.WriteLine("Getting location...");

    var locationService = new LocationService(client);
    var result = await locationService.GetAsync();

    if (!result.IsSuccess(out location))
    {
        Console.WriteLine("Failed to get location.");
        Environment.Exit(1);

        return;
    }
}

Console.WriteLine("Getting prayer times...");

var prayersService = new PrayerService(client);

var prayers = await prayersService.GetAsync(location.Country, location.City);

Console.Clear();

if (!prayers.IsSuccess(out var timings))
{
    Console.WriteLine("Failed to get prayers.");
    Environment.Exit(1);

    return;
}

var now = DateTime.Now;

Console.WriteLine($"Current time is {now:t}");

foreach (var pair in timings)
{
    var offset = now.Add(pair.Value);

    Console.WriteLine($"{pair.Key} @ {offset:t}. After {offset.Subtract(now).Hours} hours.");
}