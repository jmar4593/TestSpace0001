using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Scroll03 : MonoBehaviour
{

    private UnityEvent thing;

    private UnityAction crazy;

    delegate void happynews();
    // Start is called before the first frame update  
    void Start()
    {
        thing.AddListener(crazy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TheRight()
    {

    }

    [SerializeField] private Scroll04 subjectToObserve;

    private void OnThingHappened()
    {
        // any logic that responds to event goes here
        Debug.Log("Observer responds");
    }

    private void Awake()
    {
        if (subjectToObserve != null)
        {
            subjectToObserve.ThingHappened += OnThingHappened;
        }
    }

    private void OnDestroy()
    {
        if (subjectToObserve != null)
        {
            subjectToObserve.ThingHappened -= OnThingHappened;
        }
    }
}
