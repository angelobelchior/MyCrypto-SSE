namespace MyCrypto.Backend.Repositories;

public interface IRepository
{
    Task<IEnumerable<Crypto>> ListAll();
}

internal class FakeRepository : IRepository
{
    public Task<IEnumerable<Crypto>> ListAll()
        => Task.FromResult<IEnumerable<Crypto>>(new List<Crypto>
        {
            new Crypto("Bitcoin", "BTC"),
            new Crypto("Ethereum", "ETH"),
            new Crypto("BNB", "BNB"),
            new Crypto("Tether", "USDT"),
            new Crypto("Solana", "SOL"),
            new Crypto("XRP", "XRP"),
            new Crypto("Cardano", "ADA"),
            new Crypto("Avalanche", "AVAX"),
            new Crypto("USD Coin",    "USDC"),
            new Crypto("Chainlink", "LINK"),
            new Crypto("Algorand", "ALGO"),
            new Crypto("Polygon", "MATIC"),
            new Crypto("VeChain", "VET"),
            new Crypto("Tron", "TRX"),
            new Crypto("ZCash", "ZEC"),
            new Crypto("EOS", "EOS"),
            new Crypto("Tezos", "XTZ"),
            new Crypto("Neo", "NEO"),
            new Crypto("Dash", "DASH"),
            new Crypto("Stacks", "STX"),
            new Crypto("NEM", "NEM"),
            new Crypto("Decred", "DCR"),
            new Crypto("Storj", "STORJ"),
            new Crypto("0x", "ZRX"),
            new Crypto("DigiByte", "DGB"),
            new Crypto("Polkadot", "DOT"),
            new Crypto("Bitcoin Cash", "BCH"),
            new Crypto("Litecoin", "LTC"),
            new Crypto("Dogecoin", "DOGE"),
            new Crypto("Binance Coin", "BNB"),
            new Crypto("Polygon", "MATIC"),
        });
}