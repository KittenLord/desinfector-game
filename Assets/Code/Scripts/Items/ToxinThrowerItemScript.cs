using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ToxinThrowerItemScript : ItemScript
{
    [field:SerializeField] public ParticleSystem ParticleSystem {  get; private set; }

    protected override void Start()
    {
        base.Start();
        ParticleSystem?.Stop();
    }

    private void Update()
    {
        // if rotation is whatever change sprite?
    }
}
