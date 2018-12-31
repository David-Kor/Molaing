//  Debug 클래스 재정의
//  빌드 단계에서의 로그 출력을 방지하기 위함
//  
//  [ 출처 ]
//  https://github.com/pb0/DebugDisabler/blob/master/Debug.cs
//  

#if !ENABLE_LOG
using UnityEngine;

public class Debug
{
    public static bool developerConsoleVisible
    {
        get { return UnityEngine.Debug.developerConsoleVisible; }
        set { UnityEngine.Debug.developerConsoleVisible = value; }
    }

    public static bool isDebugBuild
    {
        get { return UnityEngine.Debug.isDebugBuild; }
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void Break()
    {
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void ClearDeveloperConsole()
    {
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void DebugBreak()
    {
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void DrawLine(Vector3 start, Vector3 end)
    {
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void DrawLine(Vector3 start, Vector3 end, Color color)
    {
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
    {
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration, bool depthTest)
    {
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void DrawRay(Vector3 start, Vector3 dir)
    {
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void DrawRay(Vector3 start, Vector3 dir, Color color)
    {
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration)
    {
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void DrawRay(Vector3 start, Vector3 dir, Color color, float duration, bool depthTest)
    {
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void Log(object message)
    {
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void Log(object message, Object context)
    {
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void LogError(object message)
    {
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void LogError(object message, Object context)
    {
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void LogException(System.Exception exception)
    {
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void LogException(System.Exception exception, Object context)
    {
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void LogWarning(object message)
    {
    }

    [System.Diagnostics.Conditional("ENABLE_LOG")]
    public static void LogWarning(object message, Object context)
    {
    }
}

#endif