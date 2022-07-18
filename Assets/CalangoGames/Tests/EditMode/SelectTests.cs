using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CalangoGames.Tests
{
    public class SelectTests
    {
        Player player;
        Shape shape;
        
        [SetUp]
        public void BeforeEveryTest()
        {
            player = new GameObject().AddComponent<Player>();
            var obj = new GameObject();
            obj.AddComponent<SpriteRenderer>();
            obj.AddComponent<BoxCollider2D>();
            shape = obj.AddComponent<Shape>();
        }

        [TearDown]
        public void AfterEveryTest()
        {
            
        }

        [Test]
        public void WhenPlayerSelectShapeThenShapeIsSelected()
        {
            // Assign
            // Act
            player.SelectShape(shape);

            // Assert
            Assert.AreEqual(expected: true, actual: shape.IsSelected);
        }

        [Test]
        public void WhenPlayerDeselectShapeThenShapeIsDeselected()
        {
            // Assign
            player.SelectShape(shape);
            
            // Act
            player.DeselectShape(shape);

            // Assert
            Assert.AreEqual(expected: false, actual: shape.IsSelected);
        }
        
        [Test]
        public void WhenPlayerSelectDifferentShapeThenFirstShapeIsDeselected()
        {
            // Assign
            player.SelectShape(shape);

            // Act
            var obj = new GameObject();
            obj.AddComponent<SpriteRenderer>();
            obj.AddComponent<BoxCollider2D>();
            var newShape  = obj.AddComponent<Shape>();
            
            player.SelectShape(newShape);

            // Assert
            Assert.AreEqual(expected: false, actual: shape.IsSelected);
        }

        [Test]
        public void WhenPlayerSelectUnselectableShapeThenShapeIsNotSelected()
        {
            // Assign
            shape.SetNotSelectable();

            // Act
            player.SelectShape(shape);

            // Assert
            Assert.AreEqual(expected: false, actual: shape.IsSelected);
        }
    }
}
