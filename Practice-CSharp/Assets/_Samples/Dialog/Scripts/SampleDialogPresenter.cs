using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Samples.Dialog
{
    public class SampleDialogPresenter : DialogPresenter<SampleDialogModel>
    {

        [SerializeField] 
        private Button createNewDialogButton;
        
        [SerializeField] 
        private Button closeButton;
        
        public override void Run()
        {
            createNewDialogButton
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    model.CreateDialog();
                }).AddTo(this);
                
            closeButton
                .OnClickAsObservable()
                .Subscribe(_=>
                {
                    model.Close().Forget();
                }).AddTo(this);
        }
    }

    public class SampleDialogModel : DialogModel
    {
        private readonly IDialogCreator dialogCreator;
        
        public SampleDialogModel(IDialogCreator dialogCreator)
        {
            this.dialogCreator = dialogCreator;
        }

        public void CreateDialog()
        {
            dialogCreator.CreateDialog<SampleDialogPresenter, SampleDialogModel>(new SampleDialogModel(dialogCreator));
        }
    }
}