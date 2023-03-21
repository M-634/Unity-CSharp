using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

internal interface IAssetLoader
{
    void Release();
    
    UniTask<(bool isSucceed, TResult result)> Load<TResult>(string path);
}


internal class AssetLoader : IAssetLoader
{
    private const string InvalidKeyExceptionErrorMessage = "AssetLoaderに指定された読み込み先のパス : {0}が" + "Addressable Groupに存在しないので確認してみてね！";
    
    private event Action ReleaseAction;
    
    public void Release()
    {
        ReleaseAction?.Invoke();
    }

    public async UniTask<(bool isSucceed, TResult result)> Load<TResult>(string path)
    {
        AsyncOperationHandle<TResult> handle = Addressables.LoadAssetAsync<TResult>(path);
        await handle.Task.AsUniTask();
        
        //例外がなければロード成功
        bool isSucceed = !CheckException(handle, path) && handle.Result != null;

        //アセットリリース時の処理を委譲する
        ReleaseAction += ()=>
        {
            Addressables.Release(handle);
            AppDebugExtension.Log($"{path}がリリースされました");
        };
        
        return (isSucceed, handle.Result);
    }
    
    private  bool CheckException<TResult>(AsyncOperationHandle<TResult> handle, string path = "")
    {
        if (handle.OperationException == null)
        {
            return false;
        }
        
        if (handle.OperationException.InnerException == null)
        {
            //こっちは、ライブラリからエラーメッセージが飛んでくるので、ここでは何もしない
            return true;
        }

        //より詳細のエラーメッセージを出力する
        switch (handle.OperationException.InnerException)
        {
            case InvalidKeyException:
                AppDebugExtension.LogError(string.Format(InvalidKeyExceptionErrorMessage, path));
                break;
            default:
                AppDebugExtension.LogError(handle.OperationException.InnerException.Message);
                break;
        }

        return true;
    }
}
