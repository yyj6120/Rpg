using UnityEngine;

namespace Rpg.Character
{
    public class ThirdPersonInput : MonoBehaviour
    {
        [System.Serializable]
        public class OnUpdateEvent : UnityEngine.Events.UnityEvent<ThirdPersonInput> { }
        public enum GameplayInputStyle
        {
            ClickAndMove,
            DirectionalInput
        }
        public GameplayInputStyle gameplayInputStyle = GameplayInputStyle.ClickAndMove;
        protected InputDevice inputDevice
        {
            get
            {
                return InputControl.Instance.InputType;
            }
        }
        protected vThirdPersonCamera tpCamera;
        public LayerMask clickMoveLayer = 1 << 0;
        [Header("Default Inputs")]
        public GenericInput horizontalInput = new GenericInput("Horizontal");
        public GenericInput verticallInput = new GenericInput("Vertical");
        public GenericInput leftMouseInput = new GenericInput("Fire1");
        public GenericInput rightMouseInput = new GenericInput("Fire2");
        public GenericInput jumpInput = new GenericInput("Space");

        public bool lockInput;
        [HideInInspector]
        public Vector3 cursorPoint;

        public ThirdPersonController character;
        [HideInInspector]
        public bool lockInputByItemManager;
        [HideInInspector]
        public OnUpdateEvent onUpdateInput = new OnUpdateEvent();

        protected bool isAttacking;

        public virtual bool lockInventory
        {
            get
            {
                return isAttacking || character.isDead;
            }
        }

        protected virtual void Start()
        {
            character = GetComponent<ThirdPersonController>();
            CharacterInit();
        }

        protected virtual void LateUpdate()
        {
            if (character == null || lockInput || Time.timeScale == 0)
            {
                return;
            }
            InputHandle();
        }

        protected virtual void FixedUpdate()
        {
            MoveToPoint();
            character.AirControl();
            character.UpdateTargetDirection();
        }

        protected virtual void Update()
        {
            character.UpdateMotor();
            character.UpdateAnimator();
        }

        protected virtual void CharacterInit()
        {
            if (character != null)
            {
                character.Init();
            }

            tpCamera = FindObjectOfType<vThirdPersonCamera>();
            if (tpCamera)
                tpCamera.SetMainTarget(this.transform);

            cursorPoint = transform.position;
        }

        protected virtual void InputHandle()
        {
            MoveCharacter();
            JumpInput();
        }

        protected virtual void MoveCharacter()
        {
            if (gameplayInputStyle == GameplayInputStyle.ClickAndMove)
            {
                ClickAndMove();
            }
        }

        protected virtual void ClickAndMove()
        {
            RaycastHit hit;

            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, clickMoveLayer))
                {
                    cursorPoint = hit.point;
                }
            }
        }

        protected virtual void MoveToPoint()
        {
            if (gameplayInputStyle != GameplayInputStyle.ClickAndMove) return;

            var dir = (cursorPoint - transform.position).normalized;

            if (!NearPoint(cursorPoint, transform.position))
            {
                character.input = new Vector2(dir.x, dir.z);
            }
            else
            {
                //if (onDisableCursor != null)
                //    onDisableCursor();

                character.input = Vector2.Lerp(character.input, Vector3.zero, 20f * Time.deltaTime);
            }
        }

        protected virtual bool NearPoint(Vector3 a, Vector3 b)
        {
            var _a = new Vector3(a.x, transform.position.y, a.z);
            var _b = new Vector3(b.x, transform.position.y, b.z);
            return Vector3.Distance(_a, _b) <= 0.5f;
        }

        protected virtual void JumpInput()
        {
            if (jumpInput.GetButtonDown())
            {
                character.Jump();
            }
        }

        protected virtual void OnTriggerStay(Collider other)
        {
            character.CheckTriggers(other);
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            character.CheckTriggerExit(other);
        }
    }
}
