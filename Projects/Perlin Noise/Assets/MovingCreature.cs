using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCreature : MonoBehaviour
{
    public float time = 0;

    public int width = 10;
    public int height = 10;

    public float scale = 20F;

    public float offsetX = 0F;
    public float offsetY = 0F;

    private void Start()
    {
        offsetX = Random.Range(0f, 99999F);
        offsetY = Random.Range(0f, 99999F);
    }

    private void Update()
    {
        transform.position = TimeToPos();
    }

    Vector3 TimeToPos()
    {
        float sampleA = Mathf.PerlinNoise(time / scale, offsetX + 0.5F) - 0.5F;
        float sampleB = Mathf.PerlinNoise(time / scale, offsetX) - 0.5F;

        Vector3 position = new Vector3(sampleA * width, sampleB * height, 0);

        float m = 1F;
        if (DistanceToPlayer() < 4)
            m = DistanceToPlayer() / 4F;
        time += Time.deltaTime * m;


        return position;


    }

    public float DistanceToPlayer()
    {
        return Vector3.Distance(GameManager.instance.player.transform.position, transform.position);
    }
}
