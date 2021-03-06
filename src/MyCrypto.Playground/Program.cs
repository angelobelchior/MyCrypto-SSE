global using System.Text.Json;

await Task.Delay(1000); //Aguarda o Server subir para começar o consumo

using var client = new HttpClient();
client.Timeout = TimeSpan.FromSeconds(15);
var url = "https://localhost:7115/cryptos/stream/?filter=*";

while (true)
{
    try
    {
        Console.WriteLine("Connecting...");
        var stream = await client.GetStreamAsync(url);
        using var reader = new StreamReader(stream);
        while (!reader.EndOfStream)
        {
            var message = await reader.ReadLineAsync();
            if (string.IsNullOrEmpty(message)) continue;

            if (!message.StartsWith("data:"))
            {
                Console.WriteLine($"Event: {message}");
                continue;
            }

            var items = JsonSerializer.Deserialize<IEnumerable<Data>>(message.Replace("data:", string.Empty));
            if (items is null) continue;

            PrintItems(items);
        }
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine("[Error] - Waiting...");
        await Task.Delay(TimeSpan.FromSeconds(5));
    }
}

void PrintItems(IEnumerable<Data> items)
{
    foreach (var item in items)
        Console.WriteLine(item);
}

public record Data(string Symbol, decimal Price, decimal PriceChange, decimal MarketCap, decimal Volume, DateTime DateTime);