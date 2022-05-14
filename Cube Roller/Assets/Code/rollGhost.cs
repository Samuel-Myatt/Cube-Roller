using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rollGhost : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed = 600;
    bool isMoving = false;

    bool buffer = false;
    public int bufferlength = 100;
    public int curtile = 1;
    public bool safe = true;
    public float health = 100f;

    public LevelWriter levelWriter;
    public AudioManager audioManager;
    public TilesManager tilesManager;
    public SoundEffectsManager soundEffects;
    public Material highlightMaterial;
    public Material normalMaterial;

    public void GreenCheck()
    {
        if (tilesManager.tiles[curtile - 1].GetComponent<FloorTile>().green == true)
        {
            tilesManager.tiles[curtile].GetComponent<FloorTile>().Advance(4, 0f);
            health += 5f;
        }
    }

    public void RollActivate(int tile, bool last)
	{
        StartCoroutine(Roll(tile,last));
	}
	IEnumerator Roll(int tile, bool last)
    {
        Vector3 direction = Vector3.left;
        isMoving = true;
        if(last)
		{
            soundEffects.PlaySound("Land2");
        }
        else
		{
            soundEffects.PlaySound("Land1");
        }
        
        switch(curtile - tile)
		{
            case -3:
                direction = Vector3.back;
                Debug.Log("DOWN");
                break;
            case -1:
                direction = Vector3.right;
                break;
            case 1:
                direction = Vector3.left;
                break;
            case 3:
                direction = Vector3.forward;
                Debug.Log("UP");
                break;
		}
        curtile = tile;
        float remainingAngle = 90;
        Vector3 rotationCenter = transform.position + direction / 2 + Vector3.down / 2;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction);

		while (remainingAngle > 0)
		{
            float rotatingAngle = Mathf.Min(Time.deltaTime * speed, remainingAngle);
            transform.RotateAround(rotationCenter, rotationAxis, rotatingAngle);
            remainingAngle -= rotatingAngle;
            yield return null;
		}

        
        tilesManager.AddtoQueue(curtile);
        tilesManager.TileGlow(curtile - 1, true);
        isMoving = false;
	}
}
