using OfficeFever.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

namespace TaylanGame.Controllers
{

    public class InputController : MonoBehaviour

    {
        #region Variables

        private Vector3 _mousefirstPosition;
        private Vector3 _mouseHoldPosition;
        private Vector3 _remoteControl;
        private bool isInput;

        #endregion
        #region Properties
        public Vector3 remoteControl => _remoteControl;
        public bool IsInput => isInput;
        #endregion
        [SerializeField] private float _inputSpeed;

     
        #region Methods

        private void Update()
        {
            SetInput();
        }

        private void SetInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetFirstClick();
            }
            else if (Input.GetMouseButton(0))
            {

                GetHoldDown();
            }
            else if(Input.GetMouseButtonUp(0))
            {
                GetMouseUp();
            }
 
        }

        private void GetHoldDown()
        {
            _mouseHoldPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            var dist = (_mouseHoldPosition - _mousefirstPosition) * _inputSpeed;
            _remoteControl = dist;
            isInput = true;
        }

        private void GetFirstClick()
        {
            _mousefirstPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        private void GetMouseUp()
        {
            isInput = false;
        }

    }
    #endregion
}