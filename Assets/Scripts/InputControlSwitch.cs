using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputControlSwitch : MonoBehaviour
{
    public static InputControlSwitch Instance;

    //FPS Controls
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject playerCanvas;


    //RCC Controls
    [SerializeField] private GameObject rcc;
    [SerializeField] private GameObject rccCamera;
    [SerializeField] private GameObject rccCanvas;
    [SerializeField] private GameObject rccCineMachine;

    public enum SetPlayer { FPS, RCC};

    public SetPlayer setPlayer;

    private void Awake()
    {
        Instance = this;
        setPlayer = SetPlayer.FPS;
    }
    private void Start()
    {
        SwitchPlayer(setPlayer);
    }

    void ControllerDefaultState()
    {

        player.SetActive(false);
        playerCamera.SetActive(false);
        playerCanvas.SetActive(false);
        rccCamera.SetActive(false);
        rccCanvas.SetActive(false);
        rccCineMachine.SetActive(false);
    }

    void EnableFPC()
    {
        setPlayer = SetPlayer.FPS;
        player.SetActive(true);
        playerCamera.SetActive(true);
        playerCanvas.SetActive(true);
    }
    void EnableRCC()
    {
        setPlayer = SetPlayer.RCC;
        rccCamera.SetActive(true);
        rccCanvas.SetActive(true);
        rccCineMachine.SetActive(true);
    }
    public void SwitchPlayer(SetPlayer player)
    {
        ControllerDefaultState();
        if (player == SetPlayer.FPS) EnableFPC();
        if (player == SetPlayer.RCC) EnableRCC();
    }

    public void SwitchToFPS()
    {
        rcc.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        ControllerDefaultState();
        player.transform.position = rcc.transform.position + Vector3.left * 2.5f;
        if (setPlayer == SetPlayer.RCC) EnableFPC();
        Invoke(nameof(RemoveConstraints), 1.0f);
    }

    void RemoveConstraints()
    {
        rcc.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
}
