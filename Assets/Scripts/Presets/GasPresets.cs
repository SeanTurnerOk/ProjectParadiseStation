using System.Collections;
using System.Collections.Generic;


public class GasPresets
{
    //GasPresets.Air(100)
    public static Gas Air(int amount) {
        List<string> tags = new List<string>();
        tags.Add("breathable");
        Gas temp = new Gas("air", tags, amount);
        return temp;
    }
}
