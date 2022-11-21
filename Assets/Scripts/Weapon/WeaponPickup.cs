using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public GameObject weapon;

    public GameObject weaponAttach;
    public GameObject weaponFirePoint;

    public float weaponDropForce;

    // Start is called before the first frame update
    void Start()
    {
        weapon = null;

        if (!weaponAttach)
            weaponAttach = GameObject.FindGameObjectWithTag("AttachPoint");

        if (weaponDropForce <= 0)
            weaponDropForce = 10f;

        weaponFirePoint = GameObject.FindGameObjectWithTag("GunSpawn");
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (weapon)
            {
                weaponAttach.transform.DetachChildren();

                StartCoroutine(EnableCollision(2));

                Rigidbody rb = weapon.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.AddForce(weapon.transform.forward * weaponDropForce, ForceMode.Impulse);
            }
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!weapon && hit.gameObject.CompareTag("Weapon"))
        {
            weapon = hit.gameObject;

            if (weapon)
            {
                weapon.GetComponent<Rigidbody>().isKinematic = true;
                weapon.transform.position = weaponAttach.transform.position;
                weapon.transform.SetParent(weaponAttach.transform);
                weapon.transform.localRotation = weaponAttach.transform.localRotation;
                Physics.IgnoreCollision(weapon.gameObject.GetComponent<Collider>(), GetComponent<Collider>(), true);
            }
        }
    }

    IEnumerator EnableCollision(float timeToDisable)
    {
        yield return new WaitForSeconds(timeToDisable);
        Physics.IgnoreCollision(weapon.gameObject.GetComponent<Collider>(), GetComponent<Collider>(), false);
        weapon = null;
    }
}