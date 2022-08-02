using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CalangoGames.Tests
{
    public class GameFlowTests
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
        
        // A Test behaves as an ordinary method
        [Test]
        public void GameFlowTestsSimplePasses()
        {
            // Use the Assert class to test conditions
        }
    }
}
