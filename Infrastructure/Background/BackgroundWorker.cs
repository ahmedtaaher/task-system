using Application.Interfaces;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Background
{
  public class BackgroundWorker : BackgroundService
  {
    private readonly IBackgroundQueue _queue;

    public BackgroundWorker(IBackgroundQueue queue)
    {
      _queue = queue;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        var workItem = await _queue.DequeueAsync(stoppingToken);

        try
        {
          await workItem(stoppingToken);
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Background error: {ex.Message}");
        }
      }
    }
  }
}