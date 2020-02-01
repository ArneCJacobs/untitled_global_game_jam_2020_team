using Game.GUI;
using Logic;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BeltManager : MonoBehaviour
{
    public bool IsScrolling = false;
    public float ScrollSpeed = 0.0f;
    public float MaxScrollSpeed = 1.0f;
    public float CurrentMoveTime = 5.0f;
    public float MaxMoveTime = 5.0f;
    public float ObjectXOffset = 1f;
    public float BeltLength = 50.0f;
    public int ItemAmount = 5;

    int counter = 0;

    float m_currentDist = 0.0f;
    float m_maxDist = 1.0f;

    public List<GameObject> beltContentsList = new List<GameObject>();

    List<Part> m_partsQueue = new List<Part>();
    List<Part> m_partsList = new List<Part>();

    // Start is called before the first frame update
    void Start()
    {
        m_maxDist = (BeltLength * 2) / ItemAmount;
        for (int i = 0; i < ItemAmount; i++)
        {

            var gameobj = GameObject.Instantiate(Resources.Load("Prefabs/Zombie_Head_01")) as GameObject;
            gameobj.GetComponent<Transform>().position -= new Vector3(BeltLength, ObjectXOffset, 0);
            gameobj.GetComponent<Transform>().position += new Vector3(i * m_maxDist, 0,0);
            beltContentsList.Add(gameobj);
        }

        for (int i = 0; i < ItemAmount; i++)
        {
            m_partsList.Add(PartGenerator.GeneratePart());
        }
    }

    // Update is called once per frame
    void Update()
    {
        i_moveBelt();
        if (MaxMoveTime > 0.0f)
        {
            ScrollSpeed = MaxScrollSpeed;
            MaxMoveTime -= Time.deltaTime;

            i_moveItemsOnBelt();
        }
        else
            ScrollSpeed = 0;

    }

    private void i_moveBelt()
    {
        var scrollComps = GetComponentsInChildren<TextureScroll>();
        var rotationComps = GetComponentsInChildren<Rotation>();
        foreach (var scrollcomp in scrollComps)
            scrollcomp.ScrollSpeed = ScrollSpeed;
        foreach (var rotationComp in rotationComps)
            rotationComp.RotationSpeed = ScrollSpeed * 160;
    }

    void i_moveItemsOnBelt()
    {
        List<Transform> baseElementList = new List<Transform>();
        foreach (var item in beltContentsList)
            baseElementList.Add(item.GetComponent<Transform>());

        var moveAmount = 0.01f * ScrollSpeed;

        i_trackMovement(moveAmount);

        foreach (var item in baseElementList)
        {
            item.position += new Vector3(moveAmount, 0, 0);
            if (item.position.x > BeltLength)
                item.position -= new Vector3(BeltLength * 2, 0, 0);
        }

    }

    private void i_trackMovement(float moveAmount)
    {
        if (m_currentDist < m_maxDist)
            m_currentDist += moveAmount;
        else
        {
            m_currentDist = 0.0f;
            MoveNext();
        }
    }

    private void MoveNext()
    {
        if (m_partsList.Count > 0)
        {
            var lastitem = m_partsList.Last();
            m_partsList.Remove(lastitem);
        }

        if (m_partsQueue.Count > 0)
        {
            m_partsList.Reverse();
            m_partsList.Add(m_partsQueue.First());
            m_partsList.Reverse();
        }

        for (int i = 0; i < beltContentsList.Count; i++)
        {
            if (i < m_partsList.Count && i >= 0)
            {
                SwitchTexture(m_partsList[i], beltContentsList[i]);
            }
        }
    }

    private void SwitchTexture(Part part,GameObject obj)
    {
        var spriteRenderer = obj.GetComponent<SpriteRenderer>();
        var partDetails = GuiHelpers.GetPartTypeDetails(part.Type);
        spriteRenderer.sprite = Resources.Load<Sprite>(partDetails.AssetName);
    }

}
