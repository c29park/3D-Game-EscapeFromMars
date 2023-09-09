using Unity.FPS.Game;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class ObjectiveKillEnemies : Objective // ObjectiveCollectAllFuelsAndReturn
    {
        /*
        [Tooltip("Chose whether you need to kill every enemies or only a minimum amount")]
        public bool MustKillAllEnemies = false;*/

        [Tooltip("Chose whether you need to return to space ship to complete mission")]
        public bool MustReturnToSpaceShip = true;

        /*[Tooltip("If MustKillAllEnemies is false, this is the amount of enemy kills required")]
        public int KillsToCompleteObjective = 3;*/
        public int NumFuelsToCollect = 3;
        public bool ReturnedToSpaceShip = false;

        [Tooltip("Start sending notification about remaining fuel when this amount of fuel is left")]
        //public int NotificationEnemiesRemainingThreshold = 3;
        public int NotificationFuelRemainingThreshold = 3;

        // int m_KillTotal;
        public int m_CollectedFuelTotal;

        protected override void Start()
        {
            base.Start();

            // EventManager.AddListener<EnemyKillEvent>(OnEnemyKilled);
            EventManager.AddListener<FuelCollectEvent>(OnFuelCollected);
            EventManager.AddListener<ReturnToSpaceShipEvent>(OnReturnedToSpaceShip);

            // set a title and description specific for this type of objective, if it hasn't one
            if (string.IsNullOrEmpty(Title))
                Title = "Collect all " + NumFuelsToCollect + ((MustReturnToSpaceShip ? "and return to SpaceShip" : ""));

            if (string.IsNullOrEmpty(Description))
                Description = GetUpdatedFuelCount();
        }

 /*        void OnEnemyKilled(EnemyKillEvent evt)
        {
            if (IsCompleted)
                return;

            m_KillTotal++;

            if (MustKillAllEnemies)
                KillsToCompleteObjective = evt.RemainingEnemyCount + m_KillTotal;

            int targetRemaining = MustKillAllEnemies ? evt.RemainingEnemyCount : KillsToCompleteObjective - m_KillTotal;

            // update the objective text according to how many enemies remain to kill
            if (targetRemaining == 0)
            {
                // should not complete
                // CompleteObjective(string.Empty, GetUpdatedCounterAmount(), "Objective complete : " + Title);
            }
            else if (targetRemaining == 1)
            {
                string notificationText = NotificationEnemiesRemainingThreshold >= targetRemaining
                    ? "One enemy left"
                    : string.Empty;
                UpdateObjective(string.Empty, GetUpdatedCounterAmount(), notificationText);
            }
            else
            {
                // create a notification text if needed, if it stays empty, the notification will not be created
                string notificationText = NotificationEnemiesRemainingThreshold >= targetRemaining
                    ? targetRemaining + " enemies to kill left"
                    : string.Empty;

                UpdateObjective(string.Empty, GetUpdatedCounterAmount(), notificationText);
            }
           
        }
    */

        void OnFuelCollected(FuelCollectEvent evt)
        {
            if (IsCompleted)
                return;

            m_CollectedFuelTotal++;

            int fuelRemaining = NumFuelsToCollect - m_CollectedFuelTotal;

            // update the objective text according to how many fuels remain to be collected
            if (fuelRemaining == 0)
            {
                /* if (MustReturnToSpaceShip)
                {
                    if (ReturnedToSpaceShip) */
                        CompleteObjective(string.Empty, GetUpdatedFuelCount(), "Objective complete : " + Title);
                // }
            }
            else
            {
                if (ReturnedToSpaceShip)
                {
                    string notificationText = "Not enough fuel. Collect all fuel and return.";
                    ReturnedToSpaceShip= false;
                    UpdateObjective(string.Empty, GetUpdatedFuelCount(), notificationText);
                    return;
                }

                if (fuelRemaining == 1)
                {
                    string notificationText = NotificationFuelRemainingThreshold >= fuelRemaining
                        ? "One Fuel left"
                     : string.Empty;
                    UpdateObjective(string.Empty, GetUpdatedFuelCount(), notificationText);
                }
                else
                {
                    // create a notification text if needed, if it stays empty, the notification will not be created
                    string notificationText = NotificationFuelRemainingThreshold >= fuelRemaining
                        ? fuelRemaining + " fuel left to collect"
                        : string.Empty;

                    UpdateObjective(string.Empty, GetUpdatedFuelCount(), notificationText);
                }
            }
        }

        void OnReturnedToSpaceShip(ReturnToSpaceShipEvent evt)
        {
            Debug.Log("OnReturn to Spaceship");

            if (!ReturnedToSpaceShip)
                ReturnedToSpaceShip = true;

            int fuelRemaining = NumFuelsToCollect - m_CollectedFuelTotal;

            // update the objective text according to how many fuels remain to be collected
            if (fuelRemaining == 0)
            {
                if (MustReturnToSpaceShip)
                    CompleteObjective(string.Empty, GetUpdatedFuelCount(), "Objective complete : " + Title);
            }
            else
            {
                string notificationText = "Not enough fuel. Collect all fuel and return.";
                ReturnedToSpaceShip = false;
                // UpdateObjective(string.Empty, GetUpdatedFuelCount(), notificationText);
            }
        }
/*
        string GetUpdatedCounterAmount()
        {
            return m_KillTotal + " / " + KillsToCompleteObjective;
        }
*/
        string GetUpdatedFuelCount()
        {
            return m_CollectedFuelTotal + " / " + NumFuelsToCollect;
        }

        void OnDestroy()
        {
            // EventManager.RemoveListener<EnemyKillEvent>(OnEnemyKilled);
            EventManager.RemoveListener<FuelCollectEvent>(OnFuelCollected);
            EventManager.RemoveListener<ReturnToSpaceShipEvent>(OnReturnedToSpaceShip);
        }
    }
}