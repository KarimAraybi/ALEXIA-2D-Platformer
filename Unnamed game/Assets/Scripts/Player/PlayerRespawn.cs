using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    [SerializeField]private Transform currentCheckpoint;
    private Health playerHealth;
    private UIManager uiManager;


    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        
    }

    public void Dead()
    {
        uiManager.GameOver();
    }
    
}