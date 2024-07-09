using Triager.Types;

namespace Triager
{
    public interface ITaskConfig
    {
        string Name { get; }
        Delegate Work { get; }
        TriagerPriority Priority { get; set; }
        TriagerState State { get; set; }
    }
}
