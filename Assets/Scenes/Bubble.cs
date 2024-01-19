using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public GameObject spherePrefab;
    public int numberOfSpheres = 3;
    public float sortingRange = 10.0f;

    private GameObject[] spheres;
    private SphereProperties[] sphereProperties;

    [System.Serializable]
    public class SphereProperties
    {
        public Color color;
        public float scale;
    }

    void Start()
    {
        CreateSpheres();
        SortSpheres();
    }

    void CreateSpheres()
    {
        spheres = new GameObject[numberOfSpheres];
        sphereProperties = new SphereProperties[numberOfSpheres];

        for (int i = 0; i < numberOfSpheres; i++)
        {
            float posY = UnityEngine.Random.Range(-sortingRange, sortingRange);
            spheres[i] = Instantiate(spherePrefab, new Vector3(0, posY, 0), Quaternion.identity);

            sphereProperties[i] = new SphereProperties
            {
                color = UnityEngine.Random.ColorHSV(),
                scale = UnityEngine.Random.Range(0.5f, 2.0f)
            };

            spheres[i].GetComponent<Renderer>().material.color = sphereProperties[i].color;
            spheres[i].transform.localScale = new Vector3(sphereProperties[i].scale, sphereProperties[i].scale, sphereProperties[i].scale);
        }
    }

    void SortSpheres()
    {
        for (int i = 0; i < numberOfSpheres - 1; i++)
        {
            for (int j = 0; j < numberOfSpheres - i - 1; j++)
            {
                if (spheres[j].transform.position.y > spheres[j + 1].transform.position.y)
                {
                    GameObject temp = spheres[j];
                    spheres[j] = spheres[j + 1];
                    spheres[j + 1] = temp;
                }
            }
        }

        for (int i = 0; i < numberOfSpheres; i++)
        {
            spheres[i].transform.position = new Vector3(i * 2, spheres[i].transform.position.y, 0);
        }
    }
}