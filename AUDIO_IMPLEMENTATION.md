# Audio Implementation Guide

## Overview

This project uses **Audiokinetic Wwise** as the audio middleware, integrated with Unity. Wwise provides advanced audio management, including soundbanks, events, and real-time parameter control, enabling a flexible and scalable solution for game audio.

---

## 1. Wwise Integration

- Wwise Unity Integration is present in the project (see `Assets/Wwise/` and related folders).
- Wwise soundbanks and events are managed via the Wwise Authoring Application and generated into the Unity project.
- Audio assets (e.g., SFX, music) are stored in `ccl4_WwiseProject/Originals/`.

---

## 2. Soundbanks

- Soundbanks are generated in the Wwise Authoring Application and imported into Unity under `ccl4_WwiseProject/GeneratedSoundBanks/`.
- Each platform (Mac, Windows) has its own soundbank folder.
- Soundbanks must be updated and regenerated in Wwise whenever new audio assets or events are added.

---

## 3. Triggering Sounds in Unity

### Via Wwise Components 

- Attach Wwise components (e.g., `AkEvent`, `AkAmbient`, `AkAudioListener`, `AkGameObj`) to GameObjects in the Unity Editor.
- Assign the appropriate Wwise Event (e.g., `Play_Coin`, `Play_Jump`, `Play_Music`) to the component.
- Sounds will trigger automatically based on the component's configuration (e.g., on collision, on trigger, on start).

### Via Script (If Needed)

To trigger Wwise events from code, use:

```csharp
AkSoundEngine.PostEvent("EventName", gameObject);
```
Replace `"EventName"` with the name of your Wwise event.

Example: To play a coin collection sound when the player collects a coin:

```csharp
AkSoundEngine.PostEvent("Play_Coin", gameObject);
```

Make sure the GameObject has an `AkGameObj` component attached.

---

## 4. Adding New Sounds

1. **Import Audio Files:** Add new audio files to `ccl4_WwiseProject/Originals/SFX/` or the appropriate folder.
2. **Create Events in Wwise:** In the Wwise Authoring Application, create new events for your sounds.
3. **Generate Soundbanks:** Regenerate soundbanks in Wwise and ensure they are imported into Unity.
4. **Assign Events in Unity:** Attach Wwise event components to GameObjects or trigger them via script as needed.

---

## 5. Music and Ambience

- Background music and ambience can be managed via Wwise Music Segments and States.
- Use `AkMusicSyncCallbackInfo` and related Wwise music features for advanced music control.

---

## 6. Testing and Debugging

- Use the Wwise Profiler and Unity Console for debugging audio issues.
- Ensure soundbanks are up to date and all required events are present in the Unity Editor.

---

## 7. References

- [Wwise Unity Integration Documentation](https://www.audiokinetic.com/library/edge/?source=Unity)
- [Wwise Authoring Documentation](https://www.audiokinetic.com/library/edge/?source=Help)

---

*For questions or onboarding, contact the audio lead or refer to the above documentation links.* 