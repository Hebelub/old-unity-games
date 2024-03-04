using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise2 : MonoBehaviour
{
    public GameObject dot;

    public int width = 256;
    public int height = 256;

    public int numberOfDots = 100;

    public float scale = 20F;

    public float offsetX = 100F;
    public float offsetY = 100F;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GenerateTexture();

            offsetX = Random.Range(0f, 99999F);
            offsetY = Random.Range(0f, 99999F);
        }
    }

    void GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int x = 0; x < numberOfDots; x++)
        {
            float sampleA = Mathf.PerlinNoise(x / scale, 0 + offsetY) - 0.5F;
            float sampleB = Mathf.PerlinNoise(0 + offsetX, x / scale) - 0.5F;

            Vector3 position = new Vector3(sampleA * width, sampleB * height, 0);
            Instantiate(dot, position, Quaternion.identity);
        }
    }

    Color CalculateColor(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / height * scale + offsetY;

        float sample = Mathf.PerlinNoise(xCoord, yCoord);

        Color pixelColor = new Color(sample, sample, sample);
        return pixelColor;
    }

}
