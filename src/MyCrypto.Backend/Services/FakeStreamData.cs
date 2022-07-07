namespace MyCrypto.Backend.Services;

public interface IStreamData
{
    Task<IEnumerable<Data>> Consume(string filter);
    Task<IEnumerable<Data>> Snapshot();
}

/// <summary>
/// Essa classe simula, de maneira bem irresponsável e simplória, um consumo do Kafka, 
/// com valores de variações das cryptos
/// </summary>
public class FakeStreamData : IStreamData
{
    private bool _cryptosLoaded = false;
    private IEnumerable<Crypto> _allCryptos;
    private int _totalCryptos;

    private readonly IRepository _repository;
    public FakeStreamData(IRepository repository)
    {
        _repository = repository;
        _allCryptos = Enumerable.Empty<Crypto>();
        _totalCryptos = 0;
    }

    public async Task<IEnumerable<Data>> Consume(string filter)
    {
        //Simulando um tempo de requisição/processamento 
        var delay = Random.Shared.Next(10, 101);
        await Task.Delay(delay);

        //Nesse caso, simula-se que apenas algumas criptos sofreram alterações de valores
        var cryptos = await GetRndCryptos();
        var data = new List<Data>();
        foreach (var crypto in cryptos)
            data.Add(CreateRndData(crypto));

        //***
        //Simula a opção de escolher algumas cryptos apenas para receber os dados de atualização
        if (filter.Equals("*")) return data;
        var symbols = filter.Split(",").Select(s => s.Trim());
        return data.Where(i => symbols.Any(symbol => symbol.Equals(i.Symbol, StringComparison.InvariantCultureIgnoreCase)));
        //***
    }

    public async Task<IEnumerable<Data>> Snapshot()
    {
        await LoadCryptos();
        var data = new List<Data>();
        foreach (var crypto in _allCryptos)
            data.Add(CreateRndData(crypto));

        return data;
    }

    private static Data CreateRndData(Crypto crypto)
        => new(crypto.Symbol, GetRndPrices(), GetRndPrices(true), GetRndPrices(), GetRndPrices(), DateTime.Now);

    /// <summary>
    /// Simula a criação aleatória de preços.
    /// </summary>
    /// <param name="useRandomSign"></param>
    /// <returns></returns>
    private static decimal GetRndPrices(bool useRandomSign = false)
    {
        var x = Random.Shared.NextDouble();
        var y = Random.Shared.NextDouble();

        var sign = 1;
        if (useRandomSign) sign = Random.Shared.Next(0, 2);
        if (sign == 0) sign = -1;

        var divider = 100 * sign;
        var value = (x / y) * divider;
        return Math.Round((decimal)value, 2);
    }

    //***
    //Obtém a lista de cryptos e deixa na memória. 
    //Isso serve para não ficar efetuando consultas desnecessárias todo tempo
    //Essa abordagem é apenas para efeito de exemplo
    private async Task<IEnumerable<Crypto>> GetRndCryptos()
    {
        await LoadCryptos();
        var count = Random.Shared.Next(1, _totalCryptos + 1);
        return _allCryptos.OrderBy(x => Guid.NewGuid()).Take(count);
    }

    private async Task LoadCryptos()
    {
        if (_cryptosLoaded) return;

        _allCryptos = await _repository.ListAll();
        _totalCryptos = _allCryptos.Count();
        _cryptosLoaded = true;
    }
}
