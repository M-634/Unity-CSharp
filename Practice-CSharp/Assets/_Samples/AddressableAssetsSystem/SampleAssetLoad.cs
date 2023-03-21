using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class SampleAssetLoad : MonoBehaviour
{
     [SerializeField] 
     private Transform root;

     [SerializeField] 
     private string path;

     private readonly IAssetLoader assetLoader = new AssetLoader();

     private void Start()
     {
        SampleLoadExecute(path).Forget();     
     }
     
     private async UniTask SampleLoadExecute(string path)
     {
         var instance = await assetLoader.Load<GameObject>(path);
         if (instance.isSucceed)
         {
             Instantiate(instance.result, root);
         }
     }

     private void OnDestroy()
     {
         assetLoader.Release();
     }
}
