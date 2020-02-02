using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PartyText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var stringbld = new StringBuilder();

        var gs = GameObject.FindGameObjectWithTag("GameState").GetComponent<GameState>();

        foreach (var item in gs.BodyQueue)
        {
            stringbld.AppendLine($"{item.Part.Name}");
            var stats = item.CalculateStats();
            stringbld.AppendLine($"Cha {item.Part.Stats.Charisma}");
            stringbld.AppendLine($"Dex {item.Part.Stats.Dexterity}");
            stringbld.AppendLine($"Imt {item.Part.Stats.Intelligence}");
            stringbld.AppendLine($"Str {item.Part.Stats.Strength}");
            stringbld.AppendLine($"Spd {item.Part.Stats.Speed}");
            stringbld.AppendLine($"Vit {item.Part.Stats.Vitality}");
            stringbld.AppendLine($"Dur {item.Part.Stats.Durability}");

            stringbld.AppendLine($"   ");

        }

        var tx = gameObject.GetComponent<Text>();
        tx.text = stringbld.ToString();
    }
}
