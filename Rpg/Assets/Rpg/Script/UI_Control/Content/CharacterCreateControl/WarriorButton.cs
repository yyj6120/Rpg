using UnityEngine.UI;
using UnityEngine;
namespace Assets._3.Script.UI_Control.Content.CharacterCreateControl
{
    class WarriorButton : IClassSelectState
    {
        CharacterCreateControl characterSelectControl;
        GameObject warrior;
        Text explanation;
        Text control;
        Text weaponType;
        Sprite symbol;
        Text koreaName;
        Text englishName;
        Color nameColor;

        public WarriorButton(CharacterCreateControl characterSelectControl)
        {
            this.characterSelectControl = characterSelectControl;
            warrior = GameObject.Find("characterCreate/warrior").gameObject;
            explanation = GameObject.Find("characterCreate/panel/explanation").GetComponent<Text>();
            control = GameObject.Find("characterCreate/panel/control").GetComponent<Text>();
            weaponType = GameObject.Find("characterCreate/panel/weapontype").GetComponent<Text>();
            symbol = Resources.Load<Sprite>("Symbol/warriorSybol");
            koreaName = GameObject.Find("characterCreate/header/koreaName").GetComponent<Text>();
            englishName = GameObject.Find("characterCreate/header/englishName").GetComponent<Text>();
            nameColor = new Color(255, 0, 0);
            warrior.SetActive(false);
        }
        public void InputWarriorButton()
        {
            warrior.SetActive(true);
            ClassInfoOutput();
        }
        public void InputMagicianButton()
        {
            warrior.SetActive(false);
            characterSelectControl.SetState(characterSelectControl.MagicianState);
            characterSelectControl.State.InputMagicianButton();
        }
        public void InputArcherButton()
        {
            warrior.SetActive(false);
            characterSelectControl.SetState(characterSelectControl.ArcherState);
            characterSelectControl.State.InputArcherButton();
        }
        public void ClassInfoOutput()
        {
            koreaName.text = "워리어";
            englishName.text = "Warrior";
            koreaName.color = nameColor;
            englishName.color = nameColor;
            explanation.text = "강력한 연속기로 적을공격합니다. 생명력증가 , 물약효율 증가와 같은 생존기를 익힐 수 있습니다.";
            control.text = "쉬움";
            weaponType.text = "두손무기 , 한손무기 , 방패";
        }
    }
}
