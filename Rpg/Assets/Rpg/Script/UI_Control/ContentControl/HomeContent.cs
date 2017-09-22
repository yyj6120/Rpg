using UnityEngine;
namespace Assets._3.Script.UI_Control.ContentControl
{
    class HomeContent : IContent
    {
        ContentControl contentControl;
        GameObject home;
        public HomeContent(ContentControl contentControl)
        {
            this.contentControl = contentControl;
            home = GameObject.Find("contentTab/content/home").gameObject;
            home.SetActive(false);
        }
        public void InputHome()
        {
            home.SetActive(true);
        }
        public void InputCharacterSelect()
        {
            home.SetActive(false);
            contentControl.SetContent(contentControl.CharterSelect);
            contentControl.Content.InputCharacterSelect();
        }
        public void InputBaseEquipment()
        {
            home.SetActive(false);
            contentControl.SetContent(contentControl.BaseEquipment);
            contentControl.Content.InputBaseEquipment();
        }
        public void InputCharacterCreate()
        {
            home.SetActive(false);
            contentControl.SetContent(contentControl.CharacterCreate);
            contentControl.Content.InputCharacterCreate();
        }

    }
}
