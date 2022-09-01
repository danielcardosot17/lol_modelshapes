using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalangoGames
{
    public interface IAnimationManager
    {
        void ShowFinishedShape();
        void StartShapeAnimation();
        void StartBackgroundAnimation();
        void MoveCamera();
        void StartSFX();
        float GetAnimationDuration();
    }
}
