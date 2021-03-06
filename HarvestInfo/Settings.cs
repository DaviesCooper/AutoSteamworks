﻿using System;
using System.Configuration;
using System.Linq;
using HarvestInfo.Core.Utility;
using Keystroke.API;

namespace HarvestInfo
{
    public static class Settings
    {
        public static string SupportedGameVersion = "410013";

        public static ulong Off_Base = 0x140000000;
        public static ulong Off_SteamworksCombo = 0x4EA1F60;

        public static ulong Off_SaveData = 0x4F2EAF0;
        public static ulong Off_DiffSlot = 0x27E9F0; // start of each save slot data slotnr * off


        private static bool _isLogEnabled = false;
        public static bool EnableLog
        {
            get
            {
#if DEBUG
                return true;
#else
                if (ConfigurationManager.AppSettings.AllKeys.Any(key => key == "IsLogEnabled"))
                {
                    return bool.TryParse(ConfigurationManager.AppSettings["IsLogEnabled"].Trim(), out _isLogEnabled) ?
                        _isLogEnabled :
                        (_isLogEnabled = false);
                }

                return _isLogEnabled;
#endif
            }
        }

        private static int _keyCodeShow = -1;
        public static int KeyCodeToggle
        {
            get
            {
                if (_keyCodeShow != -1) { return _keyCodeShow; }

                if (ConfigurationManager.AppSettings.AllKeys.Any(key => key == "KeyCodeShow"))
                {
                    if (int.TryParse(ConfigurationManager.AppSettings["KeyCodeShow"].Trim(), out _keyCodeShow))
                    {
                        try
                        {
                            KeyCode key = (KeyCode)_keyCodeShow;

                            return _keyCodeShow;
                        }
                        catch (Exception ex)
                        {
                            Logger.LogError($"Invalid number for 'Show' keycode: [{_keyCodeShow}]. Will use default instead! Exception: {ex.Message}");
                        }
                    }
                    else
                    {
                        Logger.LogError($"Invalid number for 'Show' keycode: [{ConfigurationManager.AppSettings["KeyCodeShow"]}]. Will use default instead!");
                    }
                }

                return (_keyCodeShow = 45);
            }
        }

        private static int _keyCodeExit = -1;
        public static int KeyCodeExit
        {
            get
            {
                if (_keyCodeExit != -1) { return _keyCodeExit; }

                if (ConfigurationManager.AppSettings.AllKeys.Any(key => key == "KeyCodeExit"))
                {
                    if (int.TryParse(ConfigurationManager.AppSettings["KeyCodeExit"].Trim(), out _keyCodeExit))
                    {
                        try
                        {
                            KeyCode key = (KeyCode)_keyCodeExit;

                            return _keyCodeExit;
                        }
                        catch (Exception ex)
                        {
                            Logger.LogError($"Invalid number for 'Hide' keycode: [{_keyCodeExit}]. Will use default instead! Exception: {ex.Message}");
                        }
                    }
                }
                else
                {
                    Logger.LogError($"Invalid number for 'Hide' keycode: [{ConfigurationManager.AppSettings["KeyCodeExit"]}]. Will use default instead!");
                }

                return (_keyCodeExit = 27);
            }
        }

        private static int _thresholdDuration = 4;
        public static int ThresholdDuration
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Any(key => key == "WarnWhenDurationLowerThan"))
                {
                    return int.TryParse(ConfigurationManager.AppSettings["WarnWhenDurationLowerThan"].Trim(), out _thresholdDuration) ?
                        _thresholdDuration :
                        (_thresholdDuration = 4);
                }

                return _thresholdDuration;
            }
        }

        private static int _thresholdHarvest = 20;
        public static int ThresholdHarvest
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Any(key => key == "WarnWhenBoxItemsHigherThan"))
                {
                    return int.TryParse(ConfigurationManager.AppSettings["WarnWhenBoxItemsHigherThan"].Trim(), out _thresholdHarvest) ?
                        _thresholdHarvest :
                        (_thresholdHarvest = 20);
                }

                return _thresholdHarvest;
            }
        }

        private static int _checkDelay = 5;
        public static int Delay
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Any(key => key == "CheckDelay"))
                {
                    _checkDelay = int.TryParse(ConfigurationManager.AppSettings["CheckDelay"].Trim(), out _checkDelay) ?
                        _checkDelay :
                        (_checkDelay = 5);
                }

                return _checkDelay * 1000;
            }
        }

        private static int _x = 0;
        public static int X
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Any(key => key == "Position_X"))
                {
                        _x = int.TryParse(ConfigurationManager.AppSettings["Position_X"].Trim(), out _x) ?
                                _x :
                                (_x = 0);
                }

                return _x;
            }
        }

        private static int _y = 0;
        public static int Y
        {
            get
            {
                if (ConfigurationManager.AppSettings.AllKeys.Any(key => key == "Position_Y"))
                {
                    _y = int.TryParse(ConfigurationManager.AppSettings["Position_Y"].Trim(), out _y) ?
                            _y :
                            (_y = 0);
                }

                return _y;
            }
        }
        public static Overlay OverlaySettings { get; set; } = new Overlay();

        // Config template

        public class Overlay
        {
            public bool Enabled { get; set; } = true;
            public int[] Position { get; set; } = new int[2] { X, Y };
        }
    }
}