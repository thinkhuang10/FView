using System.Collections.Generic;

namespace ShapeRuntime;

public interface IEventBind
{
    bool EnableEventBind { get; set; }

    Dictionary<string, List<EventSetItem>> EventBindDict { get; set; }
}
