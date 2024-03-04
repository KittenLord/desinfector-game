using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BookItem : Item
{
    public override void OnUseStart(ItemScript item, Entity player)
    {
        var pc = player.GetComponent<PlayerController>();

        var interaction = new Interaction(new Dictionary<string, List<string>>(), new Dictionary<string, List<InteractionElement>>
        {
            { "main", new List<InteractionElement>
            {
                new AppendLocalizedInteraction("interaction.misc.bookNote", 50, true, false),
                new HaultInteraction(),
                new ClearInteraction(),
                new SetIconInteraction("stranger", false),
                new AppendLocalizedInteraction($"interaction.books.{this.Id}.1", 50, true, false),
                new HaultInteraction(),
                new ClearInteraction(),
                new AppendLocalizedInteraction($"interaction.books.{this.Id}.2", 50, true, false),
                new HaultInteraction(),
                new ClearInteraction(),
            }}
        });

        pc.StartInteractionOutside(interaction);
    }

    public BookItem(string id) : base(id, false)
    {
    }
}
