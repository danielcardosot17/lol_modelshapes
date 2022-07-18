using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CalangoGames.Tests
{
    public class ShapeSlotTests
    {
        Player player;
        Shape shape;
        ShapeSlot shapeSlot;
        
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
            shapeSlot = obj2.AddComponent<ShapeSlot>();
        }

        [TearDown]
        public void AfterEveryTest()
        {
            
        }

        // A Test behaves as an ordinary method
        [Test]
        public void ShapeSlotTestsSimplePasses()
        {
            // Use the Assert class to test conditions
        }
    }
}
