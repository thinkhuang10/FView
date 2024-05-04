using System.Xml;

namespace ICSharpCode.TextEditor.Document;

public sealed class Span
{
    private readonly bool stopEOL;

    private readonly HighlightColor color;

    private readonly HighlightColor beginColor;

    private readonly HighlightColor endColor;

    private readonly char[] begin;

    private readonly char[] end;

    private readonly string name;

    private readonly string rule;

    private HighlightRuleSet ruleSet;

    private readonly char escapeCharacter;

    private bool ignoreCase;

    private readonly bool isBeginSingleWord;

    private readonly bool? isBeginStartOfLine;

    private readonly bool isEndSingleWord;

    internal HighlightRuleSet RuleSet
    {
        get
        {
            return ruleSet;
        }
        set
        {
            ruleSet = value;
        }
    }

    public bool IgnoreCase
    {
        get
        {
            return ignoreCase;
        }
        set
        {
            ignoreCase = value;
        }
    }

    public bool StopEOL => stopEOL;

    public bool? IsBeginStartOfLine => isBeginStartOfLine;

    public bool IsBeginSingleWord => isBeginSingleWord;

    public bool IsEndSingleWord => isEndSingleWord;

    public HighlightColor Color => color;

    public HighlightColor BeginColor
    {
        get
        {
            if (beginColor != null)
            {
                return beginColor;
            }
            return color;
        }
    }

    public HighlightColor EndColor
    {
        get
        {
            if (endColor == null)
            {
                return color;
            }
            return endColor;
        }
    }

    public char[] Begin => begin;

    public char[] End => end;

    public string Name => name;

    public string Rule => rule;

    public char EscapeCharacter => escapeCharacter;

    public Span(XmlElement span)
    {
        color = new HighlightColor(span);
        if (span.HasAttribute("rule"))
        {
            rule = span.GetAttribute("rule");
        }
        if (span.HasAttribute("escapecharacter"))
        {
            escapeCharacter = span.GetAttribute("escapecharacter")[0];
        }
        name = span.GetAttribute("name");
        if (span.HasAttribute("stopateol"))
        {
            stopEOL = bool.Parse(span.GetAttribute("stopateol"));
        }
        begin = span["Begin"].InnerText.ToCharArray();
        beginColor = new HighlightColor(span["Begin"], color);
        if (span["Begin"].HasAttribute("singleword"))
        {
            isBeginSingleWord = bool.Parse(span["Begin"].GetAttribute("singleword"));
        }
        if (span["Begin"].HasAttribute("startofline"))
        {
            isBeginStartOfLine = bool.Parse(span["Begin"].GetAttribute("startofline"));
        }
        if (span["End"] != null)
        {
            end = span["End"].InnerText.ToCharArray();
            endColor = new HighlightColor(span["End"], color);
            if (span["End"].HasAttribute("singleword"))
            {
                isEndSingleWord = bool.Parse(span["End"].GetAttribute("singleword"));
            }
        }
    }
}
