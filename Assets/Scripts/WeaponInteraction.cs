using System.Collections;
using UnityEngine;

public class WeaponInteraction : MonoBehaviour
{
    private Transform spawnBulletPos;
    private SpriteRenderer spriteRenderer;
    private ItemInteractions itemInteractions;
    private Joystick joystick;
    private Animator animator;
    private AudioSource audioSource;

    private bool allowFire = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spawnBulletPos = transform.GetChild(0).GetChild(0);
        joystick = GameObject.Find("Fire Joystick").GetComponent<Joystick>();
        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        animator = transform.GetChild(0).GetComponent<Animator>();
        itemInteractions = GetComponentInParent<ItemInteractions>();
    }
    private void Update()
    {
        WeaponRotate();
    }
    private IEnumerator Shot(Vector3 joystickRotation) 
    {
        allowFire = false;
        animator.SetTrigger("Shot");
        audioSource.PlayOneShot(AudioManager.shotSound);

        Instantiate(itemInteractions.item.bullet,
            spawnBulletPos.position,
            Quaternion.LookRotation(Vector3.forward, joystickRotation * 180f));

        yield return new WaitForSeconds(itemInteractions.item.fireRate);
        allowFire = true;
    }
    private void WeaponRotate()
    {
        Vector3 joystickRotation = (Vector3.up * joystick.Horizontal + Vector3.left * joystick.Vertical);
        if (itemInteractions.item != null)
        {
            if (joystickRotation != Vector3.zero)
            {
                if (allowFire == true)
                {
                    StartCoroutine(Shot(joystickRotation));
                }
                transform.rotation = Quaternion.LookRotation(Vector3.forward, joystickRotation * 180f);
            }
            else 
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            spriteRenderer.flipY = joystickRotation == Vector3.zero ? false : joystickRotation.y < 0f ? true : false;
            spriteRenderer.sortingOrder = joystickRotation == Vector3.zero ? 2 : 4;
            animator.SetBool("OnTheBack", joystickRotation == Vector3.zero);
        }
    }
}
