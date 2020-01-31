using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GUI
{
    public class UnityGuiImage : MonoBehaviour
    {
        GuiBaseElement m_img = new GuiBaseElement();
        Image Image { get; set; }

        public int Width;
        public int Height;

        // Start is called before the first frame update
        void Start()
        {
            runInEditMode = true;
            Image = gameObject.GetComponent<Image>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateSizeAndPosition();
        }

        private void UpdateSizeAndPosition()
        {
            m_img.Rect.Width = Width;
            m_img.Rect.Height = Height;
            m_img.Update();
            var rectComp = Image.GetComponent<RectTransform>();
            rectComp.SetPositionAndRotation(new Vector3(m_img.Rect.GetXPosition(), m_img.Rect.GetYPosition()), Quaternion.identity);
            rectComp.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, m_img.Rect.Width);
            rectComp.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, m_img.Rect.Height);
        }
    }
}
