using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Samples.Dialog
{
    public interface IDialogCreator
    {
        void CreateDialog<TPresenter, TModel>(TModel model)
            where TModel: DialogModel
            where TPresenter : DialogPresenter<TModel>;
    }

    public class DialogCreator : IDialogCreator
    {
        private readonly IEnumerable<GameObject> dialogPrefabs;
        private readonly IDialogInstanceManager dialogInstanceManager;
        
        public DialogCreator(IEnumerable<GameObject> dialogPrefabs, IDialogInstanceManager dialogInstanceManager)
        {
            this.dialogPrefabs = dialogPrefabs;
            this.dialogInstanceManager = dialogInstanceManager;
        }
        
        /// <summary>
        /// Dialogを生成する関数
        /// </summary>
        /// <param name="model">Dialogのmodelクラスのインスタンス</param>
        /// <typeparam name="TPresenter">modelと対となるpresenterクラスタイプ</typeparam>
        /// <typeparam name="TModel">modelクラスタイプ</typeparam>
        public void CreateDialog<TPresenter, TModel>(TModel model) 
            where TModel: DialogModel
            where TPresenter : DialogPresenter<TModel>
        {
            (bool isSuccess, TPresenter presenterSource) = TryGetPresenterComponent<TPresenter, TModel>();

            if (!isSuccess)
            {
                AppDebugExtension.LogError($"ダイアログの作成に失敗しました");
                return;
            }

            if (DialogInstanceManager.Instance == null)
            {
                AppDebugExtension.LogError($"ダイアログをインスタンスするDialogManagerが存在しないよ！");
                return;
            }

            (TPresenter presenterInstance, IDialogPerformer performer) = dialogInstanceManager.GetInstanceDialogComponent<TPresenter, TModel>(presenterSource);

            //inject performer to model
            model.InjectPerformerFunction(performer);
            
            //inject model to　presenter
            presenterInstance.Initialize(model);
            
            //Run event presenter
            presenterInstance.Run();
            
            //open dialog
            model.Open().Forget();
        }

        private (bool, TPresenter) TryGetPresenterComponent<TPresenter, TModel>()
            where TModel : DialogModel
            where TPresenter : DialogPresenter<TModel>
        {
            foreach (GameObject prefab in dialogPrefabs)
            {
                if (prefab.TryGetComponent(out TPresenter presenter))
                {
                    return (true, presenter);
                }
            }

            return (false, null);
        }
    }
}