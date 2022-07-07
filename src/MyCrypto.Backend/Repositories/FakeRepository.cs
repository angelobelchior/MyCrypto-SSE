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
        });
}
