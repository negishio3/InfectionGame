using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.AI;

public class AIPlayer : AIBase
{

    protected override void Start()
    {
        base.Start();
        targetTag = "Mob";
    }

    protected override void Update()
    { 
        base.Update();
    }

    protected override void OnTriggerEnter(Collider col)
    {
        throw new System.NotImplementedException();
    }

}