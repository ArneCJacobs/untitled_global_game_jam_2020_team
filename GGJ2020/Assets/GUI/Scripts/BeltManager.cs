using Game.GUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeltManager : MonoBehaviour
{
    public bool IsScrolling = false;
    public float ScrollSpeed = 0.0f;
    public float MaxScrollSpeed = 1.0f;
    public float CurrentMoveTime = 5.0f;
    public float MaxMoveTime = 5.0f;
    public float ObjectOffset = 0.5f;
    public float BeltLength = 50.0f;
    public int ItemAmount = 5;

    public List<GameObject> beltContentsList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        // var part = Resources.Load<GameObject>("Zombie_Head_01") as GameObject;
        for (int i = 0; i < ItemAmount; i++)
        {
            var gameobj = GameObject.Instantiate(Resources.Load("Prefabs/Zombie_Head_01")) as GameObject;
            gameobj.GetComponent<Transform>().position += new Vector3(i * ObjectOffset, 0,0);
            beltContentsList.Add(gameobj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveBelt();
        if (MaxMoveTime > 0.0f)
        {
            ScrollSpeed = MaxScrollSpeed;
            MaxMoveTime -= Time.deltaTime;

            MoveItemsOnBelt();
        }
        else
            ScrollSpeed = 0;

    }

    private void MoveBelt()
    {
        var scrollComps = GetComponentsInChildren<TextureScroll>();
        var rotationComps = GetComponentsInChildren<Rotation>();
        foreach (var scrollcomp in scrollComps)
            scrollcomp.ScrollSpeed = ScrollSpeed;
        foreach (var rotationComp in rotationComps)
            rotationComp.RotationSpeed = ScrollSpeed * 160;
    }

    void MoveItemsOnBelt()
    {
        List<Transform> baseElementList = new List<Transform>();
        foreach (var item in beltContentsList)
            baseElementList.Add(item.GetComponent<Transform>());

        foreach (var item in baseElementList)
        {
            item.position += new Vector3(0.01f * ScrollSpeed, 0, 0);
            if (item.position.x > BeltLength)
                item.position = new Vector3(-BeltLength, 0, 0);
        }

    }

}
