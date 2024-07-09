namespace Triager
{
    public interface ITaskManager
    {
        void Execute(IEnumerable<ITaskConfig> taskConfigs, CancellationToken cancellationToken);
        void Pause(ITaskConfig taskConfig);
        void Kill(ITaskConfig taskConfig);
    }
}
