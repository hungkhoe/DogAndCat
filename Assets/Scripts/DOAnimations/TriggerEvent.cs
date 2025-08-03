using UnityEngine.Events;

namespace _BaseFeatures.DOAnimations
{
    public class TriggerEvent
    {
        public float timeEvent;
        public UnityAction triggerAction;
        public TriggerEvent(float _time, UnityAction _triggerAction)
        {
            timeEvent = _time;
            triggerAction = _triggerAction;
        }
    }

    public struct KeyNode<T>
    {
        public float time;
        public T value;
        public KeyNode(float time, T value)
        {
            this.time = time;
            this.value = value;
        }
    }
}
