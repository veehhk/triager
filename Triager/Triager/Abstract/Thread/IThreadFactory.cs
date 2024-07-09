namespace Triager
{
    public interface IThreadFactory
    {
        IEnumerable<IThreadManager> Create();
    }
}
