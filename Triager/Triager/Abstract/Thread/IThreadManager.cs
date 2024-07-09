namespace Triager
{
    public interface IThreadManager
    {
        void Execute(IEnumerable<IThreadConfig> threadConfigs, CancellationToken cancellationToken);
        void Pause(IThreadConfig threadConfig);
        void Kill(IThreadConfig threadConfig);
    }
}
