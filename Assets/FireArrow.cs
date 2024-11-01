using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrow : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Vector2 spawnPoint;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void Shoot()
    {
        if (arrowPrefab != null) {
            spawnPoint = transform.position;

            GameObject arrow = Instantiate(arrowPrefab, spawnPoint, arrowPrefab.transform.rotation);

            ArrowMovement arrowMovement = arrow.GetComponent<ArrowMovement>();
            if (arrowMovement != null) {
                arrowMovement.ActivateMovement();
            }
        }
    }
}
