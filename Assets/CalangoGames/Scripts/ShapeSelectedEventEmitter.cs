using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public class ShapeSelectedEventEmitter : MonoBehaviour
    {
        [SerializeField] private GameEventSO eventToEmit;
        // Start is called before the first frame update
        void Start()
        {
            var shape = gameObject.GetComponent<Shape>();
            shape.SelectedEvent = eventToEmit;
        }
    }
}
