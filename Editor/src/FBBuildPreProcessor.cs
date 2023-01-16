using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class FBBuildPreProcessor : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        Debug.Log("Linking FB plugins");

        const string packagesFolderPath = "Packages/com.facebook.unity";
        const string assetsFolderPath = "Assets/FacebookSDK";
        const string linkFile = "link.xml";

        if (AssetDatabase.IsValidFolder(assetsFolderPath) &&
            File.Exists(Path.Combine(assetsFolderPath, linkFile)))
        {
            Debug.Log("FB link file exists in Assets folder.");
            return;
        }

        if (!AssetDatabase.IsValidFolder(packagesFolderPath))
        {
            StopBuildWithMessage("FB packages folder not found.");
            return;
        }
        
        AssetDatabase.CopyAsset(Path.Combine(packagesFolderPath, linkFile),
            Path.Combine(assetsFolderPath, linkFile));
            
        Debug.Log("Copied FB link file from Packages to Assets folder.");
    }
    
    private void StopBuildWithMessage(string message)
    {
        var prefix = "[FB] ";
        throw new BuildPlayerWindow.BuildMethodException(prefix + message);
    }
}
