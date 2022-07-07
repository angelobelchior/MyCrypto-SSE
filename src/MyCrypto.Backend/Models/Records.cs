namespace MyCrypto.Backend.Models;

public record Crypto(string Name, string Symbol);
public record Data(string Symbol, decimal Price, decimal PriceChange, decimal MarketCap, decimal Volume, DateTime DateTime);