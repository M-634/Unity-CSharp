using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Samples.Dialog
{
    public class DialogSampleScene : MonoBehaviour
    {
        /// <summary>
        /// このシーンで使用予定のDialogPrefab
        /// </summary>
        [SerializeField] 
        private GameObject[] dialogPrefabs;

        [SerializeField]
        private Button openDialogButton;

        private IDialogCreator dialogCreator;
        
        private void Start()
        {
            dialogCreator = new DialogCreator(dialogPrefabs, DialogInstanceManager.Instance);

            openDialogButton
                .OnClickAsObservable()
                .Subscribe(_=> OpenDialogButton())
                .AddTo(this);
        }

        private void OpenDialogButton()
        {
           OneButtonDialogModel model = new OneButtonDialogModel(); 
           
            model.OnOpenComplete.Subscribe(_ =>
            {
                AppDebugExtension.Log("開いた！");
            }).AddTo(model.Disposable);

            model.OnCloseComplete.Subscribe(_ =>
            {
                AppDebugExtension.Log("閉じた！");
            }).AddTo(model.Disposable);
            
            dialogCreator.CreateDialog<OneButtonDialogPresenter, OneButtonDialogModel>(model);
        }
    }
}