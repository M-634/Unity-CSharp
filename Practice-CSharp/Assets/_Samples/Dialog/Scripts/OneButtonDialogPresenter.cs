using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Samples.Dialog
{
    public class OneButtonDialogPresenter : DialogPresenter<OneButtonDialogModel>
    {
        [SerializeField]
        private Button closeButton;
        
        public override void Run()
        {
            closeButton.onClick
                .AsObservable()
                .Take(1)
                .Subscribe(_ =>
                {
                    model.Close().Forget();
                }).AddTo(this);
        }
    }

    public class OneButtonDialogModel : DialogModel
    {
        
    }
}

