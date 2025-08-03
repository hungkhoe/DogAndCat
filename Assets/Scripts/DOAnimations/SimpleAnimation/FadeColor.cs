using UnityEngine;
using UnityEngine.Events;

namespace _BaseFeatures.DOAnimations.Basic
{
    public class FadeColor : BaseAnimation
    {
        private readonly int instanceID;
        private Color startColor;
        private Color endColor;
        private Color dummyColor;
        private UnityAction<Color> UpdateCallback;
        
        public FadeColor(int instanceID, Color startColor, Color endColor)
        {
            this.instanceID = instanceID;
            this.startColor = startColor;
            this.endColor = endColor;
        }
        
        public FadeColor SetUpdateCallback(UnityAction<Color> UpdateCallback)
        {
            this.UpdateCallback = UpdateCallback;
            return this;
        }
        protected override void OnUpdate(float evaluate)
        {
            dummyColor.r = startColor.r + (endColor.r - startColor.r) * evaluate;
            dummyColor.g = startColor.g + (endColor.g - startColor.g) * evaluate;
            dummyColor.b = startColor.b + (endColor.b - startColor.b) * evaluate;
            dummyColor.a = startColor.a + (endColor.a - startColor.a) * evaluate;
            UpdateCallback?.Invoke(dummyColor);
        }

        protected override int GetInstanceID()
        {
            return instanceID;
        }
    }
}