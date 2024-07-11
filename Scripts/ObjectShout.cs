using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ObjectShout : MonoBehaviour
{ 
    public delegate void handler(string thing, EventArgs args);
    public event handler makeHappen;

    private Vector2 oldSize;
    private Vector2 newSize;


    [SerializeField]
    private bool checkDone;


    private void Start()
    {
        oldSize = this.GetComponent<RectTransform>().sizeDelta;
    }

    // Update is called once per frame
    void Update()
    {

        this.makeHappen += ObjectShout_makeHappen;
        ConSizTest();
    }

    private void ConSizTest()
    {
        newSize = this.GetComponent<RectTransform>().sizeDelta;
        if(!checkDone)
        {

            if ((newSize == oldSize))
            {
                Debug.Log($"Before preferring size: {ShoutObjSize()}");

                //repeat until n and o are different

                PreferSizeObj();
            }

            if (!(newSize == oldSize))
            {
                Debug.Log($"After preferring size call: {ShoutObjSize()}");

                //repeat until n and o are different
                UnconstrainedSizeObj();

                //call once set bool to true so all method does not run
                Debug.Log($"Collapsing object shows that: {ShoutObjSize()}");
                checkDone = true;
            }
        }


    }

    private string ShoutObjSize()
    {
        return "find the " + this.GetComponent<RectTransform>().rect + this.name;
    }

    private void UnconstrainedSizeObj()
    {
        this.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
        this.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.Unconstrained;
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
    }

    private void PreferSizeObj()
    {
        this.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
        this.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
    }

    private void ObjectShout_makeHappen(string thing, EventArgs args)
    {
        throw new NotImplementedException();
    }

    private void thisThing(string action, EventArgs next)
    {

    }
}

class Program
{
    static void Main(string[] args)
    {
        Counter c = new Counter(new System.Random().Next(10));
        c.ThresholdReached += c_ThresholdReached;
        Console.WriteLine("Press 'a' key to increase total");
        while (Console.ReadKey(true).KeyChar == 'a')
        {
            Console.WriteLine("Adding one");
            c.Add(1);
        }
    }
    static void c_ThresholdReached(object sender, ThresholdReachedEventArgs e)
    {
        Console.WriteLine($"The threshold of {e.Threshold} was reached at {e.TimeReached}.");
        Environment.Exit(0);
    }
    
    private void thingHappens()
    {
        
    }
    private Transform transform;
    IEnumerator MyCouroutine(Transform target)
    {
        while(Vector3.Distance(transform.position, target.position)> 0.05f)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, 1f * Time.deltaTime);

            yield return null;
        }
    }
}

class Thing : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

class OtherThing : IEnumerator
{
    public object Current => throw new NotImplementedException();

    public bool MoveNext()
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }
}

class Counter
{
    private int threshold;
    private int total;
    public Counter(int passedThreshold)
    {
        threshold = passedThreshold;
    }
    public void Add(int x)
    {
        total += x;
        if (total >= threshold)
        {
            ThresholdReachedEventArgs args = new ThresholdReachedEventArgs();
            args.Threshold = threshold;
            args.TimeReached = DateTime.Now;
            OnThresholdReached(args);
        }
    }
    protected virtual void OnThresholdReached(ThresholdReachedEventArgs e)
    {
        EventHandler<ThresholdReachedEventArgs> handler = ThresholdReached;
        handler?.Invoke(this, e);
    }
    public event EventHandler<ThresholdReachedEventArgs> ThresholdReached;
}
public class ThresholdReachedEventArgs : EventArgs
{
    public int Threshold { get; set; }
    public DateTime TimeReached { get; set; }
}
