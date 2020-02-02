using System;
using UnityEngine;

namespace Logic
{
    public class SFXEventHandler
    {
        public delegate void OnClickPart();

        public static event OnClickPart ClickPartEvent;

        public delegate void OnAttachPart(GameObject target);

        public static event OnAttachPart AttachPartEvent;

        public delegate void OnButtonHover();

        public static event OnButtonHover ButtonHoverEvent;

        public delegate void OnButtonClick();

        public static event OnButtonClick ButtonClickEvent;


        public static void SendClickPart()
        {
            ClickPartEvent?.Invoke();
        }

        public static void SendAttachPart(GameObject target)
        {
            AttachPartEvent?.Invoke(target);
        }

        public static void SendButtonHover()
        {
            ButtonHoverEvent?.Invoke();
        }

        public static void ClickButton()
        {
            ButtonClickEvent?.Invoke();
        }
    }
}