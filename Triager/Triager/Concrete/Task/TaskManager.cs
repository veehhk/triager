using Triager.Types;

namespace Triager
{
    public class TaskManager : ITaskManager
    {
        public void Execute(IEnumerable<ITaskConfig> taskConfigs, CancellationToken cancellationToken)
        {
            foreach (var taskConfig in taskConfigs)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    taskConfig.State = TriagerState.Aborted;
                    continue;
                }

                taskConfig.State = TriagerState.Running;
                Task.Run(() => Execute(taskConfig, cancellationToken), cancellationToken);
            }
        }

        private async void Execute(ITaskConfig taskConfig, CancellationToken cancellationToken)
        {
            try
            {
                if (taskConfig.Work is Action action)
                {
                    await Task.Run(() =>
                    {
                        action();
                    }, cancellationToken);
                }
                else if (taskConfig.Work is Delegate function)
                {
                    await Task.Run(() =>
                    {
                        function.DynamicInvoke();
                    }, cancellationToken);
                }
                taskConfig.State = TriagerState.Succeeded;
            }
            catch (Exception)
            {
                taskConfig.State = TriagerState.Failed;
            }
            finally
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    taskConfig.State = TriagerState.Aborted;
                }
                else if (taskConfig.State != TriagerState.Succeeded && taskConfig.State != TriagerState.Failed)
                {
                    taskConfig.State = TriagerState.Completed;
                }
            }
        }

        public void Pause(ITaskConfig taskConfig)
        {
            taskConfig.State = TriagerState.Paused;
        }

        public void Kill(ITaskConfig taskConfig)
        {
            taskConfig.State = TriagerState.Aborted;
        }
    }
}