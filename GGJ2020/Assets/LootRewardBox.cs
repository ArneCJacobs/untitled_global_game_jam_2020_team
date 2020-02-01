using System.Collections;
using System.Collections.Generic;
using Game.GUI;
using Logic;
using UnityEngine;
using UnityEngine.UI;

public class LootRewardBox : MonoBehaviour
{
    private List<Part> parts;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Display(List<Part> loot)
    {
        this.parts = loot;
        for (var i = 0; i < parts.Count; i++)
        {
            var part = parts[i];
            var partImg =
                GameObject.Instantiate(Resources.Load("Prefabs/Zombie_Head_01.prefab"), transform, true) as GameObject;
            if (partImg != null)
                partImg.transform.Translate(i * partImg.transform.GetComponent<RectTransform>().rect.width, 0, 0);


        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}