using UnityEngine;

namespace _BaseFeatures.DOAnimations.Advanced
{
    public class AdvancedMove : AdvancedScale
    {
        public AdvancedMove(KeyNode<Vector3>[] keyFrames, Transform m_transform) : base(keyFrames, m_transform)
        {
            this.m_transform = m_transform;
        }
        protected override void OnUpdate(float evaluate, Vector3 startValue, Vector3 endValue)
            => m_transform.position = startValue + (endValue - startValue) * evaluate;
    }
}
