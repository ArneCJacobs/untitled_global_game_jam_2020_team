using Game.GUI;
using Logic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPartVisual : MonoBehaviour
{
    public Part AssignedPart = null;
    public PartType PartTypeOverride = PartType.HEAD;
    public bool AttachedToBody = true;
    // Start is called before the first frame update
    void Start()
    {
        if (PartTypeOverride != PartType.TORSO)
            AssignPart(PartGenerator.GeneratePart());
        else
            AssignPart(PartGenerator.GeneratePart(PartType.TORSO));
    }

    // Update is called once per frame
    void Update()
    {
        var dragComp = GetComponent<ClickDragTest>();
    }

    public void AssignPart(Part part)
    {
        AssignedPart = part;
        SwitchTexture();
    }

    private void SwitchTexture(Transform tf = null)
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        spriteRenderer.transform.rotation = Quaternion.Euler(Vector3.forward);
        if (AssignedPart == null)
            return;

        var partDetails = GuiHelpers.GetPartTypeDetails(AssignedPart.Type);
        var sprite = Resources.Load<Sprite>(AttachedToBody ? partDetails.AssetName : partDetails.BeltAssetName);
        spriteRenderer.sprite = sprite;
        //var size = spriteRenderer.sprite.bounds.size;
        Vector3 parentScale = Vector3.one;
        //if (gameObject.transform.parent)
        //    parentScale = gameObject.transform.parent.localScale;
        if (tf != null)
            spriteRenderer.transform.localScale = new Vector3(partDetails.SizeModifier * tf.localScale.x, partDetails.SizeModifier * tf.localScale.y, 1.0f);
        else
            spriteRenderer.transform.localScale = new Vector3(partDetails.SizeModifier, partDetails.SizeModifier, 1.0f);

        Destroy(GetComponent<PolygonCollider2D>());
        gameObject.AddComponent<PolygonCollider2D>();
        
    }

    public void ResetRotationsAndTranslations(bool isBody, Transform tf = null)
    {
        AttachedToBody = isBody;
        SwitchTexture(tf);
    }
}
