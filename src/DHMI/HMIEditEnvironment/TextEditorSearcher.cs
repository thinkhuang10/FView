using System;
using ICSharpCode.TextEditor.Document;

namespace HMIEditEnvironment;

public class TextEditorSearcher : IDisposable
{
    public delegate TResult Func<T1, T2, TResult>(T1 arg1, T2 arg2);

    public delegate TResult Func<T, TResult>(T arg);

    private IDocument document;

    private TextMarker region;

    public bool MatchCase;

    public bool MatchWholeWord;

    private string lookWord;

    private string lookUpperCaseWord;

    public IDocument Document
    {
        get
        {
            return document;
        }
        set
        {
            if (document != value)
            {
                ClearScanRegion();
                document = value;
            }
        }
    }

    public bool HasScanRegion => region != null;

    public int BeginOffset
    {
        get
        {
            if (region != null)
            {
                return region.Offset;
            }
            return 0;
        }
    }

    public int EndOffset
    {
        get
        {
            if (region != null)
            {
                return region.EndOffset;
            }
            return document.TextLength;
        }
    }

    public string LookWord
    {
        get
        {
            return lookWord;
        }
        set
        {
            lookWord = value;
        }
    }

    public void ClearScanRegion()
    {
        if (region != null)
        {
            document.MarkerStrategy.RemoveMarker(region);
            region = null;
        }
    }

    public void Dispose()
    {
        ClearScanRegion();
        GC.SuppressFinalize(this);
    }

    ~TextEditorSearcher()
    {
        Dispose();
    }

    public int InRange(int beginOffset, int startAt, int endAt)
    {
        if (startAt <= beginOffset)
        {
            if (beginOffset <= endAt)
            {
                return beginOffset;
            }
            return endAt;
        }
        return startAt;
    }

    public TextRange FindNext(int startFrom, bool searchBackward, out bool loopedAround)
    {
        loopedAround = false;
        int beginOffset = BeginOffset;
        int endOffset = EndOffset;
        int num = InRange(startFrom, beginOffset, endOffset);
        lookUpperCaseWord = (MatchCase ? lookWord : lookWord.ToUpperInvariant());
        TextRange textRange;
        if (searchBackward)
        {
            textRange = FindNextIn(beginOffset, num, searchBackward: true);
            if (textRange == null)
            {
                loopedAround = true;
                textRange = FindNextIn(beginOffset, endOffset, searchBackward: true);
            }
        }
        else
        {
            textRange = FindNextIn(num, endOffset, searchBackward: false);
            if (textRange == null)
            {
                loopedAround = true;
                textRange = FindNextIn(beginOffset, endOffset, searchBackward: false);
            }
        }
        return textRange;
    }

    private TextRange FindNextIn(int offset1, int offset2, bool searchBackward)
    {
        Func<char, char, bool> func = ((!MatchCase) ? ((Func<char, char, bool>)((char LookWord, char c) => LookWord == char.ToUpperInvariant(c))) : ((Func<char, char, bool>)((char LookWord, char c) => LookWord == c)));
        Func<int, bool> func2 = ((!MatchWholeWord) ? new Func<int, bool>(IsPartWordMatch) : new Func<int, bool>(IsWholeWordMatch));
        char arg = lookUpperCaseWord[0];
        if (searchBackward)
        {
            offset2 -= lookWord.Length;
            for (int num = offset2; num >= offset1; num--)
            {
                if (func(arg, document.GetCharAt(num)) && func2(num))
                {
                    return new TextRange(document, num, lookWord.Length);
                }
            }
        }
        else
        {
            offset2 -= lookWord.Length;
            for (int i = offset1; i <= offset2; i++)
            {
                if (func(arg, document.GetCharAt(i)) && func2(i))
                {
                    return new TextRange(document, i, lookWord.Length);
                }
            }
        }
        return null;
    }

    private bool IsWholeWordMatch(int offset)
    {
        if (IsWordBoundary(offset) && IsWordBoundary(offset + lookWord.Length))
        {
            return IsPartWordMatch(offset);
        }
        return false;
    }

    private bool IsWordBoundary(int offset)
    {
        if (offset > 0 && offset < document.TextLength && IsAlphaNumeric(offset - 1))
        {
            return !IsAlphaNumeric(offset);
        }
        return true;
    }

    private bool IsAlphaNumeric(int offset)
    {
        char charAt = document.GetCharAt(offset);
        if (!char.IsLetterOrDigit(charAt))
        {
            return charAt == '_';
        }
        return true;
    }

    private bool IsPartWordMatch(int offset)
    {
        string text = document.GetText(offset, lookWord.Length);
        if (!MatchCase)
        {
            text = text.ToUpperInvariant();
        }
        return text == lookUpperCaseWord;
    }
}
