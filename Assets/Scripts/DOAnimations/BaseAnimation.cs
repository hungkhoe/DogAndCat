using UnityEngine;
using UnityEngine.Events;

namespace _BaseFeatures.DOAnimations.Basic
{
    public abstract class BaseAnimation
    {
        protected AnimationCurve curve = PresetCurves.InOutEase;
        protected float duration = 1;
        protected float speed = 1;
        protected float delay = 0;
        protected int loop = 0;
        protected UnityAction OnCompleteMethod;
        protected UnityAction OnActionByFrame;
        protected TriggerEvent[] OnTriggerMethods;
        protected float elapsedTime;
        protected bool isCompleted = true;
        protected bool unscaleTime = false;
        public bool IsPlaying => !isCompleted;
        public BaseAnimation SetCurve(AnimationCurve curve)
        {
            this.curve = curve;
            return this;
        }
        public BaseAnimation SetDuration(float duration)
        {
            this.duration = duration;
            return this;
        }
        public BaseAnimation SetSpeed(float speed)
        {
            this.speed = speed;
            return this;
        }
        public BaseAnimation SetDelay(float delay)
        {
            this.delay = delay;
            return this;
        }
        public BaseAnimation SetLoop(int loop)
        {
            this.loop = loop;
            return this;
        }
        public BaseAnimation SetUnscaleTime()
        {
            this.unscaleTime = true;
            return this;
        }
        public BaseAnimation SetDontDestroy()
        {
            AnimationBehaviour.Instance.SetDontDestroy(GetInstanceID());
            return this;
        }
        public BaseAnimation OnComplete(UnityAction onCompleted)
        {
            this.OnCompleteMethod = onCompleted;
            return this;
        }
        public BaseAnimation OnEventTrigger(params TriggerEvent[] _triggerEvents)
        {
            this.OnTriggerMethods = _triggerEvents;
            return this;
        }
        public BaseAnimation OnActionFrame(UnityAction OnAction)
        {
            this.OnActionByFrame = OnAction;
            return this;
        }
        public void Trigger()
        {
            if (OnTriggerMethods != null)
            {
                for (int i = 0; i < OnTriggerMethods.Length; i++)
                {
                    if (OnTriggerMethods[i] == null)
                        continue;
                    if (elapsedTime > OnTriggerMethods[i].timeEvent)
                    {
                        OnTriggerMethods[i].triggerAction?.Invoke();
                        OnTriggerMethods[i] = null;
                    }
                }
            }
        }
#if DEBUG_BASE_FEATURES
        ~BaseAnimation()
        {
            Debug.Log("~BaseAnimation");
        }
#endif
        protected virtual void UpdateOnBehaviour()
        {
            switch (elapsedTime)
            {
                case < 0:
                    elapsedTime += (unscaleTime ? Time.unscaledDeltaTime : Time.deltaTime) * speed / duration;
                    // OnUpdate(curve.Evaluate(0));
                    break;
                case < 1:
                    elapsedTime += (unscaleTime ? Time.unscaledDeltaTime : Time.deltaTime) * speed / duration;
                    Trigger();
                    OnUpdate(curve.Evaluate(elapsedTime));
                    break;
                default:
                {
                    switch (loop)
                    {
                        case < 0:
                            elapsedTime = 0;
                            OnCompleteMethod?.Invoke();
                            break;
                        case > 0:
                            elapsedTime = 0;
                            loop--;
                            break;
                        default:
                        {
                            if (!isCompleted)
                            {
                                Stop();
                                OnCompleteMethod?.Invoke();
                            }

                            break;
                        }
                    }

                    break;
                }
            }
        }

        public BaseAnimation UpdateManual(float time)
        {
            OnUpdate(curve.Evaluate(time));
            return this;
        }
        protected abstract void OnUpdate(float evaluate);
        protected abstract int GetInstanceID();
        public void Stop()
        {
            isCompleted = true;
            AnimationBehaviour.Instance.CleanUp(GetInstanceID());
        }
        public virtual BaseAnimation Play()
        {
            isCompleted = false;
            elapsedTime = -delay;
            // UpdateManual(0);
            AnimationBehaviour.Instance.SetListener(GetInstanceID(), this);
            AnimationBehaviour.Instance.UpdateEvent += UpdateOnBehaviour;
            return this;
        }
        public void ReleaseUpdateOnBehaviour()
        {
            AnimationBehaviour.Instance.UpdateEvent -= UpdateOnBehaviour;
        }
    }
}
