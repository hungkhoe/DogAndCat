using UnityEngine;

namespace _BaseFeatures.DOAnimations.Basic
{
    public class SimpleRotate : SimpleScale
    {
        private Vector3 dummyEuler;
        public SimpleRotate(Transform m_transform, Vector3 start, Vector3 end) : base(m_transform, start, end) { }
        protected override void OnUpdate(float evaluate)
        {
            if (HideInHierarchy()) return;
            dummyEuler.x = start.x + (end.x - start.x) * evaluate;
            dummyEuler.y = start.y + (end.y - start.y) * evaluate;
            dummyEuler.z = start.z + (end.z - start.z) * evaluate;
            m_transform.rotation = Quaternion.Euler(dummyEuler);
        }
    }
}
