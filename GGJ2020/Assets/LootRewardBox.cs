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
            var partVisual = new GameObject();
            partVisual.transform.parent = transform;

            var sr = partVisual.AddComponent<SpriteRenderer>();
            sr.sortingLayerName = "back";
            var partImg = partVisual.AddComponent<BodyPartVisual>();
            var rect = partVisual.AddComponent<RectTransform>();

            partImg.transform.localScale = new Vector3(5, 5, 1);
            var grid = transform.GetComponent<GridLayout>();
            
            // partImg.transform.position = transform.GetComponent<GridLayout>().CellToLocal(new Vector3Int(i, 0, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}