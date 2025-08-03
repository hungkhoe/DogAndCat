using UnityEngine;

namespace _BaseFeatures.DOAnimations.Basic
{
    public class SimpleAnchoredMove : SimpleScale
    {
        private readonly RectTransform m_RectTransform;
        public SimpleAnchoredMove(Transform m_transform, Vector3 start, Vector3 end) : base(m_transform, start, end)
        {
            m_RectTransform = m_transform.GetComponent<RectTransform>();
        }
        protected override void OnUpdate(float evaluate)
        {
            if (HideInHierarchy()) return;
            m_RectTransform.anchoredPosition = start + (end - start) * evaluate;
        }
    }
}
