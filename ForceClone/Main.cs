using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core;
using VRC.UI.Core.Styles;

[assembly: MelonLoader.MelonGame("VRChat", "VRChat")]
[assembly: MelonLoader.MelonInfo(typeof(ForceClone.Main), "ForceClone", "1.1", "Four_DJ")]

namespace ForceClone
{
    public class Main : MelonMod
    {
        public override void OnApplicationStart()
        {
            ClassInjector.RegisterTypeInIl2Cpp<SelectUserMenu>();
            MelonCoroutines.Start(OnUiManagerInitCoroutine());
        }

        //https://github.com/RinLovesYou/QuickMenuLib/blob/master/QuickMenuLib/QuickMenuLibMod.cs
        private IEnumerator OnUiManagerInitCoroutine()
        {
            while (VRCUiManager.prop_VRCUiManager_0 == null) yield return null;

            while (UIManager.field_Private_Static_UIManager_0 == null)
                yield return null;
            while (GameObject.Find("UserInterface").GetComponentInChildren<VRC.UI.Elements.QuickMenu>(true) == null)
                yield return null;
            SelectUserMenu SelectUserMenu = FindInactive("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_SelectedUser_Local").AddComponent<SelectUserMenu>();
            while (SelectUserMenu.StyleEngine == null)
                SelectUserMenu.StyleEngine = FindInactive("UserInterface/Canvas_QuickMenu(Clone)").GetComponent<StyleEngine>();
        }

        //https://github.com/knah/VRCMods/blob/master/UIExpansionKit/UnityUtils.cs
        public static GameObject FindInactive(string path)
        {
            var split = path.Split(new[] { '/' }, 2);
            var rootObject = GameObject.Find($"/{split[0]}")?.transform;
            if (rootObject == null) return null;
            return Transform.FindRelativeTransformWithPath(rootObject, split[1], false)?.gameObject;
        }
    }
}
