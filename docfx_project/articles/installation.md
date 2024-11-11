# Installation Instructions for Unity Game Project

This project is a Unity game using Unity 2021.3.45f1 LTS and supports both Android and Windows builds. Follow the steps below to set up the project:

## Prerequisites

1. **Unity Hub**: Download and install Unity Hub from the [Unity website](https://unity3d.com/get-unity/download).
2. **Unity Editor**: Install Unity 2021.3.45f1 LTS through Unity Hub.
3. **Android Build Support**: Ensure you have Android Build Support installed in Unity Hub for Unity 2021.3.45f1 LTS.
4. **JDK, SDK, and NDK**: Install the required JDK, SDK, and NDK for Android development through Unity Hub.

## Project Setup

1. **Clone the Repository**: Clone the project repository to your local machine.
   ```sh
   git clone <repository-url>
   cd <repository-directory>
   ```

2. **Open the Project in Unity**:
    - Open Unity Hub.
    - Click on the `Add` button and navigate to the cloned project directory.
    - Select the project and click `Open`.

3. **Configure Build Settings**:
    - Open the project in Unity Editor.
    - Go to `File` > `Build Settings`.
    - Select `Android` or `Windows` as the target platform.
    - Click on `Switch Platform`.

4. **Android Build Configuration**:
    - Go to `Edit` > `Project Settings` > `Player`.
    - Under the `Android` tab, configure the following:
        - **Company Name**: Set your company name.
        - **Product Name**: Set your product name.
        - **Package Name**: Set a unique package name (e.g., `com.yourcompany.yourgame`).
        - **Minimum API Level**: Set the minimum API level required (e.g., `API Level 21`).
    - Under `Other Settings`, ensure the `Scripting Backend` is set to `IL2CPP` and `Target Architectures` include `ARMv7` and `ARM64`.

5. **Windows Build Configuration**:
    - Go to `Edit` > `Project Settings` > `Player`.
    - Under the `Windows` tab, configure the following:
        - **Company Name**: Set your company name.
        - **Product Name**: Set your product name.
    - Under `Other Settings`, ensure the `Scripting Backend` is set to `IL2CPP`.

6. **Build the Project**:
    - Go to `File` > `Build Settings`.
    - Select the scenes you want to include in the build.
    - Click on `Build` and choose the output directory.

7. **Run the Project**:
    - For Android: Transfer the APK to your Android device and install it.
    - For Windows: Run the executable file generated in the output directory.

## Additional Notes

- Ensure all dependencies and plugins required by the project are correctly installed.
- Follow any additional setup instructions provided in the project documentation.

By following these steps, you should be able to set up and build the Unity game project for both Android and Windows platforms.