using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class logoScript : MonoBehaviour
{
    // Start is called before the first frame update
    int timer = 0;
    public Texture image1;
    public Texture image2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer++;
        if(timer == 30)
		{
            gameObject.GetComponent<RawImage>().texture = image1;
		}
        if (timer == 60)
        {
            gameObject.GetComponent<RawImage>().texture = image2;
            timer = 0;
        }
    }
}
