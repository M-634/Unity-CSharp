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

    public interface IDialogInstanceManager
    {
        public (TPresenter, IDialogPerformer)GetInstanceDialogComponent<TPresenter, TModel>(TPresenter dialogSourcePrefab)
            where TModel : DialogModel
            where TPresenter : DialogPresenter<TModel>;
    }
    
    
    /// <summary>
    /// ダイアログのインスタンスされたGameObjectを管理するクラス
    /// </summary>
    public class DialogInstanceManager : MonoBehaviour, IDialogInstanceManager
    {
        [SerializeField]
        private Transform root;
        
        private static DialogInstanceManager instance;
        public static DialogInstanceManager Instance => instance;
        
        private IDialogPerformer currentDisplayDialog;
        
        //TODO@ test用でpublicにしている。実装が完了したらprivate readonly に変換させる
        private readonly List<IDialogPerformer> instanceDialogList = new List<IDialogPerformer>();

        private void Awake()
        {
            instance = this;
        }
        
        public  (TPresenter, IDialogPerformer) GetInstanceDialogComponent<TPresenter, TModel>(TPresenter dialogSourcePrefab)
            where TModel : DialogModel
            where TPresenter : DialogPresenter<TModel>
        {
            //開いているダイアログがあれば閉じる
            currentDisplayDialog?.Hide();
            
            //ダイアログオブジェクトの生成
            TPresenter instancePresenter =  Instantiate(dialogSourcePrefab, root);
            IDialogPerformer performer = instancePresenter.GetComponent<DialogPerformer>();

            if (performer == null)
            {
                AppDebugExtension.LogWarning($"{instancePresenter.name}に{nameof(DialogPerformer)}コンポーネントを付け忘れているよ！");
                performer = instancePresenter.AddComponent<DialogPerformer>();
            }
            
            performer.Hide();
            
            currentDisplayDialog = performer;
            instanceDialogList.Add(performer);
            return (instancePresenter, performer);
        }
    }
}