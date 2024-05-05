namespace HMIEditEnvironment;

public class FindOptions
{
    public string Content { get; set; }

    public string PageName { get; set; }

    public bool MatchCase { get; set; }

    public bool WholeWord { get; set; }

    public override bool Equals(object obj)
    {
        if (obj is FindOptions findOptions)
        {
            if (Content.Equals(findOptions.Content) && PageName.Equals(findOptions.PageName) && MatchCase == findOptions.MatchCase && WholeWord == findOptions.WholeWord)
            {
                return true;
            }
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return $"FindOptions:[Content={Content},PageName={PageName},MatchCase={MatchCase},WholeWord={WholeWord}]";
    }
}
