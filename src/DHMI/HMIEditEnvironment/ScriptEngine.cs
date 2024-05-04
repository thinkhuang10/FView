using System;
using System.Windows.Forms;
using MSScriptControl;

namespace HMIEditEnvironment;

public class ScriptEngine
{
    private readonly ScriptControlClass msc;

    public ScriptLanguage Language
    {
        get
        {
            return (ScriptLanguage)Enum.Parse(typeof(ScriptLanguage), msc.Language, ignoreCase: false);
        }
        set
        {
            msc.Language = value.ToString();
        }
    }

    public int Timeout
    {
        get
        {
            return ((IScriptControl)msc).Timeout;
        }
        set
        {
            ((IScriptControl)msc).Timeout = value;
        }
    }

    public bool AllowUI
    {
        get
        {
            return msc.AllowUI;
        }
        set
        {
            msc.AllowUI = value;
        }
    }

    public bool UseSafeSubset
    {
        get
        {
            return msc.UseSafeSubset;
        }
        set
        {
            msc.UseSafeSubset = true;
        }
    }

    public Error Error => msc.Error;

    public Modules Modules => msc.Modules;

    public event RunErrorHandler RunError;

    public event RunTimeoutHandler RunTimeout;

    public ScriptEngine()
        : this(ScriptLanguage.VBscript)
    {
    }

    public ScriptEngine(ScriptLanguage language)
    {
        msc = new ScriptControlClass
        {
            UseSafeSubset = true
        };
        ((IScriptControl)msc).Timeout = 1000000;
        msc.Language = language.ToString();
        ((DScriptControlSource_Event)msc).Error += ScriptEngine_Error;
    }

    public object Eval(string expression, string codeBody)
    {
        msc.AddCode(codeBody);
        return msc.Eval(expression);
    }

    public object Eval(ScriptLanguage language, string expression, string codeBody)
    {
        if (Language != language)
        {
            Language = language;
        }
        return Eval(expression, codeBody);
    }

    public void ExecuteStatement(string statement)
    {
        try
        {
            msc.ExecuteStatement(statement);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message + Error.Line);
        }
    }

    public void AddObject(string Name, object obj)
    {
        try
        {
            msc.AddObject(Name, obj);
        }
        catch (Exception)
        {
        }
    }

    public void AddObject(string Name, object obj, bool addMembers)
    {
        msc.AddObject(Name, obj, addMembers);
    }

    public object Run(string mainFunctionName, object[] parameters, string codeBody)
    {
        msc.AddCode(codeBody);
        ScriptControlClass scriptControlClass = msc;
        object[] Parameters = parameters;
        return scriptControlClass.Run(mainFunctionName, ref Parameters);
    }

    public object Run(ScriptLanguage language, string mainFunctionName, object[] parameters, string codeBody)
    {
        if (Language != language)
        {
            Language = language;
        }
        return Run(mainFunctionName, parameters, codeBody);
    }

    public void AddCode(string code)
    {
        msc.AddCode(code);
    }

    public void Reset()
    {
        msc.Reset();
    }

    private void OnError()
    {
        if (this.RunError != null)
        {
            this.RunError();
        }
    }

    private void OnTimeout()
    {
        if (this.RunTimeout != null)
        {
            this.RunTimeout();
        }
    }

    private void ScriptEngine_Error()
    {
        OnError();
    }

    private void ScriptEngine_Timeout()
    {
        OnTimeout();
    }
}
