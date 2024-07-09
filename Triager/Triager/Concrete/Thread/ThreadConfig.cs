using Triager.Types;

namespace Triager
{
    public class ThreadConfig(string name, IEnumerable<ITaskConfig> tasksConfigs) : IThreadConfig
    {
        public string Name { get; } = name;
        public TriagerState State { get; set; } = TriagerState.Created;
        public IEnumerable<ITaskConfig> Tasks { get; private set; } = tasksConfigs;
    }
}
