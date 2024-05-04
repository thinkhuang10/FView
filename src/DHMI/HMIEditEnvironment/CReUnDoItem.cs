using System;
using System.Collections.Generic;
using ShapeRuntime;

namespace HMIEditEnvironment;

[Serializable]
internal class CReUnDoItem
{
    public CShape NewShape;

    public CShape OldShape;

    public List<CShape> NewShapeList = new();

    public List<CShape> OldShapeList = new();

    public List<CShape> CShapeList = new();

    public CReUnDoItem()
    {
    }

    public CReUnDoItem(List<CShape> newshapelist, List<CShape> oldshapelist)
    {
        if (newshapelist != null)
        {
            CShape[] array = newshapelist.ToArray();
            foreach (CShape cShape in array)
            {
                NewShape = cShape.Copy();
                NewShapeList.Add(NewShape);
            }
        }
        else
        {
            NewShapeList = null;
        }
        if (oldshapelist != null)
        {
            CShape[] array2 = oldshapelist.ToArray();
            foreach (CShape cShape2 in array2)
            {
                OldShape = cShape2.Copy();
                OldShapeList.Add(OldShape);
            }
        }
        else
        {
            OldShapeList = null;
        }
    }
}
