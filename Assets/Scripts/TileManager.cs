using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private int height, width;
    public List<List<Tile>> tiles=new List<List<Tile>>();
    private float timer;
    // Start is called before the first frame update
    void Awake()
    {
        //need to make all of the tiles
        for(int x=0; x < width; x++)
        {
            List<Tile> tempCol = new List<Tile>();
            for(int y = 0; y < height; y++)
            {
                Gas airContent;
                if (y > 0 && y < height && x > 0 && x < width)
                {
                    airContent = GasPresets.Air(100);
                }
                else
                {
                    airContent = GasPresets.Air(0);
                }
                Tile tempTile = new Tile(x, y, airContent) ;
                tempCol.Add(tempTile);
            }
            tiles.Add(tempCol);
        }
        //need to start the timer
        timer = 0F;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= .1)
        {
            //tick is sixth of a second
            //all update logic goes here
            foreach (List<Tile> j in tiles)
            {
                foreach (Tile i in j)
                {
                    if (i.active)
                    {
                        foreach (Gas e in i.getGasContents())
                        {
                            //spread gas
                            e.spreadToTiles(findTile(i.xCoord - 1, i.yCoord), findTile(i.xCoord + 1, i.yCoord), findTile(i.xCoord, i.yCoord - 1), findTile(i.xCoord, i.yCoord + 1));
                        }
                    }
                    i.updateTile();
                }
            }
            timer = 0;
        }
    }
    public Tile findTile(int x, int y)
    {
        return tiles[x][y] ;
    }
    public List<Tile> renderTileSubset(Vector3 camLoc)
    {
        List<Tile> tempList = new List<Tile>();
        //for i in range 40, for j in range 40, findTile[i][j]
        for(int i = -20; i <= 20; i++)
        {
            for(int j = -20; j <= 20; j++)
            {
                if (camLoc.x+i >= 0 && camLoc.y+j >= 0)
                {
                    tempList.Add(findTile((int) camLoc.x+i, (int) camLoc.y+j));
                }
            }
        }
        return tempList;
    }
}
