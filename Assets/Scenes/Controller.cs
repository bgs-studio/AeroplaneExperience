using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class Controller : MonoBehaviour
{
    // Configuration
    public float InAirTimeExperience = 164f;
    public TeleportationAnchor teleport;
    public VideoPlayer player,landingPlayer;
    public VideoClip takeoff, landing;
    public Transform waitingRoom, Plane;
    public GameObject landingScreen,TakeOffScreen;
    // Start the takeoff sequence
    public void StartTakeOff()
    {
        Debug.Log("Starting takeoff...");
        // player.clip = takeoff;
        player.Play();
        TakeOffScreen.SetActive(true);
        landingScreen.SetActive(false);
        // Ensure teleportation is set to Plane position
        teleport.teleportAnchorTransform = Plane;
        
        // Start timer for teleportation
        StartCoroutine(StartTimer());
    }

    // Start the landing sequence
    public void StartLanding()
    {
        Debug.Log("Starting landing...");
        // player.clip = landing;
        landingPlayer.Play();
        TakeOffScreen.SetActive(false);
        landingScreen.SetActive(true);
        // Ensure teleportation is set to Plane position
        teleport.teleportAnchorTransform = Plane;
        
        // Start timer for teleportation
        StartCoroutine(StartTimer());
    }

    // Timer to manage teleportation
    IEnumerator StartTimer()
    {
        // Request teleportation to the Plane
        teleport.RequestTeleport();
        
        // Wait for the specified air time duration before changing to waitingRoom
        yield return new WaitForSeconds(InAirTimeExperience);

        // After the air time, teleport to waiting room
        teleport.teleportAnchorTransform = waitingRoom;
        
        // Stop the video playback
        player.Stop();
        landingPlayer.Stop();
        // Call Over() method
        Over();
    }

    // Final teleportation action to the waiting room
    private void Over()
    {
        Debug.Log("Teleporting to waiting room...");
        teleport.RequestTeleport();
    }

    // Method to make sure the video plays correctly on the device
    private void EnsureVideoPlayerIsReady()
    {
        if (player != null)
        {
            Debug.Log("VideoPlayer is ready to play.");
            if (!player.isPlaying)
            {
                Debug.LogWarning("Video is not playing, attempting to start.");
                player.Play();
            }
        }
        else
        {
            Debug.LogError("VideoPlayer component is not assigned!");
        }
    }

    // Ensure that VideoPlayer works for both editor and device
    private void OnEnable()
    {
        EnsureVideoPlayerIsReady();
    }
}