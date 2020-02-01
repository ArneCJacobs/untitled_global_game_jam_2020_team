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
    
    public struct BeltObject
    {
        public GameObject obj;
        public Part part;
    }

    QueueManager queueman;

    // Start is called before the first frame update
    void Start()
    {
        m_maxDist = (BeltLength * 2) / ItemAmount;
        queueman = new QueueManager(ItemAmount);

        for (int i = 0; i < ItemAmount; i++)
        {
            queueman.BeltSlots[i] = GameObject.Instantiate(BeltSnapObject);
            queueman.BeltSlots[i].transform.parent = gameObject.transform;
            //queueman.BeltSlots[i].GetComponent<Transform>().position += new Vector3(, YOffset, 0);
            queueman.BeltSlots[i].GetComponent<Transform>().position += new Vector3((i * m_maxDist) - BeltLength, 0, 0);
        }

        for (int i =0; i < 20; i++)
        {
            queueman.AddItemToQueue(PartGenerator.GeneratePart());
        }

        queueman.AdvanceQueue(ItemAmount);
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

        foreach (var item in queueman.BeltSlots)
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
        queueman.AdvanceQueue();
    }
      
    public class QueueManager
    {
        public GameObject[] BeltSlots;
        List<Part> QueuedPartList = new List<Part>();

        List<Part> PartList = new List<Part>();

        int m_counter = 0;

        public QueueManager(int beltSlotCount)
        {
            BeltSlots = new GameObject[beltSlotCount];
        }

        public void AdvanceQueue(int steps = 1)
        {
            for (int i = 0; i < steps; i++)
            {
                var furthest = BeltSlots[m_counter];
                var snapComp = furthest.GetComponent<SnappingPoint>();
                //if (snapComp.AssignedPart == null)
                //    GameObject.Destroy(snapComp.AssignedPart);

                if (snapComp.AssignedPart == null)
                    AddItemToQueue(m_counter);

                i_advanceCounter();
            }
        }

        public GameObject GetItemInSlot(int id)
        {
            if (id >= 0 && id < BeltSlots.Length)
            {
                var snapComp = BeltSlots[id].GetComponent<SnappingPoint>();
                return snapComp.AssignedPart;
            }
            return null;
        }

        public void AddItemToQueue(int id)
        {
            var nextItem = QueuedPartList.FirstOrDefault();
            if (nextItem == null)
                return;
            else
            {
                var obj = Resources.Load("Prefabs/BodyPart") as GameObject;
                var beltSlot = BeltSlots[id];
                var snapComp = BeltSlots[id].GetComponent<SnappingPoint>();

                var comp = obj.GetComponent<ClickDragTest>();
                comp.snappedTo = beltSlot;
                comp.startSnappedTo = beltSlot;
                var bodVs = obj.GetComponent<BodyPartVisual>();
                GameObject.Instantiate(obj);
                snapComp.AssignGameObject(obj);

                bodVs.ResetRotationsAndTranslations(false, beltSlot.transform);
            }
            QueuedPartList.Remove(nextItem);
        }

        public void AddItemToQueue(Part part)
        {
            QueuedPartList.Add(part);
        }

        private void i_advanceCounter()
        {
            if (m_counter + 1 >= BeltSlots.Length)
                m_counter = 0;
            else
                m_counter++;
        }
    }
}
