using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Collider2D))]
    public class ShapeSlot : MonoBehaviour
    {
        [SerializeField] private ShapeType shapeType;
        private bool isOccupied = false;
        public bool IsOccupied { get => isOccupied; }

        public ShapeType ShapeType { get => shapeType; set => shapeType = value; }

        private GameEventSO occupyEvent;
        public GameEventSO OccupyEvent { get => occupyEvent; set => occupyEvent = value; }


        public void Occupy()
        {
            isOccupied = true;
            occupyEvent.Raise();
        }
    }
}
