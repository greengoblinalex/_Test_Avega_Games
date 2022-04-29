using System;
using UnityEngine;

namespace _GAME.Common
{
    public class ObjectTapCatcher : MonoBehaviour
    {
        public event Action OnEnter;
        public event Action OnExit;
        public event Action OnDown;
        public event Action OnUp;
        public event Action OnClick;

        private void OnMouseDown() => OnDown?.Invoke();
        private void OnMouseUp() => OnUp?.Invoke();
        private void OnMouseEnter() => OnEnter?.Invoke();
        private void OnMouseExit() => OnExit?.Invoke();
        private void OnMouseUpAsButton() => OnClick?.Invoke();
    }
}