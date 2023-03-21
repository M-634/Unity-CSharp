using UnityEngine;
using Cysharp.Threading.Tasks;

public class TestCustomDebug : MonoBehaviour
{
    /// <summary>
    /// Debug.LogとカスタムDebugクラスの比較とサンプル
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid Start()
    {
        Debug.Log("test");
        await UniTask.Delay(System.TimeSpan.FromSeconds(1f));
        AppDebugExtension.Log("tes<color=red><size=30>t</size></color>");
        await UniTask.Delay(System.TimeSpan.FromSeconds(1f));
        AppDebugExtension.Log("test", AppDebugExtension.LogTextSize.Default, AppDebugExtension.LogTextColorType.Yellow);
        await UniTask.Delay(System.TimeSpan.FromSeconds(1f));
        AppDebugExtension.LogWarning("test", AppDebugExtension.LogTextSize.Big, AppDebugExtension.LogTextColorType.Green);
        await UniTask.Delay(System.TimeSpan.FromSeconds(1f));
        AppDebugExtension.LogError("test", AppDebugExtension.LogTextSize.Huge, AppDebugExtension.LogTextColorType.Red);
    }
}
