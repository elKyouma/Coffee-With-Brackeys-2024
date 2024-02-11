using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using KinematicCharacterController;
using KinematicCharacterController.Examples;
using System;

namespace KinematicCharacterController.Examples
{
    public class Player : MonoBehaviour
    {
        private MyCharacterController character;
        private MyCharacterCamera characterCamera;

        private Vector2 movement;
        private Vector2 cameraMovement;

        public static event Action OnInteraction;
        public static event Action OnItemUseEvent;
 

        private void Start()
        {
            character = GetComponentInChildren<MyCharacterController>();
            characterCamera = GetComponentInChildren<MyCharacterCamera>();

            Cursor.lockState = CursorLockMode.Locked;

            // Tell camera to follow transform
            characterCamera.SetFollowTransform(character.CameraFollowPoint);

            // Ignore the character's collider(s) for camera obstruction checks
            characterCamera.IgnoredColliders.Clear();
            characterCamera.IgnoredColliders.AddRange(character.GetComponentsInChildren<Collider>());
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            HandleCharacterInput();
        }

        private void LateUpdate()
        {
            // Handle rotating the camera along with physics movers
            if (characterCamera.RotateWithPhysicsMover && character.Motor.AttachedRigidbody != null)
            {
                characterCamera.PlanarDirection = character.Motor.AttachedRigidbody.GetComponent<PhysicsMover>().RotationDeltaFromInterpolation * characterCamera.PlanarDirection;
                characterCamera.PlanarDirection = Vector3.ProjectOnPlane(characterCamera.PlanarDirection, character.Motor.CharacterUp).normalized;
            }

            HandleCameraInput();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            movement = context.ReadValue<Vector2>();
        }

        public void OnCameraMove(InputAction.CallbackContext context)
        {
            cameraMovement = context.ReadValue<Vector2>();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.ReadValue<float>() > 0.5f) OnInteraction?.Invoke();
        }

        public void OnItemUse(InputAction.CallbackContext context)
        {
            if (context.ReadValue<float>() > 0.5f) OnItemUseEvent?.Invoke();
        }

        private void HandleCameraInput()
        {
            // Create the look input vector for the camera
            float mouseLookAxisUp = cameraMovement.y;
            float mouseLookAxisRight = cameraMovement.x;
            Vector3 lookInputVector = new Vector3(mouseLookAxisRight, mouseLookAxisUp, 0f);

            // Prevent moving the camera while the cursor isn't locked
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                lookInputVector = Vector3.zero;
            }


            // Input for zooming the camera (disabled in WebGL because it can cause problems)
            // Apply inputs to the camera
            //float scrollInput = -Input.GetAxis(MouseScrollInput);
            characterCamera.UpdateWithInput(Time.deltaTime, 0f, lookInputVector);

            // Handle toggling zoom level
            //if (Input.GetMouseButtonDown(1))
            //{
            //    characterCamera.TargetDistance = (characterCamera.TargetDistance == 0f) ? characterCamera.DefaultDistance : 0f;
            //}
        }

        private void HandleCharacterInput()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            // Build the CharacterInputs struct
            characterInputs.MoveAxisForward = movement.y;
            characterInputs.MoveAxisRight = movement.x;
            characterInputs.CameraRotation = characterCamera.Transform.rotation;
            characterInputs.JumpDown = Input.GetKeyDown(KeyCode.Space);
            characterInputs.CrouchDown = Input.GetKeyDown(KeyCode.C);
            characterInputs.CrouchUp = Input.GetKeyUp(KeyCode.C);

            // Apply inputs to character
            character.SetInputs(ref characterInputs);
        }
    }
}