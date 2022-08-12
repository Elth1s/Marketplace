import 'react-i18next';
import ua from '../translation/ua/ua.json'

declare module 'react-i18next' {
    interface CustomTypeOptions {
        defaultNS: "common",
        resources: typeof ua;
    }
}