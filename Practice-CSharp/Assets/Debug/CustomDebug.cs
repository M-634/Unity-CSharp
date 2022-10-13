using System.Collections.Generic;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

/// <summary>
/// Unity標準のDebugクラスの拡張。
/// Scritable Define Symboleで「DEBUG_MODE」をシンボル
/// として設定するとConsole Windowにログが表示される。
/// </summary>
public static class DebugExtension
{
    [Conditional("DEBUG_MODE")]
    public static void Log(object message)
    {
        Debug.Log($"<color={LogTextColorCodeContainer[LogTextColorType.White]}>{message}</color>");
    }


    [Conditional("DEBUG_MODE")]
    public static void Log(object message, LogTextSize size = LogTextSize.Default, LogTextColorType color = LogTextColorType.White)
    {
        Debug.Log($"<size={(int)size}><color={LogTextColorCodeContainer[color]}>{message}</color></size>");
    }

    [Conditional("DEBUG_MODE")]
    public static void LogWarning(object message)
    {
        Debug.LogWarning($"<color={LogTextColorCodeContainer[LogTextColorType.White]}>{message}</color>");
    }

    [Conditional("DEBUG_MODE")]
    public static void LogWarning(object message, LogTextSize size = LogTextSize.Default, LogTextColorType color = LogTextColorType.White)
    {
        Debug.LogWarning($"<size={(int)size}><color={LogTextColorCodeContainer[color]}>{message}</color></size>");
    }

    [Conditional("DEBUG_MODE")]
    public static void LogError(object message)
    {
        Debug.LogError($"<color={LogTextColorCodeContainer[LogTextColorType.White]}>{message}</color>");
    }


    [Conditional("DEBUG_MODE")]
    public static void LogError(object message, LogTextSize size = LogTextSize.Default, LogTextColorType color = LogTextColorType.White)
    {
        Debug.LogError($"<size={(int)size}><color={LogTextColorCodeContainer[color]}>{message}</color></size>");
    }

    public enum LogTextSize
    {
        Default = 0,
        Big = 30,
        Huge = 50,
    }

    public enum LogTextColorType
    {
        White, Black, Blue, Red, Yellow, Green
    }

    private static readonly Dictionary<LogTextColorType, string> LogTextColorCodeContainer = new Dictionary<LogTextColorType, string>
    {
        {LogTextColorType.White, "white" },
        {LogTextColorType.Black,"black" },
        {LogTextColorType.Blue, "blue"},
        {LogTextColorType.Red, "red" },
        {LogTextColorType.Yellow, "yellow" },
        {LogTextColorType.Green, "green" },
    };
}


