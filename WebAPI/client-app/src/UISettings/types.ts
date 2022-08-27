export enum UIActionTypes {
    SET_THEME = "SET_THEME",
}

export interface SetThemeAction {
    type: UIActionTypes.SET_THEME,
    payload: boolean
}

export interface UIState {
    isDarkTheme: boolean
}

export type UIAction = SetThemeAction;
