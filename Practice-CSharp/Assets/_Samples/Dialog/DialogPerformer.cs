using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace _Samples.Dialog
{

    public interface IDialogPerformer
    {
        void Init();

        UniTask Open();

        UniTask Close();
    }
    
    /// <summary>
    ///　ダイアログの動きを制御するクラス
    /// </summary>
    public class DialogPerformer : MonoBehaviour, IDialogPerformer
    {
        [SerializeField] 
        private RectTransform rectTransform;

        [SerializeField] 
        private DialogPerformerParamSo dialogPerformerParam;
        
        public void Init()
        {
            rectTransform.DOScale(0f, 0f);
        }

        public async UniTask Open()
        {
            //きれいにダイアログを開くために、1フレーム待機させる.
            await UniTask.WaitForEndOfFrame(this).SuppressCancellationThrow();
         
            await rectTransform.DOScale(1f, dialogPerformerParam.OpenDuration).ToUniTask();
        }

        public async UniTask Close()
        {
            await rectTransform.DOScale(0f, dialogPerformerParam.CloseDuration).ToUniTask();
        }
    }
}