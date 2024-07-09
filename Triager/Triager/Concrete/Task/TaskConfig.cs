using Triager.Types;

namespace Triager
{
    public class TaskConfig(string name, Delegate work, TriagerPriority priority = TriagerPriority.Low) : ITaskConfig
    {
        public string Name { get; } = name;
        public Delegate Work { get; private set; } = work;
        public TriagerPriority Priority { get; set; } = priority;
        public TriagerState State { get; set; } = TriagerState.Created;
    }
}
