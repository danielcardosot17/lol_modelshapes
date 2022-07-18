using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CalangoGames.Tests
{
    public class MoveShapeTests
    {
        Player player;
        Shape shape;
        
        [SetUp]
        public void BeforeEveryTest()
        {
            player = new GameObject().AddComponent<Player>();
            shape = new GameObject().AddComponent<Shape>();
        }

        [TearDown]
        public void AfterEveryTest()
        {
            
        }
        

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator MovesToSelectedPosition()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            // Assign
            
            // Act
            shape.Move(Vector3.up);

            yield return null;
            // Assert
            Assert.AreEqual(expected: Vector3.up, actual: shape.transform.position);

        }
    }
}
