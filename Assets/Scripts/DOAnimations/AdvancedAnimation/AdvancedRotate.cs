using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _BaseFeatures.DOAnimations.Advanced
{
    public class AdvancedRotate : AdvancedScale
    {
        private Vector3 dummyEuler;
        public AdvancedRotate(KeyNode<Vector3>[] keyFrames, Transform m_transform) : base(keyFrames, m_transform)
        {
            this.m_transform = m_transform;
        }
        protected override void OnUpdate(float evaluate, Vector3 startValue, Vector3 endValue)
        {
            dummyEuler.x = startValue.x + (endValue.x - startValue.x) * evaluate;
            dummyEuler.y = startValue.y + (endValue.y - startValue.y) * evaluate;
            dummyEuler.z = startValue.z + (endValue.z - startValue.z) * evaluate;
            m_transform.rotation = Quaternion.Euler(dummyEuler);
        }
    }
}
