# UnityARSnippet
AR-Tech#2のサンプルコード

その他のDelegateはこんなかんじ。

public void Start()
{
   UnityARSessionNativeInterface.ARAnchorAddedEvent += AddAnchor;
   UnityARSessionNativeInterface.ARAnchorUpdatedEvent += UpdateAnchor;
   UnityARSessionNativeInterface.ARAnchorRemovedEvent += RemoveAnchor;
}

public void AddAnchor(ARPlaneAnchor arPlaneAnchor)
{
}

public void RemoveAnchor(ARPlaneAnchor arPlaneAnchor)
{
}

public void UpdateAnchor(ARPlaneAnchor arPlaneAnchor)
{
}

