using System;
using System.Collections;
using System.Collections.Generic;
using BeliefEngine.HealthKit;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    [SerializeField]
    ParticleSystem TeleportAuraParticleSystem;

    [SerializeField]
    SkinnedMeshRenderer skinnedMeshRenderer;

    [SerializeField]
    Material[] maskMaterials;
    [SerializeField]
    Material[] headAndBodyMaterials;
    [SerializeField]
    Material[] handsAndFeetMaterials;

    public void Teleport(Island island)
    {
        transform.position = island.GetAvatarTarget_T().position;
        transform.rotation = island.GetAvatarTarget_T().transform.rotation;
        TeleportAuraParticleSystem.Play();
        ChangeAnimationState(island);
    }

    /*! @enum BiologicalSex
		@ingroup Enumerations
		@brief possible values for Biological Sex
	 */
    public enum BiologicalSex
    {
        NotSet = 0,
        Female,
        Male,
        Other
    }

    /*! @enum BloodType
		@ingroup Enumerations
		@brief possible values for BloodType
	 */
    public enum BloodType
    {
        NotSet = 0,
        APositive,
        ANegative,
        BPositive,
        BNegative,
        ABPositive,
        ABNegative,
        OPositive,
        ONegative
    }

    /*! @enum FitzpatrickSkinType
		@ingroup Enumerations
		@brief possible values for Fitzpatrick Skin Type
	 */
    public enum FitzpatrickSkinType
    {
        FitzpatrickSkinTypeNotSet = 0,
        FitzpatrickSkinTypeI,
        FitzpatrickSkinTypeII,
        FitzpatrickSkinTypeIII,
        FitzpatrickSkinTypeIV,
        FitzpatrickSkinTypeV,
        FitzpatrickSkinTypeVI
    }

    public void SetColors()
    {
        string sexString = UserData.Instance.CharacteristicTypeValues[HealthKitDataTypes.GetIdentifier(HKDataType.HKCharacteristicTypeIdentifierBiologicalSex)].ToString();
        string bloodTypeString = UserData.Instance.CharacteristicTypeValues[HealthKitDataTypes.GetIdentifier(HKDataType.HKCharacteristicTypeIdentifierBloodType)].ToString();
        string skinTypeString = UserData.Instance.CharacteristicTypeValues[HealthKitDataTypes.GetIdentifier(HKDataType.HKCharacteristicTypeIdentifierFitzpatrickSkinType)].ToString();

        Material[] temp = new Material[3];
        // Set color for Hands and Feet
        temp[0] = handsAndFeetMaterials[(int)Enum.Parse(typeof(BiologicalSex), sexString)];

        // Set color for Body and Head
        temp[1] = headAndBodyMaterials[(int)Enum.Parse(typeof(FitzpatrickSkinType), skinTypeString)];

        // Set color for Glass on the Mask
        temp[2] = maskMaterials[(int)Enum.Parse(typeof(BloodType), bloodTypeString)];

        skinnedMeshRenderer.materials = temp;
    }

    public void ChangeAnimationState(Island island)
    {
        AnimationClip clip = island.GetClip();
        animator.Play(clip.name, 0);
    }
}
