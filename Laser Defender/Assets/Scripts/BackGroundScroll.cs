using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    [SerializeField] float BackGroundSpeed = 0.5f;
    Material Material;
    Vector2 offSet;
    // Start is called before the first frame update
    void Start()
    {
        Material = GetComponent<Renderer>().material;
        offSet = new Vector2(0f, BackGroundSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        Material.mainTextureOffset += offSet * Time.deltaTime;
    }
}
