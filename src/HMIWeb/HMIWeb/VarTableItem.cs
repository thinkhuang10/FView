using System.Collections.Generic;

namespace HMIWeb;

public class VarTableItem
{
    private string name;

    private int id;

    private int id2;

    private int type;

    private string emluator;

    private string max;

    private string min;

    private string cycle;

    private string delay;

    private bool indatabase;

    private bool invalidate;

    private bool isalive = true;

    private List<int> rBev;

    private List<int> wBev;

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

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

    public int Id2
    {
        get
        {
            return id2;
        }
        set
        {
            id2 = value;
        }
    }

    public int Type
    {
        get
        {
            return type;
        }
        set
        {
            type = value;
        }
    }

    public string Emluator
    {
        get
        {
            return emluator;
        }
        set
        {
            emluator = value;
        }
    }

    public string Max
    {
        get
        {
            return max;
        }
        set
        {
            max = value;
        }
    }

    public string Min
    {
        get
        {
            return min;
        }
        set
        {
            min = value;
        }
    }

    public string Cycle
    {
        get
        {
            return cycle;
        }
        set
        {
            cycle = value;
        }
    }

    public string Delay
    {
        get
        {
            return delay;
        }
        set
        {
            delay = value;
        }
    }

    public bool Indatabase
    {
        get
        {
            return indatabase;
        }
        set
        {
            indatabase = value;
        }
    }

    public bool Invalidate
    {
        get
        {
            return invalidate;
        }
        set
        {
            invalidate = value;
        }
    }

    public bool Isalive
    {
        get
        {
            return isalive;
        }
        set
        {
            isalive = value;
        }
    }

    public List<int> RBev
    {
        get
        {
            return rBev;
        }
        set
        {
            rBev = value;
        }
    }

    public List<int> WBev
    {
        get
        {
            return wBev;
        }
        set
        {
            wBev = value;
        }
    }
}
