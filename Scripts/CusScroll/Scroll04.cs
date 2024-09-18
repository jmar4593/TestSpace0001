using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Scroll04 : MonoBehaviour
{


    public event Action ThingHappened;

    public void DoThing()
    {
        ThingHappened?.Invoke();
    }

}
