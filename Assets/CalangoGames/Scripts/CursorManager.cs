using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class CursorManager : MonoBehaviour
    {
        [SerializeField] private Texture2D pointer;

        private void Awake()
        {
            ChangeCursor(pointer);
        }

        private void ChangeCursor(Texture2D pointer)
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}
