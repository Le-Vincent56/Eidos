using Eidos.Battles;
using Eidos.EventBus;
using UnityEngine;

namespace Eidos.Interactables
{
    public class Battler : MonoBehaviour, IInteractable
    {
        [Header("References")]
        [SerializeField] private Mosaic mosaic;

        public void Interact()
        {
            // Engage a battle
            EventBus<EngageBattle>.Raise(new EngageBattle()
            {
                CatalystName = mosaic.CatalystName,
                Eidras = mosaic.Eidras
            });
        }
    }
}
