using Game.GUI;
using Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartVisual : MonoBehaviour
{
    public Part AssignedPart = null;
    // Start is called before the first frame update
    void Start()
    {
        AssignPart(PartGenerator.GeneratePart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AssignPart(Part part)
    {
        AssignedPart = part;
        SwitchTexture(AssignedPart);
    }

    private void SwitchTexture(Part part)
    {

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        spriteRenderer.transform.rotation = Quaternion.Euler(Vector3.forward);


        var partDetails = GuiHelpers.GetPartTypeDetails(part.Type);
        var sprite = Resources.Load<Sprite>(partDetails.AssetName);
        spriteRenderer.sprite = sprite;
        var size = spriteRenderer.sprite.bounds.size;
        spriteRenderer.transform.localScale = new Vector3(partDetails.SizeModifier, partDetails.SizeModifier, 1.0f);
        spriteRenderer.transform.rotation = Quaternion.Euler(Vector3.forward * partDetails.RotationEuler);

        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();

    }
}
