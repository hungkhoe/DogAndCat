using UnityEngine;
using UnityEngine.Events;

namespace _BaseFeatures.DOAnimations.Basic
{
    public class SimpleMove : SimpleScale
    {
        private enum Axis { NONE, AXIS_X, AXIS_Y, AXIS_Z}
        private bool _isLocal = false;
        private Axis _axis = Axis.NONE;
        private Vector3 _positionCached;
        public SimpleMove(Transform m_transform, Vector3 start, Vector3 end) : base(m_transform, start, end)
        {
            this.m_transform = m_transform;
            _positionCached = m_transform.position;
        }
        public SimpleMove SetLocal()
        {
            _isLocal = true;
            _positionCached = m_transform.localPosition;
            return this;
        }
        public SimpleMove SetAxisX() { _axis = Axis.AXIS_X;  return this; }
        public SimpleMove SetAxisY() { _axis = Axis.AXIS_Y;  return this; }
        public SimpleMove SetAxisZ() { _axis = Axis.AXIS_Z;  return this; }
        protected override int GetInstanceID() => m_transform.gameObject.GetInstanceID();
        protected override void OnUpdate(float evaluate)
        {
            if (HideInHierarchy()) return;
            UpdatePosition(evaluate);
        }

        private void UpdatePosition(float evaluate)
        {
            switch (_axis)
            {
                case Axis.NONE:
                    SetNewPosition(start + (end - start) * evaluate);
                    break;
                case Axis.AXIS_X:
                    _positionCached = _isLocal ? m_transform.localPosition : m_transform.position;
                    _positionCached.x = start.x + (end.x - start.x) * evaluate;
                    SetNewPosition(_positionCached);
                    break;
                case Axis.AXIS_Y:
                    _positionCached = _isLocal ? m_transform.localPosition : m_transform.position;
                    _positionCached.y = start.y + (end.y - start.y) * evaluate;
                    SetNewPosition(_positionCached);
                    break;
                case Axis.AXIS_Z:
                    _positionCached = _isLocal ? m_transform.localPosition : m_transform.position;
                    _positionCached.z = start.z + (end.z - start.z) * evaluate;
                    SetNewPosition(_positionCached);
                    break;
            }
        }
        private void SetNewPosition(Vector3 pos)
        {
            if (!_isLocal)
                m_transform.position = pos;
            else
                m_transform.localPosition = pos;
        }
    }
}
