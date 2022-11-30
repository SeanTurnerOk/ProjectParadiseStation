using System.Collections;
using System.Collections.Generic;

public class Tile
{
    public int xCoord, yCoord;
    public bool active = false;
    private List<Liquid> liquids = new List<Liquid>();
    private List<Gas> gasses=new List<Gas>();
    public List<Gas> nextGas=new List<Gas>();
    private List<Item> items=new List<Item>();
    private Station station;
    private Critter critter;
    private List<Effect> effects=new List<Effect>();
    
    public Tile(int xPos, int yPos, Gas airContent)
    {
        this.xCoord = xPos;
        this.yCoord = yPos;
        this.gasses.Add(airContent);
    }
    public List<Gas> getGasContents()
    {
        return this.gasses;
    }
    public void setGasContents(List<Gas> gasses)
    {
        this.gasses = gasses;
    }
    public (Gas, int) getGas(string type) {
        for(int i =0;i<gasses.Count;i++){
            if (gasses[i].type == type)
            {
                return (gasses[i],i);
            }
        }
        return (null,0);
    }
    public void setGas(Gas gasItem)
    {
        for(int i = 0; i < gasses.Count; i++)
        {
            if (gasses[i].type == gasItem.type){
                gasses[i]=gasItem;
                return;
            }
        }
        gasses.Add(gasItem);
    }
    public void updateTile()
    {
        //consider throwing this into a different function when you add other things to get updated.
        //using a gas for nextgas means I can couple together the amount into the unused thing. Since I'm not passing by reference, creating a new Gas shouldn't take any more space
        foreach(Gas i in nextGas)
        {
            bool found = false;
            foreach(Gas k in this.gasses)
            {
                if (i.type == k.type)
                {
                    k.increaseAmount(i.getAmount());
                    found = true;
                }
            }
            if (!found)
            {
                this.gasses.Add(i);
            }
            nextGas.Remove(i);
        }
    }
}
