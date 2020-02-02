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
            stringbld.AppendLine("---body---");
            var stats = item.CalculateStats();
            stringbld.AppendLine($"Cha {stats.Charisma}");
            stringbld.AppendLine($"Dex {stats.Dexterity}");
            stringbld.AppendLine($"Imt {stats.Intelligence}");
            stringbld.AppendLine($"Str {stats.Strength}");
            stringbld.AppendLine($"Spd {stats.Speed}");
            stringbld.AppendLine($"Vit {stats.Vitality}");
            stringbld.AppendLine($"Dur {stats.Durability}");

            stringbld.AppendLine($"   ");

        }

        var tx = gameObject.GetComponent<Text>();
        tx.text = stringbld.ToString();
    }
}
