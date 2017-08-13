using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

	public Rail rail;
    public PlayMode mode;

    public float speed = 2.5f;
    public bool isReversed;
    public bool isLooping;
    public bool PingPong;
    public int howlongistrain;
	private int currentSeg;
	private float transition;
	private bool isCompleted;

    public int PositionofCar;

   

    private void Update()
	{
		if(!rail)
		{
			return;
		}
		if(!isCompleted)
		{
			Play (!isReversed);
		}
	}
	private void Play( bool forward = true)
	{
        float m = (rail.nodes[currentSeg + 1].position - rail.nodes[currentSeg].position).magnitude;
        float s = (Time.deltaTime * 1 / m) * speed;

        transition += (forward) ? s : -s;

		if (transition>1)
		{
			transition = 0;
			currentSeg++;
            if(currentSeg == rail.nodes.Length-1)
            {
                if(isLooping)
                {
                    if (PingPong)
                    {
                        transition = 1;
                        currentSeg = rail.nodes.Length - 2;
                        isReversed = !isReversed;
                    }
                    else
                    {
                        currentSeg = howlongistrain;
                    }
                }
                else
                {
                    isCompleted = true;
                    return;
                }
            }
		}
		else if (transition<0)
		{
            transition = 1;
            currentSeg--;
            if (currentSeg == rail.nodes.Length - 1)
            {
                if (isLooping)
                {
                    if (PingPong)
                    {
                        transition = 0;
                        currentSeg = 0;
                        isReversed = !isReversed;
                    }
                    else
                    {
                        currentSeg = currentSeg = rail.nodes.Length- 2; 
                    }
                }
                else
                {
                    isCompleted = true;
                    return;
                }
            }
        }

      
         

      
            transform.position = rail.PositionOnRail(currentSeg - PositionofCar , transition, mode);
            transform.rotation = rail.Orientation(currentSeg - PositionofCar, transition);
        
    }

}
