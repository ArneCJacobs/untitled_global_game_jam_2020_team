using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body
{
    public List<Slot> Slots;
    public Stats BaseStats;

    public Stats CalculateStats()
    {
        Stats counter = new Stats();

        foreach (Slot slot in Slots)
        {
            if (slot.AssignedPart != null)
            {
                counter += slot.AssignedPart;
            }
        }
    }

}
