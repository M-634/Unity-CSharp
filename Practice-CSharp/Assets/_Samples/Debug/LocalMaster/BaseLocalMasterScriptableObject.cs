using UnityEngine;
using Object = UnityEngine.Object;

namespace _Samples.Debug
{
    [CreateAssetMenu(fileName = "BaseLocalMaster", menuName = "CreateScriptableObject/LocalMaster", order = 0)]
    public abstract class BaseLocalMasterScriptableObject<T> : ScriptableObject where T : Object
    {
        public static T Entity(string path)
        {
            return EntityResolver.Instance.GetEntity<T>(path);
        }
        
    }
}