namespace TempConverter.Core;

public sealed class ConversionHistory
{
    private const int MaxEntries = 20;

    private readonly List<ConversionHistoryEntry> _entries = [];

    public IReadOnlyList<ConversionHistoryEntry> Entries => _entries;

    public void Add(ConversionHistoryEntry entry)
    {
        _entries.Insert(0, entry);
        while (_entries.Count > MaxEntries)
        {
            _entries.RemoveAt(_entries.Count - 1);
        }
    }

    public void Clear()
    {
        _entries.Clear();
    }
}
