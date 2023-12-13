using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System;
using System.IO;
using BeliefEngine.HealthKit;
using UnityEditor.iOS.Xcode;


namespace BeliefEngine.HealthKit
{

/*! @brief 		Build processor script.
	@details	This build processor updates the Xcode project in order to build automatically. It adds the HealthKit capability and frameworks, and creates an
				entitlements file.
				It also scans through the scenes in the Unity project looking for a HealthKitDataTypes object, and extracts the usage / update strings from it,
				which are used to present an alert to the user when requesting permission.
 */
public class HealthKitBuildProcessor : IProcessSceneWithReport
{
	private static string shareString = null;
	private static string updateString = null;
	private static string clinicalString = null;
	
	/*! @brief required by the IProcessScene interface. Set high to let other postprocess scripts run first. */
	public int callbackOrder {
		get { return 100; }
	}

	/*! @brief         Searches for HealthKitDataTypes objects & reads the usage strings for the OnPostprocessBuild phase. 
		@param scene   the scene being processed.
		@param report  a report containing information about the current build
	 */
	public void OnProcessScene(Scene scene, BuildReport report) {
		GameObject[] rootObjects = scene.GetRootGameObjects();
		foreach (GameObject obj in rootObjects) {
			HealthKitDataTypes types = obj.GetComponentInChildren<HealthKitDataTypes>();
			if (types != null) {
				if (types.AskForSharePermission()) {
					HealthKitBuildProcessor.shareString = types.healthShareUsageDescription;
				}

				if (types.AskForUpdatePermission()) {
					HealthKitBuildProcessor.updateString = types.healthUpdateUsageDescription;
				}

				if (types.AskForClinicalPermission()) {
					HealthKitBuildProcessor.clinicalString = types.clinicalUsageDescription;
				}
			}
		}
	}

	/*! @brief              Updates the Xcode project. 
		@param buildTarget  the target build platform
		@param path         the path of the target build
	 */
	[PostProcessBuildAttribute(10)]
	public static void OnPostprocessBuild(BuildTarget buildTarget, string path) {
		Debug.Log("--- BEHEALTHKIT POST-PROCESS BUILD ---");
		if (buildTarget == BuildTarget.iOS) {
			string projPath = path + "/Unity-iPhone.xcodeproj/project.pbxproj";

			PBXProject proj = new PBXProject();
			proj.ReadFromFile(projPath);

#if UNITY_2019_3_OR_NEWER
			string mainTarget = proj.GetUnityMainTargetGuid();
			string frameworkTarget = proj.GetUnityFrameworkTargetGuid();

			Debug.LogFormat("main target: {0}", mainTarget);
			Debug.LogFormat("framework target: {0}", frameworkTarget);
#else
			string targetName = PBXProject.GetUnityTargetName();
			string mainTarget = proj.TargetGuidByName(targetName);
#endif
			bool addHealthRecordsCapability = (clinicalString != null);

			// Info.plist
			//-----------
			var info = ProcessInfoPList(path, addHealthRecordsCapability);


			// Entitlements
			//--------------
			string entitlementsRelative = ProcessEntitlements(path, proj, mainTarget, info, addHealthRecordsCapability);

#if UNITY_2019_3_OR_NEWER
			// add HealthKit capability
			ProjectCapabilityManager capabilities = new ProjectCapabilityManager(projPath, entitlementsRelative, null, frameworkTarget);
			capabilities.AddHealthKit();

			// add HealthKit Framework
			proj.AddFrameworkToProject(frameworkTarget, "HealthKit.framework", true);

			// Set a custom link flag
			proj.AddBuildProperty(frameworkTarget, "OTHER_LDFLAGS", "-ObjC");
#else
			// add HealthKit capability
			ProjectCapabilityManager capabilities = new ProjectCapabilityManager(projPath, entitlementsRelative, targetName);
			capabilities.AddHealthKit();

			// add HealthKit Framework
			proj.AddFrameworkToProject(mainTarget, "HealthKit.framework", true);

			// Set a custom link flag
			proj.AddBuildProperty(mainTarget, "OTHER_LDFLAGS", "-ObjC");
#endif
			proj.WriteToFile(projPath);
		}
	}

