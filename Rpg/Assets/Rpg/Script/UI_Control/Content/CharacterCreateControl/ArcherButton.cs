using UnityEngine.UI;
using UnityEngine;
namespace Assets._3.Script.UI_Control.Content.CharacterCreateControl
{

    class ArcherButton : IClassSelectState
    {
        CharacterCreateControl characterSelectControl;
        GameObject archer;
        Text explanation;
        Text control;
        Text weaponType;
        Sprite symbol;
        Text koreaName;
        Text englishName;
        Color nameColor;

        public ArcherButton(CharacterCreateControl characterSelectControl)
        {
            this.characterSelectControl = characterSelectControl;
            archer = GameObject.Find("characterCreate/archer").gameObject;
            explanation = GameObject.Find("characterCreate/panel/explanation").GetComponent<Text>();
            control = GameObject.Find("characterCreate/panel/control").GetComponent<Text>();
            weaponType = GameObject.Find("characterCreate/panel/weapontype").GetComponent<Text>();
            symbol = Resources.Load<Sprite>("Symbol/archerSybol");
            koreaName = GameObject.Find("characterCreate/header/koreaName").GetComponent<Text>();
            englishName = GameObject.Find("characterCreate/header/englishName").GetComponent<Text>();
            nameColor = new Color(210, 255, 0);
            archer.SetActive(false);
        }
        public void InputWarriorButton()
        {
            archer.SetActive(false);
            characterSelectControl.SetState(characterSelectControl.WarriorState);
            characterSelectControl.State.InputWarriorButton();
        }
        public void InputMagicianButton()
        {
            archer.SetActive(false);
            characterSelectControl.SetState(characterSelectControl.MagicianState);
            characterSelectControl.State.InputMagicianButton();
        }
        public void InputArcherButton()
        {
            archer.SetActive(true);
            ClassInfoOutput();
        }
        public void ClassInfoOutput()
        {
            koreaName.text = "아처";
            englishName.text = "Archer";
            koreaName.color = nameColor;
            englishName.color = nameColor;
            explanation.text = "빠른 몸놀림 신속한 사냥 ,주 공격은 활을 이용한 공격이며 , 근접전에도 유용한 공격을 시전합니다.";
            control.text = "어려움";
            weaponType.text = "활,한손무기";
        }
    }
}
