
using UnityEngine;
using _BaseFeatures.DOAnimations.Basic;
using _BaseFeatures.DOAnimations.Advanced;
using UnityEngine.UI;
using UnityEngine.Events;

namespace _BaseFeatures.DOAnimations
{
    public static class AnimationExtensions
    {
        public static void Clear() => AnimationBehaviour.Instance.KillAll();
        public static void DOKill(this Transform root) => AnimationBehaviour.Instance.KillAnimation(root.gameObject.GetInstanceID());
        public static void DOKill<T>(this T root) where T : Component => AnimationBehaviour.Instance.KillAnimation(root.gameObject.GetInstanceID());
        public static void DOKill(this int instanceID) => AnimationBehaviour.Instance.KillAnimation(instanceID);
        public static void CancelSchedule<T>(this T root) where T : Object => AnimationBehaviour.Instance.KillAnimation(root.GetInstanceID());
        /// <summary>
        /// Schedule invoke the 'method' in 'time' seconds
        /// </summary>
        /// <typeparam name="T">Behaviour</typeparam>
        /// <param name="procedureKeeper"></param>
        /// <param name="method"></param>
        /// <param name="time"></param>
        public static BaseAnimation DOSchedule<T>(this T procedureKeeper, UnityAction method, float time) where T : Object
            => new Scheduler(procedureKeeper.GetInstanceID()).SetDuration(time).OnComplete(method).Play();
        public static BaseAnimation DOSchedule(this int idProcedure, UnityAction method, float time)
            => new Scheduler(idProcedure).SetDuration(time).OnComplete(method).Play();
        public static BaseAnimation DOScheduleLoop<T>(this T procedureKeeper, UnityAction method, float time) where T : Object
            => new Scheduler(procedureKeeper.GetInstanceID()).SetDuration(time).SetLoop(-1).OnComplete(method).Play();
        public static SimpleAnimation DOUpdatePerFrame<T>(this T root, UnityAction<float> action) where T : Component
            => new SimpleAnimation(root.gameObject.GetInstanceID()).SetUpdateCallback(action);
        public static ChangeFloat DOFloatProgression<T>(this T root, float start, float end, UnityAction<float> callback) where T : Component
            => new ChangeFloat(root.gameObject.GetInstanceID(), start, end).SetUpdateCallback(callback);
        static public ChangeFloat DOFloat<T>(this T root, float start, float end) where T : Component
            => new ChangeFloat(root.gameObject.GetInstanceID(), start, end);
        public static BaseAnimation DOScale<T>(this T root, Vector3 start, Vector3 end) where T : Component
            => new SimpleScale(root as Transform ?? root.transform, start, end);
        public static BaseAnimation DORotate<T>(this T root, Vector3 start, Vector3 end) where T : Component
            => new SimpleRotate(root as Transform ?? root.transform, start, end);
        public static BaseAnimation DOSpriteSheet<T>(this T root, Sprite[] sprites) where T : Component
            => new SpriteSheetAnimation(root as SpriteRenderer ?? root.GetComponent<SpriteRenderer>(), sprites);
        public static BaseAnimation DOImageSheet<T>(this T root, Sprite[] sprites) where T : Component
            => new ImageSheetAnimation(root as Image ?? root.GetComponent<Image>(), sprites);
        public static SimpleMove DOMove<T>(this T root, Vector3 start, Vector3 end) where T : Component
            => new SimpleMove(root as Transform ?? root.transform, start, end);
        public static SimpleMove DOMoveX<T>(this T root, float startX, float endX) where T : Component
        {
            Transform m_transform = root as Transform ?? root.transform;
            return new SimpleMove(m_transform, new Vector3(startX, m_transform.position.y, m_transform.position.z), new Vector3(endX, m_transform.position.y, m_transform.position.z)).SetAxisX();
        }
        public static SimpleMove DOMoveY<T>(this T root, float startY, float endY) where T : Component
        {
            Transform m_transform = root as Transform ?? root.transform;
            return new SimpleMove(m_transform, new Vector3(m_transform.position.x, startY, m_transform.position.z), new Vector3(m_transform.position.x, endY, m_transform.position.z)).SetAxisY();
        }
        public static SimpleMove DOMoveZ<T>(this T root, float startZ, float endZ) where T : Component
        {
            Transform m_transform = root as Transform ?? root.transform;
            return new SimpleMove(m_transform, new Vector3(m_transform.position.x, m_transform.position.y, startZ), new Vector3(m_transform.position.x, m_transform.position.y, endZ)).SetAxisZ();
        }
        public static SimpleMove DOMoveLocalX<T>(this T root, float startX, float endX) where T : Component
        {
            Transform m_transform = root as Transform ?? root.transform;
            return new SimpleMove(m_transform, new Vector3(startX, m_transform.localPosition.y, m_transform.localPosition.z), new Vector3(endX, m_transform.localPosition.y, m_transform.localPosition.z)).SetLocal().SetAxisX();
        }
        public static SimpleMove DOMoveLocalY<T>(this T root, float startY, float endY) where T : Component
        {
            Transform m_transform = root as Transform ?? root.transform;
            return new SimpleMove(m_transform, new Vector3(m_transform.localPosition.x, startY, m_transform.localPosition.z), new Vector3(m_transform.localPosition.x, endY, m_transform.localPosition.z)).SetLocal().SetAxisY();
        }
        public static SimpleMove DOMoveLocalZ<T>(this T root, float startZ, float endZ) where T : Component
        {
            Transform m_transform = root as Transform ?? root.transform;
            return new SimpleMove(m_transform, new Vector3(m_transform.localPosition.x, m_transform.localPosition.y, startZ), new Vector3(m_transform.localPosition.x, m_transform.localPosition.y, endZ)).SetLocal().SetAxisZ();
        }

