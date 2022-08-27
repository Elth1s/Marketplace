import { UIAction, UIActionTypes, UIState } from "./types";

const initialState: UIState = {
    isDarkTheme: false,
}

export const uiReducer = (state = initialState, action: UIAction): UIState => {
    switch (action.type) {
        case UIActionTypes.SET_THEME:
            return {
                ...state,
                isDarkTheme: action.payload,
            }
        default:
            return state;
    }
}