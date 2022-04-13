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
                Debug.Log(tiles[i]);
                tiles[i].GetComponent<FloorTile>().TriggerCall(0.46153846153f);
			}
        }
    }

    public void triggerTile(int tile, float delay)
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


    
}

/*
 * 
*/
//THE THING BEING RAN THROUGH MUST BE A STACK