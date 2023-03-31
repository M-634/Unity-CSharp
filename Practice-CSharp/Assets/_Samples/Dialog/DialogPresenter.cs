using UnityEngine;

namespace _Samples.Dialog
{
    /// <summary>
    /// DialogPresenterの基底クラス.
    /// Dialogの各種Presenterを作る際は、このクラスを継承すること
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class DialogPresenter<TModel> : MonoBehaviour where TModel : DialogModel
    {
        protected TModel model;
        
        public void Initialize(TModel model)
        {
            this.model = model;
        }

        public abstract void Run();

        private void OnDestroy()
        {
            model.Dispose();
        }
    }
    
}