using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Unity.Template.VR
{
    public class GameInfo : MonoBehaviour
    {
        public static GameInfo I;

        private bool _paused = false;
        public bool Paused => _paused;

        public InputAction ConnectAction;
        public InputAction RotateAction;
        public InputAction MoveAction;
        public InputAction MoveDistAxisAction;
        public InputAction PauseAction;
        public InputAction DeleteAction;
        public InputAction UIClickAction;
        public InputAction ConfigureAction;

        public void Pause()
        {
            _paused = true;
            Time.timeScale = 0f;
        }

        public void Unpause()
        {
            _paused = false;
            Time.timeScale = 1f;
        }

        public void TogglePause()
        {
            if (Paused)
                Unpause();
            else
                Pause();
        }

        public void Awake()
        {
            ConnectAction.Enable();
            PauseAction.Enable();
            MoveAction.Enable();
            RotateAction.Enable();
            MoveDistAxisAction.Enable();
            DeleteAction.Enable();
            UIClickAction.Enable();
            ConfigureAction.Enable();

            PauseAction.started += _ => TogglePause();
            
            I = this;
        }
    }
}