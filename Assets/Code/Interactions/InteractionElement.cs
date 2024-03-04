using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Newtonsoft.Json;

public abstract class InteractionElement
{
    public abstract InteractionElement Copy();

    public abstract Task Execute(InteractionContext context);
    public abstract void Finish();
    public abstract void ForceStop();

    public virtual void Input(int index) {}
    public virtual void InputText(string text) {}
}