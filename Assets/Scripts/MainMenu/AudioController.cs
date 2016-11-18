using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

    //Attached to slider to control the volume of the music
    public void VolumeControl(float volumeControl) {
        AudioListener.volume = volumeControl;
    }
}
