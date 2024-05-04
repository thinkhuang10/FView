using System;
using System.Collections.Generic;

namespace ShapeRuntime;

[Serializable]
public class UserType
{
    public int id;

    public string userTypeName = "";

    public List<UserType> SubUserTypes = new();

    public List<int> Regions = new();

    public int Id
    {
        get
        {
            return id;
        }
        set
        {
            id = value;
        }
    }

    public string UserTypeName
    {
        get
        {
            return userTypeName;
        }
        set
        {
            userTypeName = value;
        }
    }

    public override string ToString()
    {
        return UserTypeName;
    }
}
