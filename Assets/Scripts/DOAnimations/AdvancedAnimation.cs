using UnityEngine;
using _BaseFeatures.DOAnimations.Basic;

namespace _BaseFeatures.DOAnimations.Advanced
{
   
    public abstract class AdvancedAnimation<T> : BaseAnimation
    {
        protected KeyNode<T>[] keyFrames;
        private int frame = 0;
        private float dummyEvaluate;
        public AdvancedAnimation(KeyNode<T>[] keyFrames)
        {
            this.keyFrames = keyFrames;
        }
        protected override void UpdateOnBehaviour()
        {
            if (elapsedTime < 1)
            {
                elapsedTime += (unscaleTime ? Time.unscaledDeltaTime : Time.deltaTime) * 1 / duration;
                if (elapsedTime < 0)
                    return;
                Trigger();

                if (elapsedTime >= keyFrames[frame].time && frame < keyFrames.Length - 1)
                {
                    frame++;
                }

                dummyEvaluate = (curve.Evaluate(elapsedTime) - curve.Evaluate(keyFrames[frame - 1].time)) / (curve.Evaluate(keyFrames[frame].time) - curve.Evaluate(keyFrames[frame - 1].time));
                OnUpdate(dummyEvaluate, keyFrames[frame - 1].value, keyFrames[frame].value);
            }
            else if (loop < 0)
            {
                frame = 0;
                elapsedTime = 0;
            }
            else if (loop > 0)
            {
                frame = 0;
                elapsedTime = 0;
                loop--;
            }
            else if (!isCompleted)
            {
                Stop();
                OnCompleteMethod?.Invoke();
            }
        }
        protected abstract void OnUpdate(float evaluate, T startValue, T endValue);
        public override BaseAnimation Play()
        {
            frame = 0;
            OnUpdate(0, keyFrames[frame].value, keyFrames[frame].value);
            return base.Play();
        }
    }
}

