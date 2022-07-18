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
            shape = new GameObject().AddComponent<Shape>();
            outline = new GameObject().AddComponent<ShapeOutline>();
            outline.transform.parent = shape.transform;
        }

        [TearDown]
        public void AfterEveryTest()
        {
            
        }
        
        [UnityTest]
        public IEnumerator WhenShapeIsSelectedThenOutlineIsActive()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            // Assign

            // Act
            shape.Select();

            yield return null;
            // Assert
            Assert.AreEqual(expected: true, actual: shape.Outline.IsActive());
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
            Assert.AreEqual(expected: false, actual: shape.Outline.IsActive());
        }
    }
}
