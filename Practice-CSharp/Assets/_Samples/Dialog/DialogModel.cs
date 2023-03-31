using System;
using Cysharp.Threading.Tasks;
using UniRx;
using Unit = UniRx.Unit;

namespace _Samples.Dialog
{
    /// <summary>
    /// ダイアログの共通処理をするクラス
    /// </summary>
    public abstract class DialogModel : IDisposable
    {
        public IObservable<Unit> OnOpenComplete => onOpenComplete;
        private readonly Subject<Unit> onOpenComplete = new Subject<Unit>();
        
        public IObservable<Unit> OnCloseComplete => onCloseComplete;
        private readonly Subject<Unit> onCloseComplete = new Subject<Unit>();
        
        private event Func<UniTask> OpenFunc;
        private event Func<UniTask> CloseFunc;
        
        public  CompositeDisposable Disposable { get; } = new CompositeDisposable();

        public void InjectPerformerFunction(IDialogPerformer dialogPerformer)
        {
            //この関数は一回しか呼ばれない.
            if (OpenFunc != null && CloseFunc != null)
            {
                return;
            }
            
            OpenFunc = dialogPerformer.Open;
            CloseFunc = dialogPerformer.Close;
        }
        
        public async UniTask Open()
        {
            if (OpenFunc == null)
            {
                AppDebugExtension.Log($"{nameof(DialogCreator)}でmodelとpresenterを紐づけてください!");
                return;
            }
            await OpenFunc.Invoke();
            onOpenComplete?.OnNext(Unit.Default);
        }

        public async UniTask Close()
        {
            if (CloseFunc == null)
            {
                AppDebugExtension.Log($"{nameof(DialogCreator)}でmodelとpresenterを紐づけてください!");
                return;
            } 
            await CloseFunc.Invoke();
            onCloseComplete?.OnNext(Unit.Default);
        }

        public void Dispose()
        {
            OpenFunc = null;
            CloseFunc = null;
            Disposable.Dispose();
        }
    }
}