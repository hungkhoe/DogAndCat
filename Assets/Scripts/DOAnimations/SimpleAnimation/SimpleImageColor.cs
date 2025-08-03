using UnityEngine;
using UnityEngine.UI;

namespace _BaseFeatures.DOAnimations.Basic
{
    public class SimpleImageColor : BaseAnimation
    {
        private readonly Image m_image;
        private readonly Color startColor;
        private readonly Color endColor;
        private Color dummyColor;
        public SimpleImageColor(Image img, Color start, Color end)
        {
            this.m_image = img;
            this.startColor = start;
            this.endColor = end;
        }
        protected override int GetInstanceID() => m_image.gameObject.GetInstanceID();
        protected override void OnUpdate(float evaluate)
        {
            dummyColor.r = startColor.r + (endColor.r - startColor.r) * evaluate;
            dummyColor.g = startColor.g + (endColor.g - startColor.g) * evaluate;
            dummyColor.b = startColor.b + (endColor.b - startColor.b) * evaluate;
            dummyColor.a = startColor.a + (endColor.a - startColor.a) * evaluate;
            m_image.color = dummyColor;
        }
    }
}
