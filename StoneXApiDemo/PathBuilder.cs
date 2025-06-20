using System.Text;

namespace StoneXApiDemo;

internal class PathBuilder
{
    private readonly List<string> segments = ["TradingAPI"];

    private readonly Dictionary<string, object> keyValues = [];

    public PathBuilder AddSegment(string segment)
    {
        segments.Add(segment);

        return this;
    }

    public PathBuilder AddKeyValue(string key, object value)
    {
        keyValues.Add(key, value);

        return this;
    }

    public string Build()
    {
        var sb = new StringBuilder();

        sb.Append('/');
        sb.Append(string.Join('/', segments));

        var count = 0;

        foreach(var key in keyValues.Keys) 
        {
            sb.Append(count++ == 0 ? '?' : '&');
            sb.Append(key);
            sb.Append('=');
            sb.Append(keyValues[key]);
        }

        return sb.ToString();
    }
}
