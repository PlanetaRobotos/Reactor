// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class DarkOrbScript : MonoBehaviour
// {
//     private void OnCollisionEnter(Collision collision)
//     {
//         Debug.Log("Dark : " + gameObject.name + " | " + collision.gameObject.name);
//
//         if (collision.gameObject.tag == "Orb")
//         {
//             ContactPoint contact = collision.contacts[0];
//             Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
//             Vector3 pos = contact.point;
//
//             GameObject newFX_Violet = Instantiate(_gameMaster.WallHitFXs[4], pos, rot);
//             newFX_Violet.SetActive(true);
//
//             _gameMaster.DarkHit();
//         }
//     }
//
// }
