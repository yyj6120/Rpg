using UnityEngine.UI;
using UnityEngine;
namespace Assets._3.Script.UI_Control.Content.CharacterCreateControl
{
    class MagicionButton : IClassSelectState
    {
        CharacterCreateControl characterSelectControl;
        GameObject magicion;
        Text explanation;
        Text control;
        Text weaponType;
        Sprite symbol;
        Text koreaName;
        Text englishName;
        Color nameColor;

        public MagicionButton(CharacterCreateControl characterSelectControl)
        {
            this.characterSelectControl = characterSelectControl;
            magicion = GameObject.Find("characterCreate/magicion").gameObject;
            explanation = GameObject.Find("characterCreate/panel/explanation").GetComponent<Text>();
            control = GameObject.Find("characterCreate/panel/control").GetComponent<Text>();
            weaponType = GameObject.Find("characterCreate/panel/weapontype").GetComponent<Text>();
            symbol = Resources.Load<Sprite>("Symbol/magicionSybol");
            koreaName = GameObject.Find("characterCreate/header/koreaName").GetComponent<Text>();
            englishName = GameObject.Find("characterCreate/header/englishName").GetComponent<Text>();
            nameColor = new Color(0, 0, 255);
            magicion.SetActive(false);
        }
        public void InputWarriorButton()
        {
            magicion.SetActive(false);
            characterSelectControl.SetState(characterSelectControl.WarriorState);
            characterSelectControl.State.InputWarriorButton();
        }
        public void InputMagicianButton()
        {
            magicion.SetActive(true);
            ClassInfoOutput();
        }
        public void InputArcherButton()
        {
            magicion.SetActive(false);
            characterSelectControl.SetState(characterSelectControl.ArcherState);
            characterSelectControl.State.InputArcherButton();
        }
        public void ClassInfoOutput()
        {
            koreaName.text = "메지션";
            englishName.text = "Magicion";
            koreaName.color = nameColor;
            englishName.color = nameColor;
            explanation.text = "다양한 원소마법을 사용하며 , 생명력은 낮지만 각종 화려한스킬로 적을제압 할수있습니다.";
            control.text = "보통";
            weaponType.text = "완드,스태프";
        }
    }
}
