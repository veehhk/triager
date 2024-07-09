using Triager;
using Triager.Types;

namespace Triager
{
    public interface IThreadConfig
    {
        string Name { get; }
        IEnumerable<ITaskConfig> Tasks { get; }
        TriagerState State { get; set; }
    }
}
