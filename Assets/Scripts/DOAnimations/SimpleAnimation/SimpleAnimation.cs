using UnityEngine;
using UnityEngine.Events;

namespace _BaseFeatures.DOAnimations.Basic
{
    public class SimpleAnimation : BaseAnimation
    {
        private readonly int instanceID;
        private UnityAction<float> UpdateCallback;
        public SimpleAnimation SetUpdateCallback(UnityAction<float> UpdateCallback)
        {
            this.UpdateCallback = UpdateCallback;
            return this;
        }
        public SimpleAnimation(int instanceID)
        {
            this.instanceID = instanceID;
        }
        protected override int GetInstanceID() => instanceID;
        protected override void OnUpdate(float evaluate) => UpdateCallback?.Invoke(evaluate);
    }
}
