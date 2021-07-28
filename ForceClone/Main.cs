using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MelonLoader;
using UnityEngine;
using UnityEngine.UI;

[assembly: MelonLoader.MelonGame("VRChat", "VRChat")]
[assembly: MelonLoader.MelonInfo(typeof(ForceClone.Main), "ForceClone", "1.0", "Four_DJ")]

namespace ForceClone
{
    public class Main : MelonMod
    {
        private static Button forceCloneButton;
        private static Text forceCloneText;

        public override void OnApplicationStart()
        {
            this.HarmonyInstance.Patch(typeof(UserInteractMenu).GetMethod("Update"), new HarmonyMethod(AccessTools.Method(typeof(Main), nameof(UserInteractUpdate))));
        }

        private static bool UserInteractUpdate(UserInteractMenu __instance)
        {
            if (forceCloneButton != null)
            {
                if (QuickMenu.prop_QuickMenu_0.prop_APIUser_0 != null)
                {
                    if (QuickMenu.prop_QuickMenu_0.field_Public_MenuController_0.activeAvatar.releaseStatus == "private")
                    {
                        forceCloneText.text = "Private";
                        forceCloneText.color = Color.gray;
                        forceCloneButton.interactable = false;
                    }
                    else if (!QuickMenu.prop_QuickMenu_0.prop_APIUser_0.allowAvatarCopying)
                    {
                        forceCloneText.text = "Force Clone";
                        forceCloneText.color = Color.red;
                        forceCloneButton.interactable = true;
                    }
                    else
                    {
                        forceCloneText.text = "Clone";
                        forceCloneText.color = Color.green;
                        forceCloneButton.interactable = true;
                    }
                }
            }
            else
            {
                GameObject forceCloneObject = UnityEngine.Object.Instantiate(__instance.field_Public_Button_1.gameObject, __instance.transform, true);
                forceCloneButton = forceCloneObject.GetComponentInChildren<Button>();
                forceCloneText = forceCloneObject.GetComponentInChildren<Text>();
                forceCloneButton.onClick = __instance.field_Public_Button_1.onClick;
                __instance.field_Public_Button_1.gameObject.transform.localScale = new Vector3(0, 0, 0);

            }
            return false;
        }
    }
}
