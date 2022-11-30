using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderManager : MonoBehaviour
{
    [SerializeField] private Transform cam;
    List<Tile> visibleTiles = new List<Tile>();
    TileManager tileManager;
    [SerializeField] GameObject tilePrefab;
    void Awake()
    {
        tileManager = gameObject.GetComponent<TileManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        visibleTiles = tileManager.renderTileSubset(cam.position);
        foreach (Tile i in visibleTiles)
        {
            GameObject gameObj = Instantiate(tilePrefab);
            gameObj.transform.position = new Vector3(i.xCoord, i.yCoord, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
