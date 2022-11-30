using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderManager : MonoBehaviour
{
    [SerializeField] private Transform cam;
    private List<Tile> visibleTiles = new List<Tile>();
    private TileManager tileManager;
    [SerializeField] private GameObject tilePrefab;
    void Awake()
    {
        tileManager = gameObject.GetComponent<TileManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //I'm thinking starting off generating the tiles, then moving the camera around and generating new tiles to, say, the right when moving right, and deleting tiles to the left when moving right. But either way, the original tiles need to be created.
        visibleTiles = tileManager.renderTileSubset(cam.position);
        foreach (Tile i in visibleTiles)
        {
            (int xCoord, int yCoord) = i.getCoords();
            GameObject gameObj = Instantiate(tilePrefab);
            gameObj.transform.position = new Vector3(xCoord, yCoord, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
