# DecoAR

DecoAR is an App for exdended the decoration at home, office or any place. It is aimed to track different images from home and provide overlayed content on top of that, when we see them thru the smart glasses.  

Aim of this project is to easily configure the images from the environemnt you want to extend, and see in the smart glasses like ThinkReality A3 or similar. It usage Qualcomm Snapdragon SDK's image tracking feature.   

Here are steps user need to do. 

- Import this project in Unity and import Spaces SDK  
- Take some pictures from the environent your want to extend. 
- Define actions for those pictures, following actions are available
  -  3D model - a 3D model will pop out of the image. 
  -  Video - a video will play on top of the tracked image
  -  Animation - a gif animation will play on top of the tracked image
  -  Audio - an audio will play on top of the tracked image
  -  Text - a formatted text will be shown on the tracked image.
- Once data is setup, build the project and run in the smart glass. 



## Project Setup

#### Download the Snapdragon Spaces SDK
- Open https://spaces.qualcomm.com/download-sdk/
- Login to your account or create a account if you don't have.
- Click on "Download SDK" button for unity
- Unzip the Downloaded file

#### Setting up the project
- Clone the repo
- Open it with Unity editor
- Continue if there is any error like `Package Manager Error` while opening the project for first time
- Open Package Manager from `Window > Package Manager`.
- Click on the plus icon and choose "Add Package from tarball" option.
- Navigate the file that we unzipped and choose the `SnapdragonSpaces_Package_0_9_0.tgz` from the folder to import it.
- Wait until import and compilation are done.
- After importing click on the Snapdragon Spaces in Package Manager.
- There you will find samples. Click on Samples and import them. It will be added to the Assets folder.



## Setup Data for tracking

#### Collect Images
- Get the images(JPG/JPEG/PNG) that you want to track.
- Take printout and stick it to the wall.
- Drag and drop the image files in the Unity project as a new folder under `Assets`.

#### Updating image settings
- Click on the Image to view it on Inspect window
- Check the checkbox for `Alpha is transparency` and `Read/Write` both
- Choose the `Wrap mode` as `Clamp`
- Click on Apply to apply all the changes we made.
- Apply same settings for all the images you want to track.

#### Add image to the Image Reference Library
- Check `Image Libraries` Folder. There you will find `ReferenceImageLibrary`.
- Click on it to view it on Inspector window.
- To add a Image, click on `Add Image` button on the bottom. It will add a new row to add image.
- Drag and drop the image from Texture folder into the newly added row. `Note that the image texture type should be Sprite.`
- Check the box `Specify Size` option and add the height width of the physical image. Note that the values should be in `Meters`.
- Then check the option for `Keep texture at Runtime`.
- Update the total count of images in Image Reference Library in `AR Session Origin` game object `Max Number of moving images` field under `AR Tracked Image Manager` Script.
- Repeat the same if you want to add multiple images.

#### Add the Action for a specific image
- Locate the Prefab called `PrefabInstatiater` Under `Assets/Prefabs` folder.
- Open it in Inspector window. It should have `Tracked Image Action Manager` script attached to it. Expand the `Images With Action` field.
- To an action for the image, click on the `Plus` icon, then give the image name(same image name as in Texture) and the new [prefab](#creating-an-action-prefab) as an action prefab.
- Thats's all.

#### Creating an action prefab
For all Types of Action we have already create a demo prefab.

If you want a new prefab, then just Copy the demo prefab and then change the image/video/audio inside the copied prefab.

For 3D model, you don't need to create any extra prefab. Provide the 3D model directly.

## Build and Run
- Open the `Build settings` in unity.
- Change the build target to android.
- Do a clean build and install in the phone to run it.

## Demo 
Check the demo video of image tracking [here](./References/Demo.mp4)
