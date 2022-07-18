using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CalangoGames.Tests
{
    public class OutlineTests
    {
        Player player;
        Shape shape;
        ShapeOutline outline;
        
        [SetUp]
        public void BeforeEveryTest()
        {
            player = new GameObject().AddComponent<Player>();
            
            var obj = new GameObject();
            obj.AddComponent<SpriteRenderer>();
            obj.AddComponent<BoxCollider2D>();
            shape = obj.AddComponent<Shape>();

            var obj2 = new GameObject();
            obj2.AddComponent<SpriteRenderer>();
            obj2.AddComponent<BoxCollider2D>();
            outline = obj2.AddComponent<ShapeOutline>();
            outline.transform.parent = shape.transform;
        }

        [TearDown]
        public void AfterEveryTest()
        {
            
        }
        
        [UnityTest]
        public IEnumerator WhenStartGameThenOutlineIsNotActive()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            // Assign

            // Act

            yield return null;
            // Assert
            Assert.AreEqual(expected: false, actual: shape.Outline.IsOn());
        }

        [UnityTest]
        public IEnumerator WhenShapeIsSelectedThenOutlineIsEnabled()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            // Assign

            // Act
            shape.Select();

            yield return null;
            // Assert
            Assert.AreEqual(expected: true, actual: shape.Outline.IsOn());
        }

        [UnityTest]
        public IEnumerator WhenShapeIsDeselectedThenOutlineIsDisabled()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            // Assign
            shape.Outline.Enable();

            // Act
            shape.Deselect();

            yield return null;
            // Assert
            Assert.AreEqual(expected: false, actual: shape.Outline.IsOn());
        }
    }
}
