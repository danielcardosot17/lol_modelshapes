using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace CalangoGames
{
    [CustomEditor(typeof(GameEventSO), editorForChildClasses: true)]
    public class EventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            GameEventSO e = target as GameEventSO;
            if (GUILayout.Button("Raise"))
                e.Raise();
        }
    }
}
