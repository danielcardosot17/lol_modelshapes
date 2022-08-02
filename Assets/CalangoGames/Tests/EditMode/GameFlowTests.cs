using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TestTools;

namespace CalangoGames.Tests
{
    public class GameFlowTests
    {
        LevelManager levelManager;
        Level level;
        Shape shape;
        Shape otherShape;
        ShapeSlot shapeSlot;
        ShapeSlot otherShapeSlot;
        GameEventSO occupyEvent;
        
        [SetUp]
        public void BeforeEveryTest()
        {
            occupyEvent = ScriptableObject.CreateInstance<GameEventSO>();
            level = new Level();
            var obj = new GameObject();
            levelManager = obj.AddComponent<LevelManager>();
            levelManager.CurrentLevel = level;

            obj.AddComponent<SpriteRenderer>();
            obj.AddComponent<BoxCollider2D>();
            shape = obj.AddComponent<Shape>();
            shape.ShapeType = ShapeType.Clay;

            var obj2 = new GameObject();
            obj2.AddComponent<SpriteRenderer>();
            obj2.AddComponent<BoxCollider2D>();
            shapeSlot = obj2.AddComponent<ShapeSlot>();
            shapeSlot.ShapeType = ShapeType.Clay;

            var obj3 = new GameObject();
            obj3.AddComponent<SpriteRenderer>();
            obj3.AddComponent<BoxCollider2D>();
            otherShape = obj3.AddComponent<Shape>();
            otherShape.ShapeType = ShapeType.Stick0;
            
            var obj4 = new GameObject();
            obj4.AddComponent<SpriteRenderer>();
            obj4.AddComponent<BoxCollider2D>();
            otherShapeSlot = obj4.AddComponent<ShapeSlot>();
            otherShapeSlot.ShapeType = ShapeType.Stick0;

            levelManager.ShapesInScene = new List<Shape>();
            levelManager.ShapesInScene.Add(shape);
            levelManager.ShapesInScene.Add(otherShape);
            levelManager.NumberOfShapes += 2;

            levelManager.SlotsInScene = new List<ShapeSlot>();
            levelManager.SlotsInScene.Add(shapeSlot);
            levelManager.SlotsInScene.Add(otherShapeSlot);
            levelManager.NumberOfSlots += 2;

            levelManager.occupyEvent = occupyEvent;
            shapeSlot.OccupyEvent = occupyEvent;
            otherShapeSlot.OccupyEvent = occupyEvent;
            var eventListener = obj.AddComponent<GameEventListener>();
            eventListener.Response = new UnityEvent();
            eventListener.Response.AddListener(levelManager.UpdateOccupiedSlots);
            occupyEvent.RegisterListener(eventListener);
        }

        [TearDown]
        public void AfterEveryTest()
        {
            
        }
        
        // A Test behaves as an ordinary method
        [Test]
        public void WhenAllSlotsAreOccupiedThenLevelIsFinished()
        {
            // Assign
            // Act
            shapeSlot.Occupy();
            otherShapeSlot.Occupy();
            // Assert
            Assert.AreEqual(expected: true, actual: levelManager.CurrentLevel.IsFinished);
        }
    }
}
