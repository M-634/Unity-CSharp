using UnityEngine;

namespace _Samples.Debug
{
    [CreateAssetMenu(fileName = "ItemLocalMaster", menuName = "CreateScriptableObject/ItemLocalMaster", order = 0)]
    public  class ItemLocalMaster : BaseLocalMasterScriptableObject<ItemLocalMaster>
    {
        public int id;
        
        public string itemName;
    }
}