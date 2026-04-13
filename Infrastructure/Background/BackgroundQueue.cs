using System.Threading.Channels;
using Application.Interfaces;

namespace Infrastructure.Background
{
  public class BackgroundQueue : IBackgroundQueue
  {
    private readonly Channel<Func<CancellationToken, Task>> _queue;

    public BackgroundQueue()
    {
        _queue = Channel.CreateUnbounded<Func<CancellationToken, Task>>();
    }
    public async Task<Func<CancellationToken, Task>> DequeueAsync(CancellationToken cancellationToken)
    {
      return await _queue.Reader.ReadAsync(cancellationToken);
    }

    public void Enqueue(Func<CancellationToken, Task> workItem)
    {
      _queue.Writer.TryWrite(workItem);
    }
  }
}