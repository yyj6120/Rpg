using UnityEngine;
namespace Assets._3.Script.UI_Control.ContentControl
{
    interface IContent
    {
        void InputHome();
        void InputCharacterSelect();
        void InputBaseEquipment();
        void InputCharacterCreate();
    }
    class ContentControl : MonoBehaviour
    {
        #region Variables
        IContent home;
        IContent charterSelect;
        IContent baseEquipment;
        IContent characterCreate;
        IContent content;

        internal IContent Home
        {
            get
            {
                return home;
            }

            set
            {
                home = value;
            }
        }

        internal IContent CharterSelect
        {
            get
            {
                return charterSelect;
            }

            set
            {
                charterSelect = value;
            }
        }

        internal IContent BaseEquipment
        {
            get
            {
                return baseEquipment;
            }

            set
            {
                baseEquipment = value;
            }
        }

        internal IContent CharacterCreate
        {
            get
            {
                return characterCreate;
            }

            set
            {
                characterCreate = value;
            }
        }

        internal IContent Content
        {
            get
            {
                return content;
            }

            set
            {
                content = value;
            }
        }
        #endregion
        #region Initialize Content
        private void Awake()
        {
            Home = new HomeContent(this);
            CharterSelect = new CharaterSelectContent(this);
            BaseEquipment = new BaseEquipMentContent(this);
            CharacterCreate = new CharacterCreateContent(this);
            Content = Home;
            Content.InputHome();
        }
        #endregion
        #region Content State , Content Change
        public void InputHome()
        {
            Content.InputHome();
        }
        public void InputCharacterSelect()
        {
            Content.InputCharacterSelect();
        }
        public void InputBaseEquipment()
        {
            Content.InputBaseEquipment();
        }
        public void InputCharacterCreate()
        {
            Content.InputCharacterCreate();
        }
        public void SetContent(IContent content)
        {
            this.Content = content;
        }
        #endregion
    }
}
