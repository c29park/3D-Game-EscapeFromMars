using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.FPS.Game;

namespace Unity.FPS.Gameplay
{
    public class FuelPortal : Pickup
    {
        protected override void OnPicked(PlayerCharacterController byPlayer)
        {
            PlayPickupFeedback();
            Destroy(gameObject);

            ReturnToSpaceShipEvent evt = Events.ReturnToSpaceShipEvent;
            EventManager.Broadcast(evt);
        }
    }
}
