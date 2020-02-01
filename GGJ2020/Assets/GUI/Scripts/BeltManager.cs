﻿using Game.GUI;
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

    float m_frameTimer = 0.0f;
    float m_maxFrameTimer = 0.05f;
    public int MaxFramesPerBeltSection = 20;
    public bool BodiesOnly= true;

    private int m_currentFrameCounter = 0;


    public GameObject BeltSnapObject;
    public GameObject BeltSnapObjectAddition;

    public List<(GameObject obj, int index)> BeltSlots = new List<(GameObject obj, int index)>();
    int m_beltSlotCount = 1;
    List<Part> QueuedPartList = new List<Part>();

    List<Part> PartList = new List<Part>();



    // Start is called before the first frame update
    void Start()
    {


        m_beltSlotCount = ItemAmount;
        for (int i = 0; i < ItemAmount; i++)
        {
            BeltSlots.Add((GameObject.Instantiate(BeltSnapObject), i));
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (m_frameTimer > 0)
            m_frameTimer -= Time.deltaTime;
        else
        {
            m_frameTimer = m_maxFrameTimer;
            AdvanceFrame();
        }
    }

    public void AdvanceFrame()
    {
        for (int i = 0; i < ItemAmount; i++)
        {
            var item = BeltSlots[i];
            var perc = (float)m_currentFrameCounter / (float)MaxFramesPerBeltSection;
            var sectionLength = BeltLength / ItemAmount;
            var lerpVal = UnityEngine.Mathf.Lerp((item.index * sectionLength) - (BeltLength / 2), (item.index + 1) * sectionLength - (BeltLength / 2), perc);
            item.obj.transform.position = new Vector3(lerpVal, 0.0f, 0.0f);
            var ob = BeltSlots.Select(o => o.obj).FirstOrDefault();
            if (ob != null)
                Debug.Log(ob.transform.position);
        }


        
        i_advanceFrameTimer();
    }

    private void i_advanceFrameTimer()
    {
        if (m_currentFrameCounter > MaxFramesPerBeltSection)
        {
            for (int i = 0; i < BeltSlots.Count; i++)
                BeltSlots[i] = (BeltSlots[i].obj, BeltSlots[i].index + 1);

            RemoveItemFromQueue();
            AddItemToQueue(PartGenerator.GeneratePart());
            m_currentFrameCounter = 0;
        }
        else
            m_currentFrameCounter++;
    }


    public void AddItemToQueue(Part part)
    {
        var obj = Resources.Load("Prefabs/BodyPart") as GameObject;
        var instObj = GameObject.Instantiate(obj);

        var beltObj = GameObject.Instantiate(BeltSnapObject);
        BeltSlots.Add((beltObj, 0));

        var snapComp = beltObj.GetComponent<SnappingPoint>();

        var comp = instObj.GetComponent<ClickDragTest>();
        comp.snappedTo = beltObj;
        comp.startSnappedTo = beltObj;
        var bodVs = instObj.GetComponent<BodyPartVisual>();
        snapComp.AssignGameObject(instObj);

        bodVs.ResetRotationsAndTranslations(false, beltObj.transform);

    }

    public void RemoveItemFromQueue()
    {
        Debug.Log("removed item");

        var toRemoveItem = BeltSlots.FirstOrDefault(x => x.index == m_beltSlotCount);

        var snapComp = toRemoveItem.obj.GetComponent<SnappingPoint>();
        var attachedObj = snapComp.AssignedPart;

        BeltSlots.Remove(toRemoveItem);
        Destroy(attachedObj);
        Destroy(toRemoveItem.obj);
    }
}
