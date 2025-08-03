using UnityEngine;

namespace _BaseFeatures.DOAnimations.Basic
{
    public class SimpleSpriteColor : BaseAnimation
    {
        private readonly SpriteRenderer m_render;
        private readonly Color startColor;
        private readonly Color endColor;
        private Color dummyColor;
        public SimpleSpriteColor(SpriteRenderer sprRender, Color start, Color end)
        {
            this.m_render = sprRender;
            this.startColor = start;
            this.endColor = end;
        }
        protected override int GetInstanceID() => m_render.gameObject.GetInstanceID();
        protected override void OnUpdate(float evaluate)
        {
            dummyColor.r = startColor.r + (endColor.r - startColor.r) * evaluate;
            dummyColor.g = startColor.g + (endColor.g - startColor.g) * evaluate;
            dummyColor.b = startColor.b + (endColor.b - startColor.b) * evaluate;
            dummyColor.a = startColor.a + (endColor.a - startColor.a) * evaluate;
            m_render.color = dummyColor;
        }
    }
}
