using UnityEngine.Events;

namespace _BaseFeatures.DOAnimations.Basic
{
    public class ChangeFloat : BaseAnimation
    {
        private readonly int instanceID;
        private float startValue;
        private float endValue;
        private UnityAction<float> UpdateCallback;
        public ChangeFloat SetUpdateCallback(UnityAction<float> UpdateCallback)
        {
            this.UpdateCallback = UpdateCallback;
            return this;
        }
        public ChangeFloat(int instanceID, float startValue, float endValue)
        {
            this.instanceID = instanceID;
            this.startValue = startValue;
            this.endValue = endValue;
        }
        protected override int GetInstanceID() => instanceID;
        protected override void OnUpdate(float evaluate) => UpdateCallback?.Invoke(startValue + (endValue - startValue) * evaluate);
        public void Play(float startValue, float endValue)
        {
            Stop();
            this.startValue = startValue;
            this.endValue = endValue;
            base.Play();
        }
    }
}
