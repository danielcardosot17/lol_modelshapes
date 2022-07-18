using System;
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
            List<AudioManager> audioManagersInScene = new List<AudioManager>();

            foreach (AudioManager audioManager in Resources.FindObjectsOfTypeAll(typeof(AudioManager)) as AudioManager[])
            {
                if (!EditorUtility.IsPersistent(audioManager.transform.root.gameObject) && !(audioManager.hideFlags == HideFlags.NotEditable || audioManager.hideFlags == HideFlags.HideAndDontSave))
                    audioManagersInScene.Add(audioManager);
            }
            
            Assert.AreEqual(expected: 1, actual: audioManagersInScene.Count);
        }

        [Test]
        public void ThereIsOnlyOneGameMasterInScene()
        {
            List<GameMaster> gameMastersInScene = new List<GameMaster>();

            foreach (GameMaster gameMaster in Resources.FindObjectsOfTypeAll(typeof(GameMaster)) as GameMaster[])
            {
                if (!EditorUtility.IsPersistent(gameMaster.transform.root.gameObject) && !(gameMaster.hideFlags == HideFlags.NotEditable || gameMaster.hideFlags == HideFlags.HideAndDontSave))
                    gameMastersInScene.Add(gameMaster);
            }
            
            Assert.AreEqual(expected: 1, actual: gameMastersInScene.Count);
        }

        [Test]
        public void ThereIsAtLeastOneShapeInScene()
        {

            List<Shape> shapesInScene = new List<Shape>();

            foreach (Shape shape in Resources.FindObjectsOfTypeAll(typeof(Shape)) as Shape[])
            {
                if (!EditorUtility.IsPersistent(shape.transform.root.gameObject) && !(shape.hideFlags == HideFlags.NotEditable || shape.hideFlags == HideFlags.HideAndDontSave))
                    shapesInScene.Add(shape);
            }
            
            Assert.Greater(shapesInScene.Count, 0);
        }

        [Test]
        public void ThereIsSameNumberOfShapesAndSlotsInScene()
        {
            List<Shape> shapesInScene = new List<Shape>();

            foreach (Shape shape in Resources.FindObjectsOfTypeAll(typeof(Shape)) as Shape[])
            {
                if (!EditorUtility.IsPersistent(shape.transform.root.gameObject) && !(shape.hideFlags == HideFlags.NotEditable || shape.hideFlags == HideFlags.HideAndDontSave))
                    shapesInScene.Add(shape);
            }
            
            List<ShapeSlot> shapeSlotInScene = new List<ShapeSlot>();

            foreach (ShapeSlot shapeSlot in Resources.FindObjectsOfTypeAll(typeof(ShapeSlot)) as ShapeSlot[])
            {
                if (!EditorUtility.IsPersistent(shapeSlot.transform.root.gameObject) && !(shapeSlot.hideFlags == HideFlags.NotEditable || shapeSlot.hideFlags == HideFlags.HideAndDontSave))
                    shapeSlotInScene.Add(shapeSlot);
            }
            Assert.AreEqual(expected: shapesInScene.Count, actual: shapeSlotInScene.Count);
        }

        [Test]
        public void ThereIsSameNumberOfShapesAndOutlinesInScene()
        {
            List<Shape> shapesInScene = new List<Shape>();

            foreach (Shape shape in Resources.FindObjectsOfTypeAll(typeof(Shape)) as Shape[])
            {
                if (!EditorUtility.IsPersistent(shape.transform.root.gameObject) && !(shape.hideFlags == HideFlags.NotEditable || shape.hideFlags == HideFlags.HideAndDontSave))
                    shapesInScene.Add(shape);
            }
            
            List<ShapeOutline> shapeOutlinesInScene = new List<ShapeOutline>();

            foreach (ShapeOutline shapeOutline in Resources.FindObjectsOfTypeAll(typeof(ShapeOutline)) as ShapeOutline[])
            {
                if (!EditorUtility.IsPersistent(shapeOutline.transform.root.gameObject) && !(shapeOutline.hideFlags == HideFlags.NotEditable || shapeOutline.hideFlags == HideFlags.HideAndDontSave))
                    shapeOutlinesInScene.Add(shapeOutline);
            }
            Assert.AreEqual(expected: shapesInScene.Count, actual: shapeOutlinesInScene.Count);
        }

        [Test]
        public void ThereIsSameNumberOfShapesAndSlotsOfSameTypeInScene()
        {
            List<Shape> shapesInScene = new List<Shape>();

            foreach (Shape shape in Resources.FindObjectsOfTypeAll(typeof(Shape)) as Shape[])
            {
                if (!EditorUtility.IsPersistent(shape.transform.root.gameObject) && !(shape.hideFlags == HideFlags.NotEditable || shape.hideFlags == HideFlags.HideAndDontSave))
                    shapesInScene.Add(shape);
            }
            
            List<ShapeSlot> shapeSlotInScene = new List<ShapeSlot>();

            foreach (ShapeSlot shapeSlot in Resources.FindObjectsOfTypeAll(typeof(ShapeSlot)) as ShapeSlot[])
            {
                if (!EditorUtility.IsPersistent(shapeSlot.transform.root.gameObject) && !(shapeSlot.hideFlags == HideFlags.NotEditable || shapeSlot.hideFlags == HideFlags.HideAndDontSave))
                    shapeSlotInScene.Add(shapeSlot);
            }
            var shapeTypes = System.Enum.GetValues(typeof(ShapeType));
            
            Assert.AreEqual(expected: true, actual: IsThereEqualNumberOfTypes(shapeTypes, shapesInScene, shapeSlotInScene));
        }

        private bool IsThereEqualNumberOfTypes(Array shapeTypes, List<Shape> shapesInScene, List<ShapeSlot> shapeSlotInScene)
        {
            foreach(ShapeType shapeType in shapeTypes)
            {
                if(GetNumberOfShapesOfType(shapeType, shapesInScene) != GetNumberOfSlotsOfType(shapeType, shapeSlotInScene))
                {
                    return false;
                }
            }
            return true;
        }

        private int GetNumberOfSlotsOfType(ShapeType shapeType, List<ShapeSlot> shapeSlotInScene)
        {
            int numberOfSlotsOfType = 0;
            foreach(var slot in shapeSlotInScene)
            {
                if(slot.ShapeType == shapeType)
                {
                    numberOfSlotsOfType++;
                }
            }
            return numberOfSlotsOfType;
        }

        private int GetNumberOfShapesOfType(ShapeType shapeType, List<Shape> shapesInScene)
        {
            int numberOfShapesOfType = 0;
            foreach(var shape in shapesInScene)
            {
                if(shape.ShapeType == shapeType)
                {
                    numberOfShapesOfType++;
                }
            }
            return numberOfShapesOfType;
        }
    }
}
