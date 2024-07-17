
# Unity3dKeyPad

This repository contains a digital keypad UI component for Unity 3D. The keypad allows users to enter, validate, and change access codes, providing both visual and audio feedback for user interactions. 

## Features
- **Code Entry**: Users can enter a code using a series of buttons.
- **Code Validation**: Validates the entered code and provides feedback on success or failure.
- **Change Code**: Allows users to change the existing code after entering the correct current code.
- **Reset Functionality**: Resets the entered code.
- **Toggle Visibility**: Toggle the visibility of the keypad.
- **Audio Feedback**: Plays sounds for button clicks, correct entries, and errors.
- **Visual Feedback**: Includes a shaking effect for incorrect code entries.

## Getting Started

1. **Clone the Repository**:
   ```sh
   git clone https://github.com/GlaDioTGTV/Unity3dKeyPad.git
   ```
2. **Open the Project**:
   - Open the cloned repository in Unity.
   - Ensure all dependencies (e.g., TextMeshPro) are installed.

3. **Add the Keypad to Your Scene**:
   - Drag the keypad prefab into your scene.
   - Attach the `KeyPadUI` script to a GameObject if it's not already attached.

4. **Customize the Keypad**:
   - Set the desired code length and correct code via the inspector.
   - Adjust audio clips and volumes as needed.

## Usage
- **Interacting with the Keypad**: Use the UI buttons to enter and validate codes.
- **Changing the Code**: Enter the current code, press "Change Code", then enter the new code and press "Enter".
- **Resetting the Code Entry**: Press the "Reset" button to clear the current entry.
- **Toggling Visibility**: Use the "Toggle" button to show or hide the keypad.

## Contributing
Feel free to submit issues or pull requests for any improvements or bug fixes. Contributions are welcome!

## Contact
For questions or support, please contact me at [med.maddouri@gmail.com](mailto:med.maddouri@gmail.com).

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
