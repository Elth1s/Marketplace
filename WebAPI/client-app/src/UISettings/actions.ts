import { Dispatch } from "react"
import { UIAction, UIActionTypes } from "./types"

export const SetTheme = (darkTheme: boolean) => {
    return async (dispatch: Dispatch<UIAction>) => {
        dispatch({
            type: UIActionTypes.SET_THEME,
            payload: darkTheme
        })
    }
}