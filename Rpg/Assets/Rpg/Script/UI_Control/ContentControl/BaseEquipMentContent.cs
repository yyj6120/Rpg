using UnityEngine;
namespace Assets._3.Script.UI_Control.ContentControl
{
    class BaseEquipMentContent : IContent
    {
        ContentControl contentControl;
        GameObject baseEquipment;
        public BaseEquipMentContent(ContentControl contentControl)
        {
            this.contentControl = contentControl;
            baseEquipment = GameObject.Find("contentTab/content/baseEquipment").gameObject;
            baseEquipment.SetActive(false);
        }
        public void InputHome()
        {
            baseEquipment.SetActive(false);
            contentControl.SetContent(contentControl.Home);
            contentControl.Content.InputHome();
        }

        public void InputCharacterSelect()
        {
            baseEquipment.SetActive(false);
            contentControl.SetContent(contentControl.CharterSelect);
            contentControl.Content.InputCharacterSelect();
        }

        public void InputBaseEquipment()
        {
            baseEquipment.SetActive(true);
        }

        public void InputCharacterCreate()
        {
            baseEquipment.SetActive(false);
            contentControl.SetContent(contentControl.CharacterCreate);
            contentControl.Content.InputCharacterCreate();
        }
    }
}
