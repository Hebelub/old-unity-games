using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Levels : MonoBehaviour
{
    public SquareSpawner squareSpawner;

    public int currentLoadedImage = 0;

    private void Awake()
    {
        int startLevel = 0;
        DeactivateAllImages();
        LoadImageNumber(startLevel);
        currentLoadedImage = startLevel;
    }

    public void DeactivateAllImages()
    {
        foreach(Transform image in transform)
        {
            image.gameObject.SetActive(false);
        }
    }

    public void NextLevel()
    {
        squareSpawner.ReplaceAllSquares();

        LoadImageNumber(currentLoadedImage + 1);
    }

    public void PreviousLevel()
    {
        squareSpawner.ReplaceAllSquares();

        LoadImageNumber(currentLoadedImage + transform.childCount - 1);
    }

    public void LoadImageNumber(int imageNumber)
    {
        int childQuantity = transform.childCount;

        imageNumber = imageNumber % childQuantity;

        transform.GetChild(currentLoadedImage).gameObject.SetActive(false);

        transform.GetChild(imageNumber).gameObject.SetActive(true);

        currentLoadedImage = imageNumber;
    }
}

