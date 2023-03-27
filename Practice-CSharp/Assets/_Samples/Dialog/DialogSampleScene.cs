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
            dialogCreator = new DialogCreator(dialogPrefabs, DialogGenerator.Instance);
            
            CompositeDisposable compositeDisposable = new CompositeDisposable();
            
            SampleDialogModel model = new SampleDialogModel(dialogCreator);
            
            model.OnOpenComplete.Subscribe(_ =>
            {
                AppDebugExtension.Log("開いた！");
            }).AddTo(compositeDisposable);

            model.OnCloseComplete.Subscribe(_ =>
            {
                AppDebugExtension.Log("閉じた！");
                compositeDisposable.Dispose();
                
            }).AddTo(compositeDisposable);
            
            dialogCreator.CreateDialog<SampleDialogPresenter, SampleDialogModel>(model);
        }
    }
}