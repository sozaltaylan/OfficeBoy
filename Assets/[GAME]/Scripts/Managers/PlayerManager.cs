using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OfficeFever.Exceptions;
using TaylanGame.Controllers;

namespace OfficeFever.Managers
{
    public class PlayerManager : MonoSingleton<PlayerManager>
    {
        #region Variables

        [SerializeField] private PlayerController playerController;

        #endregion

        #region Methods

        public PlayerController GetPlayer()
        {
            return playerController;
        }
        #endregion
    }
}