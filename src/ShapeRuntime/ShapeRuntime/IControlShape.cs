using System;

namespace ShapeRuntime;

public interface IControlShape
{
    string ID { get; set; }

    event EventHandler IDChanged;
}
