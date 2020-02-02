using System;
using Logic;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using UnityEngine;

public class BeltManager : MonoBehaviour
{
    // public bool IsScrolling = false;
    // public float ScrollSpeed = 0.0f;
    // public float MaxScrollSpeed = 1.0f;
    // public float CurrentMoveTime = 5.0f;
    // public float MaxMoveTime = 5.0f;
    public float TimeToPosition = 1.0f;

    public float BeltLength = 50.0f;

    // public float Imagesize = 50.0f;
    public float YOffset = 0.0f;
    public int ItemAmount = 5;
    public float BeltTimer;
    public float LerpValue;

    float m_frameTimer = 0.0f;
    public bool BodiesOnly = true;

    public GameObject BeltSnapObject;
    public List<GameObject> BeltSnapObjectAdditions;

    public List<(GameObject obj, int index)> BeltSlots = new List<(GameObject obj, int index)>();
    int m_beltSlotCount = 1;
    List<Part> QueuedPartList = new List<Part>();

    List<Part> PartList = new List<Part>();
    public bool paused = true;

    public bool ControlledByPlayer = false;
    private bool pauzeAfterRemove = false;

    private GameState gameState;


    // Start is called before the first frame update
    void Start()
    {
        gameState = transform.Find("/GameState").GetComponent<GameState>();
        m_beltSlotCount = ItemAmount;
        for (var i = 0; i < ItemAmount; i++)
        {
            BeltSlots.Add((GameObject.Instantiate(BeltSnapObject), i));
        }
    }

    private void OnEnable()
    {
        StateEventManager.StatePauseEvent += Pause;
        StateEventManager.StatePlayEvent += Play;
    }

    private void OnDisable()
    {
        StateEventManager.StatePauseEvent -= Pause;
        StateEventManager.StatePlayEvent -= Play;
    }

    // Update is called once per frame
    void Update()
    {
        if (ControlledByPlayer && Input.GetKeyDown("space"))
        {
            TimeToPosition = 0.3f;
            paused = false;
            pauzeAfterRemove = true;
        }

        if (paused) return;
        if (m_frameTimer <= 0)
        {
            i_advanceBelt();
            m_frameTimer = TimeToPosition;
        }
        else
        {
            m_frameTimer -= Time.deltaTime;
        }

        UpdateMovement();
    }

    private void UpdateMovement()
    {
        BeltTimer += Time.deltaTime / TimeToPosition;

        for (int i = 0; i < ItemAmount; i++)
        {
            var item = BeltSlots[i];
            var sectionLength = BeltLength / ItemAmount;

            LerpValue = Mathf.Lerp((item.index * sectionLength) - (BeltLength / 2),
                (item.index + 1) * sectionLength - (BeltLength / 2), BeltTimer);
            var snappingPart = item.obj.GetComponent<SnappingPoint>();

            item.obj.transform.position = new Vector3(LerpValue, YOffset);
            var ob = BeltSlots.Select(o => o.obj).FirstOrDefault();
        }
    }

    private void i_advanceBelt()
    {
        for (int i = 0; i < BeltSlots.Count; i++)
            BeltSlots[i] = (BeltSlots[i].obj, BeltSlots[i].index + 1);

        RemoveItemFromQueue();
        AddItemToQueue(PartGenerator.GeneratePart());
        BeltTimer = 0;
    }


    public void AddItemToQueue(Part part)
    {
        var rand = new System.Random();
        GameObject instObj = null;
        instObj = GameObject.Instantiate(BeltSnapObjectAdditions[rand.Next(0, BeltSnapObjectAdditions.Count)]);
        var beltObj = GameObject.Instantiate(BeltSnapObject);
        BeltSlots.Add((beltObj, 0));

        var snapComp = beltObj.GetComponent<SnappingPoint>();

        var comp = instObj.GetComponent<ClickDragTest>();
        comp.snappedTo = beltObj;
        comp.startSnappedTo = beltObj;
        var bodVs = instObj.GetComponent<BodyPartVisual>();

        if (BeltSnapObjectAdditions.Count > 1)
        {
            if (instObj.transform.name.Contains("BodyHuman"))
            {
                bodVs.AssignPart(PartGenerator.GeneratePart(PartType.TORSO, VisualPartType.HUMAN_TORSO));
            }
            else if (instObj.transform.name.Contains("BodyZombie"))
            {
                bodVs.AssignPart(PartGenerator.GeneratePart(PartType.TORSO, VisualPartType.ZOMBIE_TORSO));
            }
        }
        else
            bodVs.generateBodyPartVisual();

        snapComp.AssignGameObject(instObj);

        bodVs.ResetRotationsAndTranslations(false, beltObj.transform);

        instObj.transform.position = new Vector3(-20.0f, 0.0f, 0.0f);
    }

    public void RemoveItemFromQueue()
    {
        var toRemoveItem = BeltSlots.FirstOrDefault(x => x.index == m_beltSlotCount);

        var snapComp = toRemoveItem.obj.GetComponent<SnappingPoint>();
        var attachedObj = snapComp.AssignedPart;

        if (ControlledByPlayer)
        {
            gameState.AddBody(attachedObj);
        }

        BeltSlots.Remove(toRemoveItem);
        Destroy(attachedObj);
        Destroy(toRemoveItem.obj);

        if (pauzeAfterRemove)
        {
            paused = true;
            pauzeAfterRemove = false;
        }
    }

    private void Pause()
    {
        paused = true;
    }

    private void Play()
    {
        paused = false;
    }
}