using System.Collections.Generic;
using UnityEngine;

namespace _Samples.Debug
{
    public class DebugLocalMaster : MonoBehaviour
    {
        [SerializeField]
        private List<ItemLocalMaster> itemLocalMasters;

        [SerializeField]
        private string[] pathes;  
        
        private void Start()
        {
            itemLocalMasters = new List<ItemLocalMaster>();

            //TODOここをResoucesフォルダーから直接指定できるとよい
            foreach (string path in pathes)
            {
                itemLocalMasters.Add(ItemLocalMaster.Entity(path));
            }
        }
    }
}