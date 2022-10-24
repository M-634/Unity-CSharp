using UnityEngine;
using Cysharp.Threading.Tasks;

public class TestCustomDebug : MonoBehaviour
{
    /// <summary>
    /// Debug.Log�ƃJ�X�^��Debug�N���X�̔�r�ƃT���v��
    /// </summary>
    /// <returns></returns>
    private async UniTaskVoid Start()
    {
        Debug.Log("test");
        await UniTask.Delay(System.TimeSpan.FromSeconds(1f));
        DebugExtension.Log("tes<color=red><size=30>t</size></color>");
        await UniTask.Delay(System.TimeSpan.FromSeconds(1f));
        DebugExtension.Log("test", DebugExtension.LogTextSize.Default, DebugExtension.LogTextColorType.Yellow);
        await UniTask.Delay(System.TimeSpan.FromSeconds(1f));
        DebugExtension.LogWarning("test", DebugExtension.LogTextSize.Big, DebugExtension.LogTextColorType.Green);
        await UniTask.Delay(System.TimeSpan.FromSeconds(1f));
        DebugExtension.LogError("test", DebugExtension.LogTextSize.Huge, DebugExtension.LogTextColorType.Red);
    }
}
