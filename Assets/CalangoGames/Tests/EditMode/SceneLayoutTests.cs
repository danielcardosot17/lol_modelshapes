using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
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
            List<Player> playersInScene = new List<Player>();

            foreach (Player player in Resources.FindObjectsOfTypeAll(typeof(Player)) as Player[])
            {
                if (!EditorUtility.IsPersistent(player.transform.root.gameObject) && !(player.hideFlags == HideFlags.NotEditable || player.hideFlags == HideFlags.HideAndDontSave))
                    playersInScene.Add(player);
            }
            
            Assert.AreEqual(expected: 1, actual: playersInScene.Count);
        }

        [Test]
        public void ThereIsOnlyOneAudioManagerInScene()
        {
            var audioManagers = Resources.FindObjectsOfTypeAll<AudioManager>();
            
            Assert.AreEqual(expected: 1, actual: audioManagers.Length);
        }

        [Test]
        public void ThereIsOnlyOneGameMasterInScene()
        {
            var gameMasters = Resources.FindObjectsOfTypeAll<GameMaster>();
            
            Assert.AreEqual(expected: 1, actual: gameMasters.Length);
        }

        [Test]
        public void ThereIsAtLeastOneShapeInScene()
        {
            var shapes = Resources.FindObjectsOfTypeAll<Shape>();
            
            Assert.AreEqual(expected: 1, actual: shapes.Length);
        }

        [Test]
        public void ThereIsSameNumberOfShapesAndSlotsInScene()
        {
            var shapes = Resources.FindObjectsOfTypeAll<Shape>();
            var shapeSlot = Resources.FindObjectsOfTypeAll<ShapeSlot>();
            
            Assert.AreEqual(expected: shapes.Length, actual: shapeSlot.Length);
        }
    }
}
