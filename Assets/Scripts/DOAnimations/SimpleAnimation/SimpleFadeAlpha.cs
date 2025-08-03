using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _BaseFeatures.DOAnimations.Basic
{
    public class SimpleFadeAlpha : BaseAnimation
    {
        private readonly CanvasGroup grpCanvas;
        private readonly float start;
        private readonly float end;
        public SimpleFadeAlpha(CanvasGroup grpCanvas, float start, float end)
        {
            this.grpCanvas = grpCanvas;
            this.start = start;
            this.end = end;
        }
        protected override int GetInstanceID() => grpCanvas.gameObject.GetInstanceID();
        protected override void OnUpdate(float evaluate) => grpCanvas.alpha = start + (end - start) * evaluate;
    }
}