	// -------------------------------

	internal static PlistDocument ProcessInfoPList(string path, bool addHealthRecordsCapability) {
		string plistPath = Path.Combine(path, "Info.plist");
		PlistDocument info = GetInfoPlist(plistPath);
		PlistElementDict rootDict = info.root;
		// // Add the keys
		if (HealthKitBuildProcessor.shareString != null) {
			rootDict.SetString("NSHealthShareUsageDescription", HealthKitBuildProcessor.shareString);
		}
		else {
			Debug.LogError("unable to read NSHealthShareUsageDescription");
		}
		if (HealthKitBuildProcessor.updateString != null) {
			rootDict.SetString("NSHealthUpdateUsageDescription", HealthKitBuildProcessor.updateString);
		}
		if (addHealthRecordsCapability) {
			rootDict.SetString("NSHealthClinicalHealthRecordsShareUsageDescription", HealthKitBuildProcessor.clinicalString);
		}

		// Write the file
		info.WriteToFile(plistPath);

		return info;
	}

	internal static string ProcessEntitlements(string path, PBXProject proj, string target, PlistDocument info, bool addHealthRecordsCapability) {
		string entitlementsFile;
		string entitlementsRelative;
		string entitlementsPath;

		entitlementsRelative = proj.GetBuildPropertyForConfig(target, "CODE_SIGN_ENTITLEMENTS");
		Debug.LogFormat("get build property [{0}, {1} = {2}]", target, "CODE_SIGN_ENTITLEMENTS", entitlementsRelative);
		PlistDocument entitlements = new PlistDocument();

		if (entitlementsRelative != null) {
			entitlementsPath = Path.Combine(path, entitlementsRelative);
		} else {
			string projectname = GetProjectName(info);
			entitlementsFile = Path.ChangeExtension(projectname, "entitlements");
			entitlementsRelative = entitlementsFile;

			entitlementsPath = Path.Combine(path, entitlementsFile);

			proj.AddFileToBuild(target, proj.AddFile(entitlementsFile, entitlementsFile, PBXSourceTree.Source));

			Debug.LogFormat("add build property [{0}, {1}] => {2}", target, "CODE_SIGN_ENTITLEMENTS", entitlementsFile);
			proj.AddBuildProperty(target, "CODE_SIGN_ENTITLEMENTS", entitlementsPath);
			string newEntitlements = proj.GetBuildPropertyForConfig(target, "CODE_SIGN_ENTITLEMENTS");
			Debug.LogFormat("=> {0}", newEntitlements);
		}

		ReadEntitlements(entitlements, entitlementsPath);
		entitlements.root.SetBoolean("com.apple.developer.healthkit", true);
		var healthkitAccess = entitlements.root.CreateArray("com.apple.developer.healthkit.access");
		if (addHealthRecordsCapability) {
			healthkitAccess.AddString("health-records");
		}
		SaveEntitlements(entitlements, entitlementsPath);

		return entitlementsRelative;
	}

	// -------------------------------

	internal static void ReadEntitlements(PlistDocument entitlements, string destinationPath) {
		
		if (System.IO.File.Exists(destinationPath)) {
			try {
				Debug.LogFormat("reading existing entitlements: '{0}'.", destinationPath);
				entitlements.ReadFromFile(destinationPath);
			}
			catch (Exception e) {
				Debug.LogErrorFormat("error reading from file: {0}", e);
			}
		}
	}
	
	internal static void SaveEntitlements(PlistDocument entitlements, string destinationPath) {
		try {
			entitlements.WriteToFile(destinationPath);
		}
		catch (Exception e) {
			Debug.LogErrorFormat("error writing to file: {0}", e);
		}
	}

	internal static PlistDocument GetInfoPlist(string plistPath) {
		// Get the plist file
		PlistDocument plist = new PlistDocument();
		plist.ReadFromFile(plistPath);
		return plist;
	}
	
	internal static string GetProjectName(PlistDocument plist) {
		string projectname = plist.root["CFBundleDisplayName"].AsString();
		return projectname;
	}
}

}