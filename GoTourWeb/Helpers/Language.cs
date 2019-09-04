using GoTourWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace GoTourWeb.Helpers {
    public static class Language {

        public static string vLanguageGeneral = @"\LanguageFiles\language.json";
        public static string vLanguageMessage = @"\LanguageFiles\messageLang.json";
        private static string ReadFile(string valJson) {
            string vResult = string.Empty;
            try {
                string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location).Replace("\\bin\\Debug\\netcoreapp2.1", "");
                string fullPath = string.Empty;
                fullPath = appPath + valJson;
                if (!string.IsNullOrEmpty(fullPath)) {
                    vResult = System.IO.File.ReadAllText(fullPath, Encoding.UTF8);
                    return vResult;
                }
                return vResult;

            } catch (Exception) {
                return vResult;

            }
        }
        private static string ReadFileHTML(string valJson) {
            string vResult = string.Empty;
            try {
                string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location).Replace("\\bin\\Debug\\netcoreapp2.1", "");
                string fullPath = string.Empty;
                fullPath = appPath + valJson;
                if (!string.IsNullOrEmpty(fullPath)) {
                    vResult = System.IO.File.ReadAllText(fullPath, Encoding.UTF8);
                    return vResult;
                }
                return vResult;

            } catch (Exception) {
                return vResult;

            }
        }

        public static void MessageLangRead() {
            string vJson = ReadFile(vLanguageMessage);
            LanguageMessageViewModel vResult = null;
            vResult = JsonConvert.DeserializeObject<LanguageMessageViewModel>(vJson);
            Startup._vViewMessLang = vResult;
        }

        public static string GetMessageIn(string valNameView, string valValueResponse, string valLanguage, string valKey) {
            string vResult = string.Empty;
            ViewTextMessageModel vTextMessage = Startup._vViewMessLang.Messages.FirstOrDefault(x => x.NameView == valNameView);
            if (vTextMessage != null) {
                ViewContentModel vContentModel = vTextMessage.Content.FirstOrDefault(x => x.ValueResponse == valValueResponse);
                if (vContentModel != null) {
                    LanguageContentViewModel vLanguageContent = vContentModel.LanguageContent.FirstOrDefault(x => x.Language.ToUpper() == valLanguage);
                    if (vLanguageContent != null) {
                        KeyValuePair<string, string> vText = new KeyValuePair<string, string>();
                        foreach (var item in vLanguageContent.ListText) {
                            if (item.Select(i => i.Key == valKey).FirstOrDefault()) {
                                vText = item.FirstOrDefault(x => x.Key == valKey);
                            }
                        }
                        return vText.Value;
                    }
                }
            }

            return vResult;
        }
        public static void LanguageViewRead() {
            string vJson = ReadFileHTML(vLanguageGeneral);
            ViewLanguajeViewModel vResult = null;
            try {
                if (!string.IsNullOrEmpty(vJson)) {
                    vResult = JsonConvert.DeserializeObject<ViewLanguajeViewModel>(vJson);
                    Startup._vViewInfo = vResult;
                }
            } catch (Exception) {
                Startup._vViewInfo = vResult;
            }
        }

        public static string GetTextView(string valNameView, string valKey, string valLang) {
            string vResult = string.Empty;
            ViewTextViewModel vTextView = Startup._vViewInfo.ViewText.FirstOrDefault(x => x.NameView == valNameView);
            if (vTextView != null) {
                KeyValuePair<string, List<Dictionary<string, string>>> vElement = new KeyValuePair<string, List<Dictionary<string, string>>>();
                foreach (Dictionary<string, List<Dictionary<string, string>>> vItem in vTextView.Elements) {
                    if (vItem.Select(i => i.Key == valKey).FirstOrDefault()) {
                        vElement = vItem.FirstOrDefault(i => i.Key == valKey);
                        break;
                    }
                }
                List<Dictionary<string, string>> vItemElement = vElement.Value;
                KeyValuePair<string, string> vElementLanguage = new KeyValuePair<string, string>();
                foreach (Dictionary<string, string> vItem in vItemElement) {
                    if (vItem.Select(i => i.Key.ToUpper() == valLang).FirstOrDefault()) {
                        vElementLanguage = vItem.FirstOrDefault(i => i.Key.ToUpper() == valLang);
                        break;
                    }
                }

                vResult = vElementLanguage.Value;
            }
            return vResult;
        }


    }
}
