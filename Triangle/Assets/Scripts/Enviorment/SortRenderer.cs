using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortRenderer : MonoBehaviour
{
    public int sortingbase = 5000;
    public int offset = 0;
    private Renderer[] renderers;
    public bool isStatic = false;

    private int layeroffset = 0;

    // Start is called before the first frame update
    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    public void Rerun()
    {
        renderers = GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        layeroffset = 0;
        foreach (Renderer renderer in renderers)
        {
            renderer.sortingOrder = (int)(sortingbase - (transform.position.y * 10) - offset - layeroffset);
            layeroffset++;
        }
        if (isStatic)
        {
            Destroy(this);
        }
    }
}
