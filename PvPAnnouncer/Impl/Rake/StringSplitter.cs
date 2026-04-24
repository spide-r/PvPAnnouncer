using System;

namespace PvPAnnouncer.Impl.Rake;

// Attributed to https://github.com/benmcevoy/Rake under MIT
internal ref struct StringSplitter
{
    private readonly ReadOnlySpan<char> text;
    private readonly char seperator;

    private int position;

    public StringSplitter(ReadOnlySpan<char> text, char seperator)
    {
        this.text = text;
        this.seperator = seperator;
        position = 0;
    }

    public bool TryGetNext(out ReadOnlySpan<char> result)
    {
        if (position >= text.Length)
        {
            result = default;

            return false;
        }

        var start = position;

        var commaIndex = text.Slice(position).IndexOf(seperator);

        if (commaIndex > -1)
        {
            position += commaIndex + 1;

            result = text.Slice(start, commaIndex);
        }
        else
        {
            position = text.Length;

            result = text.Slice(start);
        }

        return true;
    }
}