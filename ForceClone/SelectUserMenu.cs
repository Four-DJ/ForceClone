using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC;
using VRC.Core;
using VRC.UI;
using VRC.UI.Core.Styles;
using VRC.UI.Elements.Menus;

namespace ForceClone
{
    internal class SelectUserMenu : MonoBehaviour
    {
        public SelectUserMenu(IntPtr obj) : base(obj) { }

        GameObject CloneButton;
        TextMeshProUGUI ForceCloneText;
        Button ForceCloneButton;
        Player SelectedUser;
        SelectedUserMenuQM SelectedUserMenuQM;
        public StyleEngine StyleEngine;

        void Start()
        {
            CloneButton = this.transform.Find("ScrollRect/Viewport/VerticalLayoutGroup/Buttons_AvatarActions/Button_CloneAvatar").gameObject;

            GameObject forceClone = UnityEngine.Object.Instantiate<GameObject>(Main.FindInactive("UserInterface/Canvas_QuickMenu(Clone)/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_Respawn"), CloneButton.transform.parent);
            forceClone.transform.parent = CloneButton.transform.parent;
            forceClone.name = "ForceCloneButton";

            ForceCloneText = forceClone.GetComponentInChildren<TextMeshProUGUI>();
            ForceCloneText.text = "Clone Avatar";

            Image forceCloneIcon = forceClone.transform.Find("Icon").GetComponent<Image>();
            forceCloneIcon.sprite = StyleEngine.field_Private_Dictionary_2_String_Sprite_0["CloneAvatar"];
            forceCloneIcon.overrideSprite = StyleEngine.field_Private_Dictionary_2_String_Sprite_0["CloneAvatar"];

            ForceCloneButton = forceClone.GetComponent<Button>();
            ForceCloneButton.onClick = new Button.ButtonClickedEvent();
            ForceCloneButton.onClick.AddListener(new Action(delegate
            {
                PageAvatar component = GameObject.Find("Screens").transform.Find("Avatar").GetComponent<PageAvatar>();
                component.field_Public_SimpleAvatarPedestal_0.field_Internal_ApiAvatar_0 = new ApiAvatar
                {
                    id = SelectedUser._vrcplayer.field_Private_ApiAvatar_0.id
                };
                component.ChangeToSelectedAvatar();
            }));

            SelectedUserMenuQM = this.GetComponent<VRC.UI.Elements.Menus.SelectedUserMenuQM>();
            CloneButton.SetActive(false);
            SelectedUser = PlayerManager.prop_PlayerManager_0.prop_ArrayOf_Player_0.First(x => x.prop_APIUser_0.id == SelectedUserMenuQM.field_Private_IUser_0.prop_String_0);
        }

        void LateUpdate()
        {
            if (SelectedUser.prop_APIUser_0.id == SelectedUserMenuQM.field_Private_IUser_0.prop_String_0)
                return;

            SelectedUser = PlayerManager.prop_PlayerManager_0.prop_ArrayOf_Player_0.First(x => x.prop_APIUser_0.id == SelectedUserMenuQM.field_Private_IUser_0.prop_String_0);

            if (SelectedUser._vrcplayer.field_Private_ApiAvatar_0.releaseStatus == "public")
            {
                if (SelectedUser.prop_APIUser_0.allowAvatarCopying)
                {
                    ForceCloneText.text = "Clone Avatar";
                    ForceCloneButton.interactable = true;
                }
                else
                {
                    ForceCloneText.text = "ForceClone Avatar";
                    ForceCloneButton.interactable = true;
                }
            }
            else
            {
                ForceCloneText.text = "Private Avatar";
                ForceCloneButton.interactable = true;
            }
        }
    }
}
