import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import LanguageDetector from "i18next-browser-languagedetector";
import en from "./en/en.json";
import ua from "./ua/ua.json"

const resources = {
    en,
    ua
}

export const availableLanguages = Object.keys(resources)

i18n.use(initReactI18next)
    .use(LanguageDetector)
    .init({
        resources,
        defaultNS: "common",
        fallbackLng: "ua",
    });