using UnityEngine;

namespace _BaseFeatures.DOAnimations.Basic
{
    public class SimpleScale : BaseAnimation
    {
        protected Transform m_transform;
        protected Vector3 start;
        protected Vector3 end;
        public SimpleScale(Transform m_transform, Vector3 start, Vector3 end)
        {
            this.m_transform = m_transform;
            this.start = start;
            this.end = end;
        }
        protected override int GetInstanceID() => m_transform.gameObject.GetInstanceID();
        protected override void OnUpdate(float evaluate)
        {
            if (HideInHierarchy()) return;
            m_transform.localScale = start + (end - start) * evaluate;
        }

        protected bool HideInHierarchy()
        {
            if (m_transform != null)
            {
                if (!m_transform.gameObject.activeInHierarchy)
                {
                    Stop();
                    return true;
                }
                else
                    return false;
            }
            else
            {
                return false;
            }
            
        }
    }
}
