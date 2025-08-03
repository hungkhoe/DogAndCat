using UnityEngine;

namespace _BaseFeatures.DOAnimations.Basic
{
    public class SimpleMoveBezier : BaseAnimation
    {
        private readonly Transform m_transform;
        private readonly Vector3[] points;
        private Vector3 nextPoint;
        private Vector3 positionCached;
        private bool isNotUpdateX;
        //private bool IsLookAtPath;
        public SimpleMoveBezier(Transform m_transform, Vector3[] points)
        {
            this.m_transform = m_transform;
            this.points = points;
        }
        public BaseAnimation NotUpdateX()
        {
            isNotUpdateX = true;
            return this;
        }
        //public BaseAnimation SetLookAt(bool isLookAt)
        //{
        //    this.IsLookAtPath = isLookAt;
        //    return this;
        //}
        //public override void Play()
        //{
        //    nextPoint = CalculateBezierPoint(Time.deltaTime * 1 / duration, points);
        //    base.Play();
        //}
        protected override int GetInstanceID()
        {
            return m_transform.gameObject.GetInstanceID();
        }

        protected override void OnUpdate(float evaluate)
        {
            nextPoint = CalculateBezierPoint(evaluate, points);
            positionCached = m_transform.position;
            //nextPoint = CalculateBezierPoint(evaluate, points);
            //if (IsLookAtPath)
            //    m_transform.rotation = Quaternion.LookRotation(Vector3.forward, nextPoint - m_transform.position);
            if (!isNotUpdateX) positionCached.x = nextPoint.x;
            positionCached.y = nextPoint.y;
            positionCached.z = nextPoint.z;
            m_transform.position = positionCached;// CalculateBezierPoint(evaluate, points); //nextPoint;

            //Debug.LogErrorFormat("<color=lime>{0}</color>", m_transform.position);
        }
        private Vector3 CalculateBezierPoint(float t, params Vector3[] points)
        {
            switch (points.Length)
            {
                case 3:
                    return GetQuadraticBezierPoint(t, points[0], points[1], points[2]);
                case 4:
                    return GetCubicBezierPoint(t, points[0], points[1], points[2], points[3]);
                default:
                    Debug.LogError("points'Lengh invalid. Must is 3 or 4");
                    return Vector3.zero;
            }
        }

        //public static Vector3 GetPoint(float t,Vector3 p0, Vector3 p1, Vector3 p2)
        //{
        //    return Vector3.Lerp(Vector3.Lerp(p0, p1, t), Vector3.Lerp(p1, p2, t), t);
        //}
        private Vector3 GetQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            return (1 - t) * (1 - t) * p0 + 2 * (1 - t) * t * p1 + t * t * p2;
        }

        private Vector3 GetCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            return (1 - t) * (1 - t) * (1 - t) * p0 + 3 * (1 - t) * (1 - t) * t * p1 + 3 * (1 - t) * t * t * p2 + t * t * t * p3;
        }
    }
}
