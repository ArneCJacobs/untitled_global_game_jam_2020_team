using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GUI
{
    public class UnityGuiImage : MonoBehaviour
    {
        GuiImage m_img = new GuiImage();
        Image Image { get; set; }

        public int Width, Height;

        // Start is called before the first frame update
        void Start()
        {
            
            Image = gameObject.GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            m_img.Rect.Width = Width;
            m_img.Rect.Height = Height;
            m_img.Update();
            var rectComp = Image.GetComponent<Transform>();
            rectComp.SetPositionAndRotation(new Vector3(m_img.Rect.GetXPosition(), m_img.Rect.GetYPosition()), Quaternion.identity);
        }
    }
}
