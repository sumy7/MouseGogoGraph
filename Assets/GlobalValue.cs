using System;

public class GlobalValue
{
    private static readonly GlobalValue _instance = new GlobalValue();

    public static GlobalValue Instance
    {
        get
        {
            return _instance;
        }
    }

    private GlobalValue() { }

    // 是否显示调试信息
    private Boolean _debuginfo = false;

    public Boolean DebugInfo
    {
        get
        {
            return _debuginfo;
        }
        set
        {
            _debuginfo = value;
        }
    }

}