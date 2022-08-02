using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CalangoGames.Tests
{
    public class GameFlowTests
    {
        LevelManager levelManager;
        Shape shape;
        Shape otherShape;
        ShapeSlot shapeSlot;
        ShapeSlot otherShapeSlot;
        
        [SetUp]
        public void BeforeEveryTest()
        {
            var obj = new GameObject();
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
