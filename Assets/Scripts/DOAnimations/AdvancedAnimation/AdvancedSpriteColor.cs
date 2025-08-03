using JetBrains.Annotations;
using UnityEngine;

namespace _BaseFeatures.DOAnimations.Advanced
{
    public class AdvancedSpriteColor : AdvancedAnimation<Color>
    {
        protected SpriteRenderer m_render;
        private Color dummyColor;
        public AdvancedSpriteColor(KeyNode<Color>[] keyFrames, SpriteRenderer sprRender) : base(keyFrames)
        {
            this.m_render = sprRender;
        }
        protected override int GetInstanceID() => m_render.gameObject.GetInstanceID();
        protected override void OnUpdate(float evaluate, Color startColor, Color endColor)
        {
            dummyColor.r = startColor.r + (endColor.r - startColor.r) * evaluate;
            dummyColor.g = startColor.g + (endColor.g - startColor.g) * evaluate;
            dummyColor.b = startColor.b + (endColor.b - startColor.b) * evaluate;
            dummyColor.a = startColor.a + (endColor.a - startColor.a) * evaluate;
            m_render.color = dummyColor;
        }

        protected override void OnUpdate(float evaluate)
        {
            
        }
    }
}
