using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour
{

    public int currTile = 3;
    public bool green = false;
    
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TriggerCall(float delay)
	{
        StartCoroutine(Trigger(delay));
	}
    private IEnumerator Expand(float delay, int stage)
    {
        GameObject tempPlane = transform.GetChild(stage).gameObject;
        tempPlane.active = true;
        tempPlane.transform.localScale = new Vector3(0.1f, 1f, 0.1f);
        if(stage > 0)
		{
            transform.GetChild(stage - 1).gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        for (int i = 0; i < 9; i++)
		{
            tempPlane.transform.localScale += new Vector3(0.1f, 0, 0.1f);
            yield return new WaitForSeconds(delay / 9);
        }
            
        
        
        
    }
    public void Advance(int tile, float delay)
	{
        switch(tile)
		{
            case 0:
                //transform.GetChild(0).gameObject.active = true;
                StartCoroutine(Expand(delay, 0));
                break;
            case 1:
                //transform.GetChild(1).gameObject.active = true;
                StartCoroutine(Expand(delay, 1));

                break;
            case 2:
                //transform.GetChild(2).gameObject.active = true;
                StartCoroutine(Expand(delay, 2));
                break;
            case 3:
                transform.GetChild(2).gameObject.active = true;
                green = true;
                break;
            case 4:
                green = false;
                transform.GetChild(0).gameObject.active = false;
                transform.GetChild(1).gameObject.active = false;
                transform.GetChild(2).gameObject.active = false;
                transform.GetChild(3).gameObject.active = true;
                break;
        }
        /*if(currTile == 0)
		{
            
            transform.GetChild(currTile).gameObject.active = false;
            currTile = 3;
            transform.GetChild(currTile).gameObject.active = true;
        }
        else
		{
            transform.GetChild(currTile).gameObject.active = false;
            currTile--;
            transform.GetChild(currTile).gameObject.active = true;
        }*/
	}
    IEnumerator Trigger(float delay)
	{
        Debug.Log("Triggered");
        transform.GetChild(0).gameObject.active = true;
        yield return new WaitForSeconds(delay);
        transform.GetChild(0).gameObject.active = false;
        transform.GetChild(1).gameObject.active = true;
        yield return new WaitForSeconds(delay);
        transform.GetChild(1).gameObject.active = false;
        transform.GetChild(2).gameObject.active = true;
        green = true;
        yield return new WaitForSeconds(delay);
        green = false;
        transform.GetChild(2).gameObject.active = false;
        transform.GetChild(3).gameObject.active = true;
    }
}
