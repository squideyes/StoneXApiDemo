using Microsoft.Extensions.Configuration;
using StoneXApiDemo;
using System.Net;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, false)
    .AddUserSecrets<Program>()
    .Build();

var appKey = config["AppKey"];
var userName = config["UserName"];
var password  = config["Password"];

try
{
    var client = new StoneXClient(appKey, userName, password);

    await client.LogOnAsync();

    Console.WriteLine($"UserName: {userName}");
    Console.WriteLine($"ClientAccountId: {client.ClientAccountId}");

    Console.WriteLine();
    Console.WriteLine("Logon JSON:");
    Console.WriteLine(client.SessionJson);

    var marketIds = await client.GetMarketIdsAsync();

    Console.WriteLine();
    Console.WriteLine("MarketId JSON:");
    Console.WriteLine(marketIds);
}
catch (HttpRequestException ex) 
    when (ex.StatusCode == HttpStatusCode.Unauthorized)
{
    Console.WriteLine("Unauthorized: check your credentials in \"appsettings.json\"");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
