using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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

        [SerializeField] 
        private Image backGroundImage;
        
        private static DialogInstanceManager instance;
        public static DialogInstanceManager Instance => instance;

        private readonly Stack<IDialogPerformer> instanceHideDialogList = new Stack<IDialogPerformer>();
        
        private IDialogPerformer currentDisplayDialog;

        private CompositeDisposable compositeDisposable;
        
        private void Awake()
        {
            instance = this;
            backGroundImage.enabled = false;
        }
        
        public  (TPresenter, IDialogPerformer) GetInstanceDialogComponent<TPresenter, TModel>(TPresenter dialogSourcePrefab)
            where TModel : DialogModel
            where TPresenter : DialogPresenter<TModel>
        {
            //開いているダイアログがあれば閉じる
            if (currentDisplayDialog != null)
            {
                currentDisplayDialog.Hide();
                instanceHideDialogList.Push(currentDisplayDialog);
            }
            
            //ダイアログオブジェクトの生成
            TPresenter instancePresenter =  Instantiate(dialogSourcePrefab, root);
            IDialogPerformer performer = instancePresenter.GetComponent<DialogPerformer>();

            if (performer == null)
            {
                AppDebugExtension.LogWarning($"{instancePresenter.name}に{nameof(DialogPerformer)}コンポーネントを付け忘れているよ！");
                performer = instancePresenter.AddComponent<DialogPerformer>();
            }
            
            SetDisplayDialog(performer);
            
            return (instancePresenter, performer);
        }


        private void SetDisplayDialog(IDialogPerformer dialogPerformer)
        {
            dialogPerformer.Hide();
            
            compositeDisposable?.Dispose();
            compositeDisposable = new CompositeDisposable();
            
            currentDisplayDialog = dialogPerformer;
            backGroundImage.enabled = true;

            currentDisplayDialog.OnEndPerformer.Subscribe(_ =>
            {
                if (instanceHideDialogList.Count < 1)
                {
                    compositeDisposable?.Dispose();
                    backGroundImage.enabled = false;
                    return;
                }
                
                IDialogPerformer nextDialogPerformer = instanceHideDialogList.Pop();
                SetDisplayDialog(nextDialogPerformer);
                
            }).AddTo(compositeDisposable);

            currentDisplayDialog.Open();
        }
    }
}