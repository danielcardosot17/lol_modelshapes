using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CalangoGames.Tests
{
    public class SceneLayoutTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void ThereIsOnlyOnePlayerInScene()
        {
            var players = Resources.FindObjectsOfTypeAll<Player>();
            
            Assert.AreEqual(expected: 1, actual: players.Length);
        }

        // [Test]
        // public void ThereIsOnlyOneAudioManagerInScene()
        // {
            
        // }

        // [Test]
        // public void ThereIsOnlyOneGameMasterInScene()
        // {
            
        // }
    }
}
