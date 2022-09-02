import {
    AuthAction,
    AuthActionTypes,
    AuthState,
} from "./types"

const initialState: AuthState = {
    isAuthDialogOpen: false,
    user: {
        name: "",
        emailOrPhone: "",
        isEmailExist: false,
        role: "",
        photo: ""
    },
    isAuth: false,
}

export const authReducer = (state = initialState, action: AuthAction): AuthState => {
    switch (action.type) {
        case AuthActionTypes.AUTH_DIALOG_CHANGE:
            return {
                ...state,
                isAuthDialogOpen: !state.isAuthDialogOpen,
            }
        case AuthActionTypes.AUTH_SUCCESS:
            return {
                ...state,
                user: action.payload,
                isAuth: true
            }
        case AuthActionTypes.AUTH_LOGOUT:
            return {
                ...state,
                user: {
                    name: "",
                    emailOrPhone: "",
                    isEmailExist: false,
                    role: "",
                    photo: "",
                },
                isAuth: false
            }
        default:
            return state;
    }
}