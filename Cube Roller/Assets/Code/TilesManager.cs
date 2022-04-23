using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour
{
    public List<GameObject> tiles;
    public Stack<int> tileStack = new Stack<int>();
    public Stack<int> tileStack2 = new Stack<int>();
    public Queue<int> tileQueue = new Queue<int>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
			for (int i = 0; i<tiles.Count ; i++)
			{
                
                tiles[i].GetComponent<FloorTile>().TriggerCall(0.46153846153f);
			}
        }
    }

    public void TileGlow(int tile, bool on)
	{
        int tempCount = tileQueue.Count;
        Queue<int> tileQueueTemp = new Queue<int>();

        for (int i = 0; i < tempCount; i++)
		{
            tiles[tileQueue.Peek()-1].GetComponent<FloorTile>().Glow(false,i);
            tileQueueTemp.Enqueue(tileQueue.Dequeue());
        }
        int tempAgain = tileQueueTemp.Count;
        for (int i = 0; i < tempAgain; i++)
        {
            tiles[tileQueueTemp.Peek() - 1].GetComponent<FloorTile>().Glow(on, i);
            tileQueue.Enqueue(tileQueueTemp.Dequeue());
        }
    }

    public void TileTurnOff(int tile)
	{
        tiles[tile - 1].GetComponent<FloorTile>().TurnOffHighest();
	}

    public void ResetQueue()
	{
        int temp = tileQueue.Count;
		for (int i = 0; i < temp; i++)
		{
            tiles[tileQueue.Dequeue() - 1].GetComponent<FloorTile>().Glow(false,1);
		}
        
	}
    public void AddtoQueue(int tile)
	{
        tileQueue.Enqueue(tile);
	}


    /*public void triggerTile(int tile, float delay)
	{
        //tiles[tile].GetComponent<FloorTile>().TriggerCall(delay);
        tileQueue.Enqueue(tile);
        Debug.Log(tileQueue.Count + "TILEQUEUE COUNT");
        int tileQueueCount = tileQueue.Count;
        for (int i = 0; i < tileQueueCount; i++)
        {
            Debug.Log("I IS " + i.ToString());
            tileStack.Push(tileQueue.Dequeue());
        }
        Debug.Log(tileStack.Count + "TILESTACK COUNT");
        int tileStackCount = tileStack.Count;
        for (int i = 0; i < tileStackCount; i++)
        {
            tiles[tileStack.Peek()].gameObject.GetComponent<FloorTile>().Advance(i,delay);
            tileStack2.Push(tileStack.Pop());
        }
        int tileStackCount2 = tileStack2.Count;
        for (int i = 0; i < tileStackCount2; i++)
        {
            tileQueue.Enqueue(tileStack2.Pop());
        }
        if (tileQueue.Count > 4)
        {
            tileQueue.Dequeue();
        }
    }
    */

    
}

/*
 * 
*/
//THE THING BEING RAN THROUGH MUST BE A STACK