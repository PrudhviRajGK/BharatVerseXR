using Assets.Convai.Scripts.Editor.Setup.LongTermMemory;
using Convai.Scripts.Editor.CustomPackage;
using Convai.Scripts.Editor.Setup.AccountsSection;
using Convai.Scripts.Editor.Setup.Documentation;
using Convai.Scripts.Editor.Setup.LoggerSettings;
using Convai.Scripts.Editor.Setup.ServerAnimation.View;
using Convai.Scripts.Editor.Setup.Updates;
using Convai.Scripts.Runtime.LoggerSystem;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Convai.Scripts.Editor.Setup
{

    public class ConvaiSDKSetupEditorWindow : EditorWindow
    {
        private const string STYLE_SHEET_PATH = "Assets/Convai/Art/UI/Editor/ConvaiSDKSetupWindow.uss";
        private const string VISUAL_TREE_PATH = "Assets/Convai/Art/UI/Editor/ConvaiSDKSetupWindow.uxml";
        private static VisualElement _contentContainer;
        private static VisualElement _root;
        private static readonly Dictionary<string, (VisualElement Section, Button Button)> Sections = new();

        private static readonly string[] SectionNames =
            { "welcome", "account", "package-management", "logger-settings", "updates", "documentation", "contact-us", "ltm", "server-anim" };

        private static readonly HashSet<string> ApiKeyDependentSections = new() { "logger-settings", "package-management", "ltm", "server-anim" };

        private static bool _isApiKeySet;

        public static bool IsCoreAPIAllowed = false;

        public static bool IsApiKeySet
        {
            get => _isApiKeySet;
            set
            {
                _isApiKeySet = value;
                OnAPIKeySet?.Invoke();
            }
        }

        private void CreateGUI()
        {
            InitializeUI();
            SetupNavigationHandlers();
        }

        public static event Action OnAPIKeySet;

        [MenuItem("Convai/Welcome", priority = 1)]
        public static void OpenWindow()
        {
            OpenSection("welcome");
        }

        [MenuItem("Convai/API Key Setup", priority = 2)]
        public static void OpenAPIKeySetup()
        {
            OpenSection("account");
        }

        [MenuItem("Convai/Logger Settings", priority = 4)]
        public static void OpenLoggerSettings()
        {
            OpenSection("logger-settings");
        }

        [MenuItem("Convai/Custom Package Installer", priority = 5)]
        public static void OpenCustomPackageInstaller()
        {
            OpenSection("package-management");
        }

        [MenuItem("Convai/Documentation", priority = 6)]
        public static void OpenDocumentation()
        {
            OpenSection("documentation");
        }

        [MenuItem("Convai/Long Term Memory", priority = 7)]
        public static void OpenLTM()
        {
            OpenSection("ltm");
        }

        [MenuItem("Convai/Server Animation", priority = 8)]
        public static void OpenServerAnimation()
        {
            OpenSection("server-anim");
        }

        private static void OpenSection(string sectionName)
        {
            Rect rect = new(100, 100, 1200, 550);
            ConvaiSDKSetupEditorWindow window = GetWindowWithRect<ConvaiSDKSetupEditorWindow>(rect, true, "Convai SDK Setup", true);
            window.minSize = window.maxSize = rect.size;
            window.Show();
            ShowSection(sectionName);
        }

        private void InitializeUI()
        {
            _root = rootVisualElement;
            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(VISUAL_TREE_PATH);
            _root.Add(visualTree.Instantiate());

            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(STYLE_SHEET_PATH);
            _root.styleSheets.Add(styleSheet);

            _contentContainer = _root.Q<VisualElement>("content-container");

            InitializeSections();

            _ = new APIKeySetupUI(_root);
            _ = new AccountInformationUI(_root);
            _ = new LoggerSettingsUI(_root);
            _ = new DocumentationUI(_root);
            _ = new UpdatesSectionUI(_root);
            _ = new ConvaiCustomPackageInstaller(_root);
            _ = new LongTermMemoryUI(_root);
            _ = new ServerAnimationPageView(_root);

            _root.Q<Button>("documentation-page").clicked += () => Application.OpenURL("https://docs.convai.com/api-docs/plugins-and-integrations/unity-plugin");
            _root.Q<Button>("download-meta-quest-app").clicked += () => Application.OpenURL("https://www.meta.com/en-gb/experiences/convai-animation-capture/28320335120898955/");
        }

        private static void InitializeSections()
        {
            foreach (string section in SectionNames)
                Sections[section] = (_contentContainer.Q(section), _root.Q<Button>($"{section}-btn"));
        }

        private static void SetupNavigationHandlers()
        {
            foreach (KeyValuePair<string, (VisualElement Section, Button Button)> section in Sections)
                section.Value.Button.clicked += () => ShowSection(section.Key);
        }

        public static void ShowSection(string sectionName)
        {
            if (!IsApiKeySet && ApiKeyDependentSections.Contains(sectionName))
            {
                EditorUtility.DisplayDialog("API Key Required", "Please set up your API Key to access this section.", "OK");
                return;
            }

            if (Sections.TryGetValue(sectionName, out (VisualElement Section, Button Button) sectionData))
            {
                foreach ((VisualElement Section, Button Button) section in Sections.Values)
                    section.Section.style.display = section.Section == sectionData.Section ? DisplayStyle.Flex : DisplayStyle.None;

                UpdateNavigationState(sectionName);
            }
            else
                ConvaiLogger.Warn($"Section '{sectionName}' not found.", ConvaiLogger.LogCategory.Character);
        }

        private static void UpdateNavigationState(string activeSectionName)
        {
            foreach (KeyValuePair<string, (VisualElement Section, Button Button)> section in Sections)
            {
                bool isActive = section.Key == activeSectionName;
                section.Value.Button.EnableInClassList("sidebar-link--active", isActive);
                section.Value.Button.EnableInClassList("sidebar-link", !isActive);
            }
        }
    }

}