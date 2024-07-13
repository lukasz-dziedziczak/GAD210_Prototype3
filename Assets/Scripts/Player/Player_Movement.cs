using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    Player player;
    [SerializeField] float movementSpeed;
    [SerializeField] float lookSpeed;
    [SerializeField] float maxRotationAngle = 90f;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Start()
    {
        player.Camera.transform.localEulerAngles = Vector3.zero;
        player.transform.localEulerAngles = Vector3.zero;
    }

    private void FixedUpdate()
    {
        if (UI.MenuOpen) return;

        if (player.Input.Movement.magnitude > 0)
        {
            Vector3 forwardMovement = player.Input.Movement.y * transform.forward * movementSpeed * Time.deltaTime;
            Vector3 rightMovement = player.Input.Movement.x * transform.right * movementSpeed * Time.deltaTime;

            player.Rigidbody.MovePosition(transform.position + forwardMovement + rightMovement);
        }

        if (player.Input.Look.x != 0)
        {
            Vector3 eulerRot = transform.eulerAngles;
            eulerRot.y += player.Input.Look.x * lookSpeed * Time.deltaTime;
            Quaternion rotation = Quaternion.Euler(eulerRot.x, eulerRot.y, eulerRot.z);

            player.Rigidbody.MoveRotation(rotation);
        }

        if (player.Input.Look.y != 0)
        {
            Quaternion camRotation = player.Camera.transform.localRotation;
            Vector3 camRotEuler = camRotation.eulerAngles;
            camRotEuler.x += -player.Input.Look.y * lookSpeed * Time.deltaTime;
            if (camRotEuler.x >= 180 && camRotEuler.x < 360 - maxRotationAngle)
            {
                camRotEuler.x = 360 - maxRotationAngle;
            }
            else if (camRotEuler.x < 180 && camRotEuler.x > maxRotationAngle)
            {
                camRotEuler.x = maxRotationAngle;
            }
            else if (camRotEuler.x > maxRotationAngle && camRotEuler.x < 360 - maxRotationAngle)
            {
                camRotEuler.x = 0;
            }
            player.Camera.transform.localRotation = Quaternion.Euler(camRotEuler.x, camRotEuler.y, camRotEuler.z);
        }
    }
}
