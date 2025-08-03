using UnityEngine;

namespace _BaseFeatures.DOAnimations
{
    public class PresetCurves
    {
        public static readonly AnimationCurve Linear = AnimationCurve.Linear(0, 0, 1, 1);

        public static readonly AnimationCurve InOutEase = AnimationCurve.EaseInOut(0, 0, 1, 1);
        public static readonly AnimationCurve InEase = new AnimationCurve(new Keyframe(0, 0, 0, 0), new Keyframe(1, 1, 2, 0));
        public static readonly AnimationCurve InOutClimb = new AnimationCurve(new Keyframe(0, 0, 0, 2), new Keyframe(1, 1, 2, 0));
        public static readonly AnimationCurve InClimb = new AnimationCurve(new Keyframe(0, 0, 0, 3), new Keyframe(1, 1, 0, 0));
        public static readonly AnimationCurve InOutBounce = new AnimationCurve(new Keyframe(0, 0, 0, 0), new Keyframe(0.2f, -0.15f, 0, 0), new Keyframe(0.8f, 1.15f, 0, 0), new Keyframe(1, 1, 0, 0));
        public static readonly AnimationCurve InMiddleBounce = new AnimationCurve(new Keyframe(0, 0, 0, 0), new Keyframe(0.5f, -.15f, 0, 0), new Keyframe(1, 1, 0, 0));
        public static readonly AnimationCurve OutMiddleBounce = new AnimationCurve(new Keyframe(0, 0, 0, 0), new Keyframe(0.5f, 1.15f, 0, 0), new Keyframe(1, 1, -0, 0));
        public static readonly AnimationCurve InBounce = new AnimationCurve(new Keyframe(0, 0, 0, 0), new Keyframe(0.15f, -.15f, 0, 0), new Keyframe(1, 1, 0, 0));
        public static readonly AnimationCurve OutBounce = new AnimationCurve(new Keyframe(0, 0, 0, 0), new Keyframe(0.6f, 1.1f, 0, 0), new Keyframe(1, 1, 2, 0));
        public static readonly AnimationCurve PingPong = new AnimationCurve(new Keyframe(0, 0, 0, 0), new Keyframe(.5f, 1, 0, 0), new Keyframe(1f, 0, 0, 0));
        public static readonly AnimationCurve NumberCount = new AnimationCurve(new Keyframe(0, 0, 0, 0), new Keyframe(.25f, -0.15f, 0, 0), new Keyframe(1f, 1f, 0, 0));
        
        public static readonly AnimationCurve Bobble =  new AnimationCurve(
                                                        new Keyframe(0, 1, 0, 0),
                                                        new Keyframe(.2f, 1.2f, 0, 0),
                                                        new Keyframe(.4f, .9f, 0, 0),
                                                        new Keyframe(.6f, 1.05f, 0, 0),
                                                        new Keyframe(.8f, .98f, 0, 0),
                                                        new Keyframe(1, 1, 0, 0));

        public static readonly AnimationCurve IdleBounceDelay = new AnimationCurve(
                                                                new Keyframe(0, 0, 0, 0),
                                                                new Keyframe(0.15f, 1f, 0, 0),
                                                                new Keyframe(0.3f, 0, 0, 0),
                                                                new Keyframe(0.45f, 1f, 0, 0),
                                                                new Keyframe(0.6f, 0, 0, 0),
                                                                new Keyframe(1, 0, 0, 0));
        public static Vector2 GetBrizerRecusive(float smoothTime, params Vector2[] positions)
        {
            if (positions.Length <= 1)
            {
                return positions[0];
            }

            Vector2[] vectors = new Vector2[positions.Length - 1];
            for (int i = 1; i < positions.Length; i++)
            {
                vectors[i - 1] = GetBrizerPos(smoothTime, positions[i - 1], positions[i]);
            }
            return GetBrizerRecusive(smoothTime, vectors);
        }

        private static Vector2 GetBrizerPos(float smoothTime, Vector2 pos1, Vector2 pos2)
        {
            return Vector2.Lerp(pos1, pos2, smoothTime);
        }

    }
}