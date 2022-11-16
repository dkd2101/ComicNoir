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
        [InspectorName("Clue/Body")]
        Body,
        [InspectorName("Clue/Bank Statement")]
        BankStatement,
        [InspectorName("Clue/Broken Glass Door")]
        BrokenGlassDoor,
        [InspectorName("Clue/Glass Shard")]
        GlassShard,
        [InspectorName("Clue/Bowling Ball")]
        BowlingBall,
        [InspectorName("Clue/Bowling Ball Stand")]
        BowlingBallStand,
        
        // Flavor Text Props
        [InspectorName("Prop/Book")]
        Book,
        [InspectorName("Prop/Diary")]
        Diary,
        [InspectorName("Prop/Photo of Friends")]
        FriendsPhoto,
    }
}