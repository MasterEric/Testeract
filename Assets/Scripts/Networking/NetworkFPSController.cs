using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using Random = UnityEngine.Random;

namespace MasterEric.Characters.FirstPerson
{
    [RequireComponent(typeof (CharacterController))]
    public class NetworkFPSController : NetworkBehaviour {
        [SerializeField] private float m_WalkSpeed = 5;
        [SerializeField] [Range(0f, 1f)] private float m_RunstepLenghten;
        [SerializeField] private float m_JumpSpeed;
        [SerializeField] private float m_StickToGroundForce;
        [SerializeField] private float m_GravityMultiplier;
        [SerializeField] private UnityStandardAssets.Characters.FirstPerson.MouseLook m_MouseLook;
        [SerializeField] private float m_StepInterval;

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;

        // Use this for initialization
        private void Start() {
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
			if((!GameStateManager.isSinglePlayer && !isLocalPlayer) || (GameStateManager.isSinglePlayer)) 
				DisableLocalPlayer();
			else {
				m_CharacterController = GetComponent<CharacterController>();
				m_Camera = GetComponentInChildren<Camera>();
				m_MouseLook.Init(transform , m_Camera.transform);
			}
        }

		private void DisableLocalPlayer() {
			Debug.LogWarning("Not local player!");
			GetComponentInChildren<Camera>().enabled = false;
			foreach(AudioListener i in GetComponentsInChildren<AudioListener>()) {
				i.enabled = false;
			}
		}

        // Update is called once per frame
        private void Update() {
			if((!GameStateManager.isSinglePlayer && !isLocalPlayer) || (GameStateManager.isSinglePlayer)) 
				return;
			RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump) {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded) {
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded) {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;
        }

        private void FixedUpdate() {
			if((!GameStateManager.isSinglePlayer && !isLocalPlayer) || (GameStateManager.isSinglePlayer)) 
				return;
			float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               		m_CharacterController.height/2f);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;

            if (m_CharacterController.isGrounded) {
                m_MoveDir.y = -m_StickToGroundForce;

                if (m_Jump) {
                    m_MoveDir.y = m_JumpSpeed;
                    m_Jump = false;
                    m_Jumping = true;
                }
            } else {
                m_MoveDir += Physics.gravity*m_GravityMultiplier*Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            //UpdateCameraPosition(speed);
        }

        private void ProgressStepCycle(float speed) {
			if((!GameStateManager.isSinglePlayer && !isLocalPlayer) || (GameStateManager.isSinglePlayer)) 
				return;
			if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0)) {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed))* Time.fixedDeltaTime;
            }
            if (!(m_StepCycle > m_NextStep)) {
                return;
            }
            m_NextStep = m_StepCycle + m_StepInterval;;
        }

        private void GetInput(out float speed) {
			speed = m_WalkSpeed;			
			if((!GameStateManager.isSinglePlayer && !isLocalPlayer) || (GameStateManager.isSinglePlayer)) 
				return;
			// Read input
            float horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
            float vertical = CrossPlatformInputManager.GetAxis("Vertical");

			#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
			#endif

            // set the desired speed to be walking or running
			Debug.Log ("Look:"+horizontal+":"+vertical);
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1) {
                m_Input.Normalize();
            }
        }

        private void RotateView() {
			if((!GameStateManager.isSinglePlayer && !isLocalPlayer) || (GameStateManager.isSinglePlayer)) 
	        	return;
			else {
				m_MouseLook.LookRotation (transform, m_Camera.transform);
			}
        }

        private void OnControllerColliderHit(ControllerColliderHit hit) {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below) {
                return;
            }
            if (body == null || body.isKinematic) {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
}
