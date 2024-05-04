namespace ShapeRuntime;

public interface ISupportHtml5
{
    event requestEventBindDictDele requestEventBindDict;

    event requestPropertyBindDataDele requestPropertyBindData;

    string makeHTML();

    string makeScript();

    string makeStyle();

    string makeCycleScript();
}
