using UnityEngine;

namespace _BaseFeatures.DOAnimations.Basic
{
    public class SpriteSheetAnimation : BaseAnimation
    {
        private readonly SpriteRenderer m_render;
        private readonly Sprite[] sprites;
        private readonly float intervalPerFrame;
        private int frame;
        public SpriteSheetAnimation(SpriteRenderer m_render, Sprite[] sprites)
        {
            this.m_render = m_render;
            this.sprites = sprites;
            intervalPerFrame = 1f / (sprites.Length);
        }
        protected override int GetInstanceID() => m_render.gameObject.GetInstanceID();
        protected override void OnUpdate(float evaluate)
        {
            frame = ((int)(evaluate / intervalPerFrame));
            if (frame > sprites.Length - 1)
                frame = sprites.Length - 1;
            m_render.sprite = sprites[frame];
        }
    }
}
