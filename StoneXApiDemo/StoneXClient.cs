using System.Text;
using System.Text.Json;

namespace StoneXApiDemo;

internal class StoneXClient(
    string appKey, string userName, string password)
{
    private readonly string appKey = appKey;
    private readonly string userName = userName;
    private readonly string password = password;

    private readonly HttpClient client = GetHttpClient();

    private string session = null!;

    public int ClientAccountId { get; private set; }
    public string? SessionJson { get; private set; }

    public async Task LogOnAsync()
    {
        session = await GetSessionAsync();

        ClientAccountId = await GetClientAccountIdAsync();        
    }

    private async Task<string> GetSessionAsync()
    {
        var body = new
        {
            AppKey = appKey,
            UserName = userName,
            Password = password,
            AppVersion = "1.0.0",
            AppComments = "StoneXApiDemo",
        };

        var localPath = new PathBuilder()
            .AddSegment("v2")
            .AddSegment("Session")
            .Build();

        var response = await client.PostAsync(localPath,
            new StringContent(JsonSerializer.Serialize(body),
                Encoding.UTF8, "application/json"));

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        SessionJson = json;

        var root = JsonDocument.Parse(json).RootElement;

        return root.GetProperty("session").GetString()!;
    }

    async Task<int> GetClientAccountIdAsync()
    {
        var localPath = new PathBuilder()
            .AddSegment("v2")
            .AddSegment("userAccount")
            .AddSegment("ClientAndTradingAccount")
            .AddKeyValue("UserName", userName)
            .AddKeyValue("Session", session)
            .Build();

        var response = await client.GetAsync(localPath);

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        var root = JsonDocument.Parse(json).RootElement;

        var accounts = root.GetProperty("tradingAccounts");

        var account = accounts[0];

        return account.GetProperty("clientAccountId").GetInt32()!;
    }

    public async Task<string> GetMarketIdsAsync()
    {
        var localPath = new PathBuilder()
            .AddSegment("v2")
            .AddSegment("market")
            .AddSegment("fullSearchWithTags")
            .AddKeyValue("UserName", userName)
            .AddKeyValue("Session", session)
            .AddKeyValue("ClientAccountId", ClientAccountId)
            .Build();

        var response = await client.GetAsync(localPath);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }

    private static HttpClient GetHttpClient()
    {
        var baseUri = new Uri("https://ciapi.cityindex.com");

        var client = new HttpClient();

        client.DefaultRequestHeaders.Add("Accept", "application/json");

        client.BaseAddress = baseUri;

        return client;
    }
}
