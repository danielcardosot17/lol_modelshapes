using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ShapeOutline : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
            Disable();
        }

        public void Disable()
        {
            spriteRenderer.enabled = false;
        }

        public void Enable()
        {
            spriteRenderer.enabled = true;
        }

        public bool IsOn()
        {
            return spriteRenderer.enabled;
        }
    }
}
