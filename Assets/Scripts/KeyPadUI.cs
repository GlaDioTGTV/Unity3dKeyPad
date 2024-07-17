namespace TGTV.KeyPadLock
{
    using System.Collections;
    using UnityEngine;
    using TMPro;
    using UnityEngine.Events;

    
    // Represents a digital keypad UI for code entry and validation.
    public class KeyPadUI : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private int desiredCodeLength = 4; // Scalability: Allow setting desired code length
        [SerializeField] private string _correctCode = "0000"; // Uses property instead of field for encapsulation
        public bool debugMode = true;   // Enables logging for debugging

        [Header("Audio")]
        [SerializeField] private AudioClip clickSound;
        [SerializeField] private AudioClip digitSound;
        [SerializeField] private AudioClip errorSound;
        [SerializeField] private AudioClip unlockedSound;
        [SerializeField] private float clickSoundVolume = 0.3f;
        [SerializeField] private float digitSoundVolume = 0.5f;
        [SerializeField] private float errorSoundVolume = 0.5f;
        [SerializeField] private float unlockedSoundVolume = 0.5f;

        [Header("Events")]
        public UnityEvent OnValidCodeEntered;    // Event triggered when correct code is entered
        public UnityEvent OnChangingCodeEntered;    // Event triggered when incorrect code is entered

        private TMP_InputField digitInput;
        private TMP_Text placeholder;
        private AudioSource audioSource;

        private int codeIndex = 0;
        private string enteredCode = "";

        // Gets the state of the keypad changing code.
        public bool IsChangingCode { get; private set; } = false;

        // Gets the activity state of the keypad.
        public bool IsActive { get; private set; } = false;

        // Gets the correct code for the keypad.
        public string CorrectCode
        {
            get => _correctCode;
            private set => _correctCode = value;
        }

        private void Start()
        {
            digitInput = GetComponentInChildren<TMP_InputField>();
            audioSource = GetComponentInChildren<AudioSource>();
            placeholder = digitInput.placeholder.GetComponent<TMP_Text>();
            IsActive = gameObject.activeSelf;

            // Input Validation
            if (_correctCode.Length != desiredCodeLength)
            {
                Debug.LogError($"Initial correct code length ({_correctCode.Length}) doesn't match the desired code length ({desiredCodeLength}).");
            }
        }

        // Adds a number to the entered code.
        public void AddNumber(string number)
        {
            if (codeIndex < desiredCodeLength)
            {
                codeIndex++;
                enteredCode += number; // Variable Consistency
                digitInput.text += number;
                audioSource.PlayOneShot(digitSound, digitSoundVolume);
            }
            else if (debugMode)
            {
                Debug.Log("Max code length reached.");
            }
        }

        // Resets the entered numbers.
        public void ResetNumber()
        {
            if (!string.IsNullOrEmpty(enteredCode))
            {
                codeIndex = 0;
                enteredCode = "";
                digitInput.text = "";
                audioSource.PlayOneShot(digitSound, digitSoundVolume);
            }
            IsChangingCode = false;
            placeholder.SetText("Enter Code");
        }

        // Plays a button click sound effect.
        public void PlayBtnClickSound()
        {
            audioSource.PlayOneShot(clickSound, clickSoundVolume);
        }


        // Initiates the code change sequence if the correct code is entered.
        public void ChangeCode()
        {
            if (digitInput.text == CorrectCode)
            {
                if (codeIndex != 0)
                {
                    ResetNumber();
                }
                IsChangingCode = true;
                placeholder.SetText("Change Code");
            }
            else
            {
                audioSource.PlayOneShot(errorSound, errorSoundVolume);
                StartCoroutine(Shake(gameObject.transform, 0.5f, 5f));
            }
        }


        // Validates the entered code against the correct code.
        public void Enter()
        {
            if (!string.IsNullOrEmpty(enteredCode) && codeIndex >= desiredCodeLength)
            {
                if ((digitInput.text == CorrectCode)&&(!IsChangingCode))
                {
                    OnValidCode();
                }
                else
                {
                    if (IsChangingCode)
                    {
                        OnChangingCode();
                    }
                    else
                    {
                        audioSource.PlayOneShot(errorSound, errorSoundVolume);
                        if (debugMode) Debug.Log($"Invalid code: {digitInput.text}");
                        StartCoroutine(Shake(gameObject.transform, 0.5f, 5f));
                    }
                    ResetNumber();
                }
            }
            else
            {
                if (debugMode) Debug.Log($"Incomplete code: {digitInput.text}");
                audioSource.PlayOneShot(errorSound, errorSoundVolume);
                StartCoroutine(Shake(gameObject.transform, 0.5f, 5f));
            }
        }

        private void OnValidCode()
        {
            audioSource.PlayOneShot(unlockedSound, unlockedSoundVolume);
            if (debugMode) Debug.Log($"Correct code: {digitInput.text}");
            OnValidCodeEntered.Invoke();
            StartCoroutine(DelayedToggleKeyPad(1f));
            
            
        }

        private void OnChangingCode()
        {
            CorrectCode = digitInput.text;
            if (debugMode) Debug.Log($"Code has changed to : {CorrectCode}");
            OnChangingCodeEntered.Invoke();
        }

        // Toggles the visibility and functionality of the keypad.
        public void ToggleKeyPad()
        {
            ResetNumber();
            gameObject.SetActive(!IsActive);
            IsActive = gameObject.activeSelf;
        }

        private IEnumerator DelayedToggleKeyPad(float timeInSeconds)
        {
            yield return new WaitForSeconds(timeInSeconds);
            ToggleKeyPad();
        }


        private IEnumerator Shake(Transform target, float duration, float magnitude)
        {
            Vector3 originalPosition = target.localPosition;
            float elapsed = 0.0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;

                target.localPosition = new Vector3(x, y, originalPosition.z);

                elapsed += Time.deltaTime;

                yield return null;
            }

            target.localPosition = originalPosition;
        }

    }
}
