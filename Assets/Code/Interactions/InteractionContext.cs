using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class InteractionContext
{
    public InteractionBar Bar { get; private set; }
    public InteractionPerformer Performer { get; private set; }

    public InteractableEntity Player { get; private set; }
    public InteractableEntity Other { get; private set; }

    public InteractionContext(InteractionBar bar, InteractionPerformer performer, InteractableEntity player, InteractableEntity other)
    {
        Bar = bar;
        Performer = performer;
        Player = player;
        Other = other;
    }
}