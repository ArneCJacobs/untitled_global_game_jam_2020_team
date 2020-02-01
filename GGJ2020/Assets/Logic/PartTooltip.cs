using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Logic;
using UnityEngine;

public class PartTooltip : SimpleTooltip
{
    // Start is called before the first frame update
    private bool initialized = false;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialized)
        {
            initialized = true;
            BodyPartVisual bodyPartVisual = gameObject.GetComponent<BodyPartVisual>();
            Part part;
            if (bodyPartVisual == null)
                return;

            part = bodyPartVisual.AssignedPart;
            StringBuilder sb = new StringBuilder();
            infoRight = "`" + part.Name.ToLower();

            sb.Append("Speed: " + (int) part.Stats.Speed + "\n");
            sb.Append("Vitality: " + (int) part.Stats.Vitality + "\n");
            sb.Append("Intelligence: " + (int) part.Stats.Intelligence + "\n");
            sb.Append("Strength: " + (int) part.Stats.Strength + "\n");
            sb.Append("Dexterity: " + (int) part.Stats.Dexterity + "\n");
            sb.Append("Charisma: " + (int) part.Stats.Charisma + "\n");
            sb.Append("Durability: " + (int) part.Stats.Durability + "\n");

            infoLeft = sb.ToString();
            // infoRight = "cookies `cookies @cookies ~cookies $cookies";
        }
    }
}