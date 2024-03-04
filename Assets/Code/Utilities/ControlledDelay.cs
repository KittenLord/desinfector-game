using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ControlledDelay
{
    public int Delay { get; private set; }
    private bool ForceStop = false;

    public async Task Start()
    {
        var amount = Delay / 50;
        for(int i = 0; i < amount; i++) 
        {
            if (ForceStop) break;
            await Task.Delay(50);
        }
    }

    public void Stop()
    {
        ForceStop = true;
    }

    public ControlledDelay(int delay)
    {
        Delay = delay;
    }
}
