using UnityEngine;

namespace _Samples.Dialog
{
    [CreateAssetMenu(fileName = "DialogPerformerParam", menuName = "CreateScriptableObject/DialogPerformerParam", order = 0)]
    public class DialogPerformerParamSo : ScriptableObject
    {
        [SerializeField, Range(0f, 1f)]
        private float openDuration;
        public float OpenDuration => openDuration;
        
        [SerializeField, Range(0f, 1f)] 
        private float closeDuration;
        public float CloseDuration => closeDuration;
    }
}