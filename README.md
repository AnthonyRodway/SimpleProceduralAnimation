# SimpleProceduralAnimation
This was a small procedural animation project that I did in unity that explores some ways to procedurally animate a bipedal character with inverse kinematics and a wave function

# Procedural Animation with Inverse Kinematics

SimpleProceduralAnimation

## Table of Contents

- [Project Description](#project-description)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Project Description

This was a smaller animation project that I accomplished in the animation class CSC 473 at the University of Victoria. I was tasked with creating a project that would try to solve a mathematical problem related to the field of computer animation. Thats when I thought that traditioanl animation project tend to take enormous amounts of time and effort for the animator. So I thought that learning a bit about procedural animation would be a fun project and tried Inverse Kinematics (IK) with it.  It's main purpose it to make a bipedal character look like they are walking with some degree of naturalness. It was created in Unity, so all scripts are written in C#. 

There are two different IK algorithms I implemented, Forward And Backward Reaching Inverse Kinematics which you can read a short explanation about [here](http://www.andreasaristidou.com/FABRIK.html), and Cyclic Coordinate Descent Inverse Kinematics which more can be read about it [here](http://www.root-motion.com/finalikdox/html/page5.html).

This project can also be interpreted as not fully complete. There are some bugs in the system that I will come back to in the future, but for the sake of the project about learning, I was happy with my progress.

https://github.com/AnthonyRodway/SimpleProceduralAnimation/assets/69998437/d1b037cb-6255-4900-9d62-485cf8a26fc3

## Installation

Before we start, you will need to install a few things if you do not already have them on your local machine, this project was developed on Editor version 2021.3.16f1, so follow the steps below if you need help:

### 1. Unity Hub

Unity Hub is a tool that helps you manage your Unity installations and projects. It provides an interface to download and install different versions of the Unity Editor. Here's how to install Unity Hub:

1. Visit the official Unity website: [Unity Hub Download](https://unity3d.com/get-unity/download).
2. Choose the appropriate operating system for your machine (Windows or macOS).
3. Click the download link to start downloading Unity Hub.
4. Once the download is complete, run the installer and follow the on-screen instructions to install Unity Hub on your machine.

### 2. Unity Editor

Unity Editor is the main development environment for Unity projects. You can install a specific version (2021.3.16f1) to ensure the best compatibility with this project. Follow these steps to install Unity Editor:

1. Navigate to the following link: [Editor Downlaod](https://unity.com/releases/editor/whats-new/2021.2.16).
2. Choose the appropriate operating system for your machine (Windows, Linux, or macOS).
3. Click the download link to start downloading Unity Hub.
4. Once the download is complete, run the installer and follow the on-screen instructions to install Unity Hub on your machine.

However, since Unity follows backwards compatibility, it is generally fine to run a project with an editor version within the same major version. So you can follow the steps below for a quicker solution:

1. Open Unity Hub, which you installed in the previous step.
2. Click on the "Installs" tab at the side of the Unity Hub window.
3. Click the "Install Editor" button near the top to open the Unity version installation menu.
4. Find "Unity 2021.3.*" in the list and click the "Install" button for it.
5. On the next page, you can choose additional components to install with Unity Editor. Make sure to select the components you need or leave the default selections.
6. Click "Continue", then accept the terms and conditions and the "Install" button will start the process.
7. Unity Hub will now download and install the Unity Editor version 2021.3.* on your machine. This may take some time depending on your internet connection speed.

By following these steps, you will successfully install Unity on your machine.

### 3. Clone the Project

Now that you have the prerequisites installed, you can clone the Unity project repository from your GitHub account. Follow these steps:

1. Visit this GitHub repository in a web browser.
2. Click on the blue 'Code' button near the top and copy the HTTPS link.
3. Open your chosen command line interface, navigate to where you want the project folder, and type the command

   `git clone https://github.com/AnthonyRodway/SimpleProceduralAnimation.git`

### 4. Download the Repository

If you prefer not to use Git or don't have it installed, you can download the Unity project repository directly from GitHub. Here's how:

1. Visit this GitHub repository in a web browser.
2. Click on the blue "Code" button located near the top right of the repository.
3. Select "Download ZIP" from the dropdown menu.
4. Save the ZIP file to a location on your machine where you want to store the project.
5. Extract the contents of the ZIP file to a folder of your choice.

### 5. Open the Project in Unity Editor

With the repository cloned or downloaded, you can now open the Unity project in Unity Editor:

1. Open Unity Hub on your machine.
2. Click on the "Projects" tab.
3. Click "Open" and navigate to the location where you put the project folder.
4. Select the project folder and click "Open".
5. Unity Hub will now load the project and show it in the list of available projects.
6. If you have more than one editor version installed, you may need to change it to the one you installed above with the dropdown selection menu.
7. Click on the project name to open it in Unity Editor.

### 6. Resolve Dependencies and Build

Once the project is open in Unity Editor, it might have some additional dependencies or packages that need to be resolved.

- Unity Editor will automatically detect missing packages or dependencies and prompt you to install them. Follow the on-screen instructions to install any required packages.
- If there are any build errors, Unity Editor will display them in the console window. Resolve any build errors by checking the error messages and making the necessary changes.
- Once all dependencies are resolved and there are no build errors, you can build and run the project using the Unity Editor's built-in tools.

That's it! You should now have all the components installed and the Unity project running on Editor version 2021.3.16f1 or a similar major version. If you encounter any issues or have questions, please refer to the project's documentation or seek help from me through email.

## Usage

After opening the project, you will probably see a completely blank Scene. Go to the bottom of the window in the projects panel, double-click the Scenes folder and drag the only available Scene into the heirarchy panel on the side. This should load everything up so that you see something like the below. Hopefully everything imported correctly, if it did you should see a scene with a bipedal character standing in what looks like a room. 
![image](https://github.com/AnthonyRodway/SimpleProceduralAnimation/assets/69998437/d75bd5b8-4693-4f98-b46e-8c9340b5835a)

The only things you should be changing are inside of the SKELETON object in the Hierarchy panel. Click on the object and you will see the inspector panel change to give you information about the object. From here you may:
- Select whichever IK script you want the model to use during the procedural animation. Only one can be selected during runtime or it will break so check only one of "CCDIK (Script)" or "FABRIK (Script)"
  
  ![image](https://github.com/AnthonyRodway/SimpleProceduralAnimation/assets/69998437/ec36702d-e0ed-4bb0-8e04-7715a7ff2208)
  
- After selecting which IK algorithm you want the animation to use, you can change the Max Iterations and the Distance Threshold in the UI under the scripts to make it more or less precise.

  ![image](https://github.com/AnthonyRodway/SimpleProceduralAnimation/assets/69998437/e58a93d3-4cdd-47e6-869b-251d5738da95) ![image](https://github.com/AnthonyRodway/SimpleProceduralAnimation/assets/69998437/f9b5f5e6-1f53-44b4-982c-703d31a3892d)

- You can also set the parameters (Step Deight, Step Distance, Step Durration) of the footstep motion script that will change the default values that the animation uses when it starts up.

  ![image](https://github.com/AnthonyRodway/SimpleProceduralAnimation/assets/69998437/189fe298-a8f2-48b6-8ccf-78cdc637a1f7)

This project can be run like any other unity project, if you are unfarmiliar with unity, there is some great documentation [here](https://docs.unity3d.com/560/Documentation/Manual/UnityBasics.html) about basics. But to start the procedural animation, click the play button at the top center of the window.

You will notice the window changes to play mode and the characater is now moving according to the parameters you set and the IK algorithm that was chosen. Since this is a procedural animation, it means that is it generated as it goes, so during runtime you can change the parameters that you set above in the footstep motion and it will change the animation in real time.

That is all there is to it! have fun playing with it if you want.

## Contributing

At this current point, this project is not open for contributing.

## License

This project is licensed under the [MIT License](LICENSE).

[Click here to view the license file](LICENSE) and review the terms and conditions of the MIT License.

## Contact

You can reach me at my email on my profile if you have any questions or feedback :)

**README.md last Updated 2023-06-17**
