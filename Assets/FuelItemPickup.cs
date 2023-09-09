using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class FuelItemPickup : Pickup
    {

        protected override void OnPicked(PlayerCharacterController player)
        {
            player.IncFuelCount();

            FuelCollectEvent evt = Events.FuelCollectEvent;
            EventManager.Broadcast(evt);

            PlayPickupFeedback();
            Destroy(gameObject);

            /*
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth && playerHealth.CanPickup())
            {
                playerHealth.Heal(HealAmount);
                PlayPickupFeedback();
                Destroy(gameObject);
            }*/
        }
    }
}
