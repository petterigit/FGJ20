using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAudioController : MonoBehaviour
{
    public AudioSource walkingLoop;
    public AudioSource walkingLoopFast;
    public AudioSource swimmingLoop;
    public AudioSource sawingLoop;

    public AudioSource comboRight;
    public AudioSource comboWrong;

    public AudioSource pickup;
    public AudioSource placedown;
    public AudioSource swing;
    public AudioSource hit;

    public AudioSource dash;

    public enum State { walking, running, swimming, sawing, stop };

    private State state;

    public void PlayCorrect() {
        comboRight.PlayOneShot(comboRight.clip);
    }

    public void PlayFalse() {
        comboWrong.PlayOneShot(comboWrong.clip);
    }

    public void PlayPickup() {
        pickup.PlayOneShot(pickup.clip);
    }

    public void PlayPlaceDown() {
        placedown.PlayOneShot(placedown.clip);
    }

    public void PlaySwing() {
        swing.PlayOneShot(swing.clip);
    }

    public void PlayHit() {
        hit.PlayOneShot(hit.clip);
    }

    public void PlayDash() {
        dash.PlayOneShot(dash.clip);
    }

    public void SetSoundState(State s) {
        state = s;
        switch(state) {
            case State.walking:
                
                walkingLoopFast.Stop();
                swimmingLoop.Stop();
                sawingLoop.Stop();
                if(!walkingLoop.isPlaying) {
                    //Debug.Log("walking");
                    walkingLoop.Play();
                }
                break;

            case State.running:
                if(!walkingLoopFast.isPlaying) {
                    Debug.Log("Running");
                    walkingLoopFast.Play();
                } 
                swimmingLoop.Stop();
                sawingLoop.Stop();
                walkingLoop.Stop();
                break;

            case State.swimming:
                walkingLoopFast.Stop();
                if(!swimmingLoop.isPlaying) {
                   //Debug.Log("Swimming");
                    swimmingLoop.Play();
                }
                sawingLoop.Stop();
                walkingLoop.Stop();
                break;

            case State.sawing:
                walkingLoopFast.Stop();
                swimmingLoop.Stop();
                if(!sawingLoop.isPlaying) {
                    //Debug.Log("Sawing");
                    sawingLoop.Play();
                }
                walkingLoop.Stop();
                break;

            case State.stop:
                //Debug.Log("Stop");
                walkingLoopFast.Stop();
                swimmingLoop.Stop();
                sawingLoop.Stop();
                walkingLoop.Stop();
                break;

            default:
                //Debug.Log("Default");
                walkingLoopFast.Stop();
                swimmingLoop.Stop();
                sawingLoop.Stop();
                walkingLoop.Stop();
                break;

        }
    }
}
