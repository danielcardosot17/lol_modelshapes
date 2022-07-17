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
            shape = new GameObject().AddComponent<Shape>();
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
            // Act
            player.DeselectShape(shape);

            // Assert
            Assert.AreEqual(expected: false, actual: shape.IsSelected);
        }
    }
}
