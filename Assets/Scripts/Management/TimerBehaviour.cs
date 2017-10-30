using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBehaviour : MonoBehaviour {

    private static int id = 0;
    private List<Subscriber> subscribers = new List<Subscriber>();
	
	// Update is called once per frame
	void Update () {
		foreach(Subscriber subscriber in subscribers)
        {
            if (subscriber.Countdown > 0)
            {
                subscriber.Countdown -= Time.deltaTime;
                if (subscriber.Countdown <= 0)
                    subscriber.Timable.TimeUp(subscriber.Id);
            }
        }
	}

    public int Subscribe(ITimable timable, float delay)
    {
        subscribers.Add(new Subscriber() { Id = ++id, Timable = timable, Delay = delay, Countdown = 0 });
        return id;
    }

    public void Unsubscribe(int id)
    {
        subscribers.RemoveAt(subscribers.FindIndex(subscriber => subscriber.Id == id));
    }

    public void StartTimer(int id)
    {
        subscribers.Find(subscriber => subscriber.Id == id).Init();
    }

    public void StopTimer(int id)
    {
        subscribers.Find(subscriber => subscriber.Id == id).Reset();
    }

    private class Subscriber
    {
        public int Id { get; set; }
        public ITimable Timable { get; set; }
        public float Delay { get; set; }
        public float Countdown { get; set; }

        public void Init()
        {
            Countdown = Delay;
        }

        public void Reset()
        {
            Countdown = 0;
        }
    }
}
