using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Samples.Dialog
{
    
    public interface IDialogPerformer
    {
        void Hide();

        UniTask Open();

        UniTask Close();

        IObservable<Unit> OnEndPerformer { get; }
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

        public IObservable<Unit> OnEndPerformer => onEndPerformer;
        private readonly Subject<Unit> onEndPerformer = new Subject<Unit>();
        
        public void Hide()
        {
            Assert.IsNotNull(gameObject);
            
            rectTransform.DOScale(0f, 0f);
            gameObject.SetActive(false);
        }

        public async UniTask Open()
        {
            Assert.IsNotNull(gameObject);
            
            gameObject.SetActive(true);
            
            //きれいにダイアログを開くために、1フレーム待機させる.
            await UniTask.WaitForEndOfFrame(this).SuppressCancellationThrow();
         
            await rectTransform.DOScale(1f, dialogPerformerParam.OpenDuration).ToUniTask();
        }

        public async UniTask Close()
        {
            Assert.IsNotNull(gameObject);
            
            await rectTransform.DOScale(0f, dialogPerformerParam.CloseDuration).ToUniTask();
            onEndPerformer.OnNext(Unit.Default);
            Destroy(gameObject);
        }
    }
}