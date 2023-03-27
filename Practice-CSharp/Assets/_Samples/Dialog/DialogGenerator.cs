using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace _Samples.Dialog
{
    /******************************************************************
     * DialogSceneに配置するコンポーネント。
     * このコンポーネントはシングルトンで運用する。
     *******************************************************************/

    public interface IDialogGenerator
    {
        public (TPresenter, IDialogPerformer)GetInstanceDialogComponent<TPresenter, TModel>(TPresenter dialogSourcePrefab)
            where TModel : DialogModel
            where TPresenter : DialogPresenter<TModel>;
    }
    
    
    /// <summary>
    /// DialogをObjectとして実際にゲームシーンに生成する役割をするクラス
    /// Instanceを取得する際は、各シーンクラスで取得すること
    /// </summary>
    public class DialogGenerator : MonoBehaviour, IDialogGenerator
    {
        [SerializeField]
        private Transform root;
        
        private static DialogGenerator instance;
        public static DialogGenerator Instance => instance;
        
        private IDialogPerformer currentInstanceDialog;
        
        //TODO@ test用でpublicにしている。実装が完了したらprivate readonly に変換させる
        public List<IDialogPerformer> instanceDialogList = new List<IDialogPerformer>();

        private void Awake()
        {
            instance = this;
        }
        
        public  (TPresenter, IDialogPerformer) GetInstanceDialogComponent<TPresenter, TModel>(TPresenter dialogSourcePrefab)
            where TModel : DialogModel
            where TPresenter : DialogPresenter<TModel>
        {
            //開いているダイアログがあれば閉じる
            if (currentInstanceDialog != null)
            { 
                currentInstanceDialog.Close().Forget();
            }
            
            //ダイアログオブジェクトの生成
            TPresenter instancePresenter =  Instantiate(dialogSourcePrefab, root);
            IDialogPerformer performer = instancePresenter.GetComponent<DialogPerformer>();

            if (performer == null)
            {
                AppDebugExtension.LogWarning($"{instancePresenter.name}に{nameof(DialogPerformer)}コンポーネントを付け忘れているよ！");
                performer = instancePresenter.AddComponent<DialogPerformer>();
            }
            
            performer.Init();
            
            currentInstanceDialog = performer;
            instanceDialogList.Add(performer);
            return (instancePresenter, performer);
        }
    }
}