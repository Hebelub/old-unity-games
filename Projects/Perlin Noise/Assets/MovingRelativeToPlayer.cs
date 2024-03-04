using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingRelativeToPlayer : MonoBehaviour
{
    public int width = 10;
    public int height = 10;

    public float scale = 20F;

    public float offsetX1 = 0F;
    public float offsetY1 = 0F;
    public float offsetX2 = 0F;
    public float offsetY2 = 0F;
    public float offsetX3 = 0F;
    public float offsetY3 = 0F;


    public bool willResize = true;
    public bool randomizeSeed = false;

    private void Awake()
    {
    }

    private void Start()
    {
        GameManager.instance.creatures.Add(this);

        if(randomizeSeed)
        {
            RandomizeSeed();
        }
    }

    public void RandomizeSeed()
    {
            offsetX1 = Random.Range(0f, 99999F);
            offsetY1 = Random.Range(0f, 99999F);
            offsetX2 = Random.Range(0f, 99999F);
            offsetY2 = Random.Range(0f, 99999F);
            offsetX3 = Random.Range(0f, 99999F);
            offsetY3 = Random.Range(0f, 99999F);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            RandomizeSeed();
        }

        Vector3 playerPos = GameManager.instance.player.transform.position;

        transform.position = PlayerPosToPos(playerPos);
        transform.localScale = Vector3.one * PlayerPosToScale(playerPos);
    }

    public Vector3 PlayerPosToPos(Vector3 player)
    {
        float sampleA = Mathf.PerlinNoise(player.x / scale + offsetX1, player.y / scale + offsetY1) - 0.5F;
        float sampleB = Mathf.PerlinNoise(player.x / scale + offsetX2, player.y / scale + offsetX2) - 0.5F;

        Vector3 position = new Vector3(sampleA * width, sampleB * height, 0);

        return position;
    }

    public float PlayerPosToScale(Vector3 player)
    {
        if (willResize)
        {
            float size = Mathf.PerlinNoise(player.x / scale + offsetX3, player.y / scale + offsetX3) + 0.1F;

            return size;
        }
        return 1F;
    }
}
