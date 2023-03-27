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
        private readonly IDialogGenerator dialogGenerator;
        
        public DialogCreator(IEnumerable<GameObject> dialogPrefabs, IDialogGenerator dialogGenerator)
        {
            this.dialogPrefabs = dialogPrefabs;
            this.dialogGenerator = dialogGenerator;
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

            if (DialogGenerator.Instance == null)
            {
                AppDebugExtension.LogError($"ダイアログをインスタンスするDialogManagerが存在しないよ！");
                return;
            }

            (TPresenter presenterInstance, IDialogPerformer performer) = dialogGenerator.GetInstanceDialogComponent<TPresenter, TModel>(presenterSource);

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