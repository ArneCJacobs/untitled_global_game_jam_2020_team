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
        var stats = gs.Party.GetAverageStats();


        stringbld.AppendLine($"Party members: {gs.Party.Bodies.Count}");
        stringbld.AppendLine($"Cha {(int)stats.Charisma}");
        stringbld.AppendLine($"Dex {(int)stats.Dexterity}");
        stringbld.AppendLine($"Imt {(int)stats.Intelligence}");
        stringbld.AppendLine($"Str {(int)stats.Strength}");
        stringbld.AppendLine($"Spd {(int)stats.Speed}");
        stringbld.AppendLine($"Vit {(int)stats.Vitality}");
        stringbld.AppendLine($"Dur {(int)stats.Durability}");

        stringbld.AppendLine($"   ");

        var tx = gameObject.GetComponent<Text>();
        tx.text = stringbld.ToString();
    }
}