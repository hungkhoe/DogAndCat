using UnityEngine;

namespace _BaseFeatures.DOAnimations.Advanced
{
    public class AdvancedScale : AdvancedAnimation<Vector3>
    {
        protected Transform m_transform;
        public AdvancedScale(KeyNode<Vector3>[] keyFrames, Transform m_transform) : base(keyFrames)
        {
            this.m_transform = m_transform;
        }
        protected override int GetInstanceID() => m_transform.gameObject.GetInstanceID();
        protected override void OnUpdate(float evaluate, Vector3 startValue, Vector3 endValue)
            => m_transform.localScale = startValue + (endValue - startValue) * evaluate;
        protected override void OnUpdate(float evaluate) { }
    }
}
