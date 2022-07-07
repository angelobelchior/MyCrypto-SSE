using Microsoft.AspNetCore.Mvc;

namespace MyCrypto.Backend.Controllers;

[ApiController]
[Route("cryptos")]
public class CryptosController : ControllerBase
{
    private readonly IRepository _repository;
    private readonly IStreamData _streamData;
    private readonly ILogger<CryptosController> _logger;
    public CryptosController(IRepository repository, IStreamData streamData, ILogger<CryptosController> logger)
    {
        _repository = repository;
        _streamData = streamData;
        _logger = logger;
    }

    [HttpGet]
    public Task<IEnumerable<Crypto>> Get()
        => _repository.ListAll();

    [HttpGet("snapshot")]
    public Task<IEnumerable<Data>> Snapshot()
        => _streamData.Snapshot();

    [HttpGet("stream")]
    public async Task Stream(string filter)
    {
        Response.Headers.Add("Content-Type", "text/event-stream");
        Response.Headers.Add("Cache-Control", "no-cache");
        Response.Headers.Add("Connection", "keep-alive");
        var writer = new StreamWriter(Response.Body);
        while (!HttpContext.RequestAborted.IsCancellationRequested)
        {
            var items = await _streamData.Consume(filter);           
            var json = JsonSerializer.Serialize(items);
            _logger.LogInformation(json);

            await writer.WriteLineAsync("event: crypto");
            await writer.WriteLineAsync($"data: {json}");
            await writer.FlushAsync();
        }
        writer.Close();
        writer.Dispose();
    }
}