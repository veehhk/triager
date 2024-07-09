using Triager.Types;

namespace Triager
{
    public class ThreadManager(ITaskManager taskManager) : IThreadManager
    {
        private readonly ITaskManager _taskManager = taskManager;

        public void Execute(IEnumerable<IThreadConfig> threadConfigs, CancellationToken cancellationToken)
        {
            foreach (var threadConfig in threadConfigs)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    threadConfig.State = TriagerState.Aborted;
                    continue;
                }

                threadConfig.State = TriagerState.Running;
                Thread thread = new(() => _taskManager.Execute(threadConfig.Tasks, cancellationToken));
                thread.Start();
            }
        }

        public void Pause(IThreadConfig threadConfig)
        {
            threadConfig.State = TriagerState.Paused;
            foreach (var task in threadConfig.Tasks)
            {
                _taskManager.Pause(task);
            }
        }

        public void Kill(IThreadConfig threadConfig)
        {
            threadConfig.State = TriagerState.Aborted;
            foreach (var task in threadConfig.Tasks)
            {
                _taskManager.Kill(task);
            }
        }
    }
}