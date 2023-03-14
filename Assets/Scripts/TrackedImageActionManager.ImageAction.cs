using System;

public partial class TrackedImageActionManager
{
    [Serializable]
    private enum Action
    {
        Audio, Video, Image, Model
    }
    
    [Serializable]
    private sealed class ImageAction
    {
        public string imageName = null;
        public Action action; 
        public string sourcePath = null;
    }
}