        public static BaseAnimation DOAnchoredMove<T>(this T root, Vector3 start, Vector3 end) where T : Component
            => new SimpleAnchoredMove(root as RectTransform ?? root.GetComponent<RectTransform>(), start, end);
        public static SimpleMoveBezier DOMoveBezier<T>(this T root, params Vector3[] points) where T : Component
            => new SimpleMoveBezier(root as Transform ?? root.transform, points);
        public static FadeColor DOFadeColor<T>(this T root, Color start, Color end, UnityAction<Color> callback) where T : Component
            => new FadeColor(root.gameObject.GetInstanceID(), start, end).SetUpdateCallback(callback);
        public static BaseAnimation DOColorSprite<T>(this T root, Color start, Color end) where T : Component
            => new SimpleSpriteColor(root as SpriteRenderer ?? root.GetComponent<SpriteRenderer>(), start, end);
        public static BaseAnimation DOColorImage<T>(this T root, Color start, Color end) where T : Component
            => new SimpleImageColor(root as Image ?? root.GetComponent<Image>(), start, end);
        public static BaseAnimation DOCrossAlpha<T>(this T root, float start, float end) where T : Component
            => new SimpleFadeAlpha(root as CanvasGroup ?? root.GetComponent<CanvasGroup>(), start, end);
        public static BaseAnimation DOScale<T>(this T root, params KeyNode<Vector3>[] keyFrames) where T : Component
           => new AdvancedScale(keyFrames, root as Transform ?? root.transform);
        public static BaseAnimation DORotate<T>(this T root, params KeyNode<Vector3>[] keyFrames) where T : Component
           => new AdvancedRotate(keyFrames, root as Transform ?? root.transform);
        public static BaseAnimation DOMove<T>(this T root, params KeyNode<Vector3>[] keyFrames) where T : Component
            => new AdvancedMove(keyFrames, root as Transform ?? root.transform);
        public static BaseAnimation DOColorSprite<T>(this T root, params KeyNode<Color>[] keyFrames) where T : Component
            => new AdvancedSpriteColor(keyFrames, root as SpriteRenderer ?? root.GetComponent<SpriteRenderer>());
    }
}
