using System;
using UnityEngine;

namespace Features.Player
{
    public abstract class BasePlayerInput : MonoBehaviour
    {
        protected MazeControls Controls;

        protected virtual void Awake()
        {
            Controls = new MazeControls();
        }
    }
}