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
    public float BeltLength = 50.0f;
    public float Imagesize = 50.0f;
    public float YOffset = 0.0f;
    public int ItemAmount = 5;

    int counter = 0;

    float m_currentDist = 0.0f;
    float m_maxDist = 1.0f;

    public GameObject BeltSnapObject;

    public List<GameObject> beltContentsList = new List<GameObject>();

    public struct BeltObject
    {
        public GameObject obj;
        public Part part;
    }

    List<Part> m_partsQueue = new List<Part>();
    List<Part> m_partsList = new List<Part>();

    // Start is called before the first frame update
    void Start()
    {
        m_maxDist = (BeltLength * 2) / ItemAmount;
        for (int i = 0; i < ItemAmount; i++)
        {
            var part = PartGenerator.GeneratePart();
            var partDetails = GuiHelpers.GetPartTypeDetails(part.Type);
            var gameobj = GameObject.Instantiate(BeltSnapObject) as GameObject;
            gameobj.GetComponent<Transform>().position -= new Vector3(BeltLength, YOffset, 0);
            gameobj.GetComponent<Transform>().position += new Vector3(i * m_maxDist, 0,0);
            m_partsList.Add(part);
            beltContentsList.Add(gameobj);
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
        var moveAmount = 0.01f * ScrollSpeed;

        i_trackMovement(moveAmount);

        foreach (var item in beltContentsList)
        {
            var tf = item.GetComponent<Transform>();

            tf.position += new Vector3(moveAmount, 0, 0);
            if (tf.position.x > BeltLength)
                tf.position -= new Vector3(BeltLength * 2, 0);

            tf.position = new Vector3(tf.position.x, YOffset);
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
        var lastItem = beltContentsList.OrderByDescending(o => o.GetComponent<Transform>().position.x).FirstOrDefault();
        if (lastItem == null)
            return;

        var snapComp = lastItem.GetComponent<SnappingPoint>();
        snapComp.UnSnap(true);
    }
}
