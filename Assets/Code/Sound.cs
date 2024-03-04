using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Sound
{
    public static AudioClip Get(string p) => Resources.Load<AudioClip>("Sounds/" + p);
}