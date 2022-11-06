using UnityEngine;

namespace Interactions
{
    public enum InteractionEvents
    {
        // Default case
        None,
        
        // Generic Interaction Events
        NextStep,
        EndInteraction,
        
        // Specific Interaction Triggers (interact w dead body, find specific clue, etc.)
        [InspectorName("Clue/MurderWeapon")]
        MurderWeapon
    }
}