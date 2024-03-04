using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NoteItem : Item
{
    public override void OnUseStart(ItemScript item, Entity player)
    {
        var pc = player.GetComponent<PlayerController>();
        var needToDisable = !UI.Main.ToggleNote();

        pc.canMove = needToDisable;
        pc.canInteract = needToDisable;
        pc.canOpenInventory = needToDisable;
    }

    public NoteItem(string id) : base(id, false)
    {
    }
}