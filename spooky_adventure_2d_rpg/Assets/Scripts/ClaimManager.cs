using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Thirdweb;
using System.Threading.Tasks;

public class ClaimManager : MonoBehaviour
{
    public GameObject goalCanvas;

    private PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player won");

            var animator = other.GetComponent<Animator>();
            animator.SetBool("isMoving", false);

            playerController = other.GetComponent<PlayerController>();
            playerController.isMoving = false;
            playerController.enabled = false;

            goalCanvas.SetActive(true);
        }
    }

    public async Task ClaimNFT()
    {
        Contract contract = ThirdwebManager.Instance.SDK.GetContract("0xAac04471056308E3859E098D1FD017A7bc9dE1Ca");
        await contract.ERC721.Claim(1);
    }

    public async void Claim()
    {
        await ClaimNFT();
        
        goalCanvas.SetActive(false);

        if(playerController != null)
        {
            playerController.enabled = true;
        }

    }
}
