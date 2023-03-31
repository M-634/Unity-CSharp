using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Samples.Dialog
{
    
    public interface IDialogPerformer
    {
        void Hide();

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
        
        public void Hide()
        {
            rectTransform.DOScale(0f, 0f);
            gameObject.SetActive(false);
        }

        public async UniTask Open()
        {
            gameObject.SetActive(true);
            
            //きれいにダイアログを開くために、1フレーム待機させる.
            await UniTask.WaitForEndOfFrame(this).SuppressCancellationThrow();
         
            await rectTransform.DOScale(1f, dialogPerformerParam.OpenDuration).ToUniTask();
        }

        public async UniTask Close()
        {
            await rectTransform.DOScale(0f, dialogPerformerParam.CloseDuration).ToUniTask();
            Destroy(gameObject);
        }
    }
}