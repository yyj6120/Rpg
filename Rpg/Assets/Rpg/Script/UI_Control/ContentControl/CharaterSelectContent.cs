using UnityEngine;
namespace Assets._3.Script.UI_Control.ContentControl
{
    class CharaterSelectContent : IContent
    {
        ContentControl contentControl;
        GameObject characterSelect;
        public CharaterSelectContent(ContentControl contentControl)
        {
            this.contentControl = contentControl;
            characterSelect = GameObject.Find("contentTab/content/characterSelect").gameObject;
            characterSelect.SetActive(false);
        }
        public void InputHome()
        {
            characterSelect.SetActive(false);
            contentControl.SetContent(contentControl.Home);
            contentControl.Content.InputHome();
        }
        public void InputCharacterSelect()
        {
            characterSelect.SetActive(true);
        }
        public void InputBaseEquipment()
        {
            characterSelect.SetActive(false);
            contentControl.SetContent(contentControl.BaseEquipment);
            contentControl.Content.InputBaseEquipment();
        }
        public void InputCharacterCreate()
        {
            characterSelect.SetActive(false);
            contentControl.SetContent(contentControl.CharacterCreate);
            contentControl.Content.InputCharacterCreate();
        }
    }
}
