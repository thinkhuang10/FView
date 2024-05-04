namespace HMIEditEnvironment;

public class FindResult
{
    public string PageName { get; set; }

    public string PageDisplayName { get; set; }

    public string ShapeName { get; set; }

    public override string ToString()
    {
        return $"FindResult:[PageName={PageName},PageDisplayName={PageDisplayName},ShapeName={ShapeName}]";
    }
}
