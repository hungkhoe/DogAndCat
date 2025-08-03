using UnityEngine;
using UnityEngine.UI;

namespace _BaseFeatures.DOAnimations.Basic
{
    public class ImageSheetAnimation : BaseAnimation
    {
        private readonly Image m_image;
        private readonly Sprite[] sprites;
        private readonly float intervalPerFrame;
        private int frame;
        public ImageSheetAnimation(Image m_image, Sprite[] sprites)
        {
            this.m_image = m_image;
            this.sprites = sprites;
            intervalPerFrame = 1f / (sprites.Length);
        }
        protected override int GetInstanceID() => m_image.gameObject.GetInstanceID();
        protected override void OnUpdate(float evaluate)
        {
            frame = ((int)(evaluate / intervalPerFrame));
            if (frame > sprites.Length - 1)
                frame = sprites.Length - 1;
            m_image.sprite = sprites[frame];
        }
    }
}
