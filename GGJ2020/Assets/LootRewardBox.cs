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
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
            
        }
        this.parts = loot;
        foreach (var part in parts)
        {
            var partVisual = new GameObject();
            partVisual.transform.parent = transform;

            var cr = partVisual.AddComponent<CanvasRenderer>();
            var rect = partVisual.AddComponent<RectTransform>();
            var img = partVisual.AddComponent<Image>();
            var partDetails = GuiHelpers.GetPartTypeDetails(part.VisualType);
            img.sprite = Resources.Load<Sprite>(partDetails.AssetName);
            img.transform.localScale = new Vector3(partDetails.SizeModifier, partDetails.SizeModifier, 1.0f);
            img.transform.rotation = Quaternion.Euler(Vector3.forward * partDetails.RotationEuler);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}