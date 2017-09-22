using UnityEngine;
using Assets._3.Script.UI_Control.Content.CharacterCreateControl;
namespace Assets._3.Script.UI_Control.ContentControl
{
    class CharacterCreateContent : IContent
    {
        ContentControl contentControl;
        CharacterCreateControl characterCreateControl;
        public CharacterCreateContent(ContentControl contentControl)
        {
            this.contentControl = contentControl;
            characterCreateControl = GameObject.Find("characterCreate").GetComponent<CharacterCreateControl>();
            characterCreateControl.gameObject.SetActive(false);
        }
        public void InputHome()
        {
            characterCreateControl.gameObject.SetActive(false);
            contentControl.SetContent(contentControl.Home);
            contentControl.Content.InputHome();
        }
        public void InputCharacterSelect()
        {
            characterCreateControl.gameObject.SetActive(false);
            contentControl.SetContent(contentControl.CharterSelect);
            contentControl.Content.InputCharacterSelect();
        }
        public void InputBaseEquipment()
        {
            characterCreateControl.gameObject.SetActive(false);
            contentControl.SetContent(contentControl.BaseEquipment);
            contentControl.Content.InputBaseEquipment();
        }
        public void InputCharacterCreate()
        {
            characterCreateControl.gameObject.SetActive(true);
        }
    }
}
