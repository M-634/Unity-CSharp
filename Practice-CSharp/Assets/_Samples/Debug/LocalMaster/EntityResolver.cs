using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Samples.Debug
{
#if UNITY_EDITOR
    
    [Serializable]
    public struct DebugEntity
    {
        public string path;
        
        public Object entity;
        public bool HasValue => entity != null;
    }
    
    public class EntityResolver : MonoBehaviour
    {
        [SerializeField]
        private List<DebugEntity> entityList;
        
        public static EntityResolver Instance { get; private set; }
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void BuildEntity()
        { 
            GameObject go = new GameObject(nameof(EntityResolver));
            go.AddComponent<EntityResolver>();
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }

            Instance = this;
            entityList = new List<DebugEntity>();
            DontDestroyOnLoad(this.gameObject);
        }

        public T GetEntity<T>(string path) where T : Object
        {
            DebugEntity temp = entityList.FirstOrDefault(item => item.path == path);

            if (temp.HasValue)
            {
                return (T)temp.entity;
            }

            T entity = Resources.Load<T>(path);

            if (entity != null)
            {
                entityList.Add(new DebugEntity
                {
                    path = path,
                    entity = entity
                });
            }
            else
            {
                AppDebugExtension.LogError($"{typeof(T)}の読み込みに失敗しました！ {path} : パスが正しいか確認してください！");
            }
            
            return entity;
        }
    }

#endif
}