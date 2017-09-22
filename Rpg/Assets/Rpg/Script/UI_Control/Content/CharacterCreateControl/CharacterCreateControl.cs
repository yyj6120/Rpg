using UnityEngine.UI;
using UnityEngine;
namespace Assets._3.Script.UI_Control.Content.CharacterCreateControl
{
    interface IClassSelectState
    {
        void InputWarriorButton();
        void InputMagicianButton();
        void InputArcherButton();
    }
    class CharacterCreateControl : MonoBehaviour
    {
        #region Variables
        IClassSelectState warriorState;
        IClassSelectState magicianState;
        IClassSelectState archerState;
        IClassSelectState state;

        internal IClassSelectState State
        {
            get
            {
                return state;
            }

            set
            {
                state = value;
            }
        }

        internal IClassSelectState WarriorState
        {
            get
            {
                return warriorState;
            }

            set
            {
                warriorState = value;
            }
        }

        internal IClassSelectState MagicianState
        {
            get
            {
                return magicianState;
            }

            set
            {
                magicianState = value;
            }
        }

        internal IClassSelectState ArcherState
        {
            get
            {
                return archerState;
            }

            set
            {
                archerState = value;
            }
        }
        #endregion
        #region Initialize BaseClass
        private void Start()
        {
            warriorState = new WarriorButton(this);
            magicianState = new MagicionButton(this);
            archerState = new ArcherButton(this);
            State = WarriorState;
            State.InputWarriorButton();
        }
        #endregion
        #region State Method , StateChage
        public void InputWarriorButton()
        {
            State.InputWarriorButton();
        }

        public void InputMagicianButton()
        {
            State.InputMagicianButton();
        }

        public void InputArcherButton()
        {
            State.InputArcherButton();
        }
        public void SetState(IClassSelectState state)
        {
            this.State = state;
        }
        #endregion
    }
}
