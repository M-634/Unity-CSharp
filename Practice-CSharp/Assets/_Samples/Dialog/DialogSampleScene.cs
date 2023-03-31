using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace _Samples.Dialog
{
    public class DialogSampleScene : MonoBehaviour
    {
        /// <summary>
        /// このシーンで使用予定のDialogPrefab
        /// </summary>
        [SerializeField] 
        private GameObject[] dialogPrefabs;
        
        private IDialogCreator dialogCreator;
        
        private void Start()
        {
            dialogCreator = new DialogCreator(dialogPrefabs, DialogInstanceManager.Instance);
            
            SampleDialogModel model = new SampleDialogModel(dialogCreator);
            
            model.OnOpenComplete.Subscribe(_ =>
            {
                AppDebugExtension.Log("開いた！");
            }).AddTo(model.Disposable);

            model.OnCloseComplete.Subscribe(_ =>
            {
                AppDebugExtension.Log("閉じた！");
            }).AddTo(model.Disposable);
            
            dialogCreator.CreateDialog<SampleDialogPresenter, SampleDialogModel>(model);
        }
    }
}