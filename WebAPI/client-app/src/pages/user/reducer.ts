import {
    ConfirmEmailActionTypes,
    EmailConfirmAction,
    ProfileAction,
    ProfileActionTypes,
    ProfileState,
} from "./types";

const initialState: ProfileState = {
    userInfo: {
        firstName: "",
        secondName: "",
        photo: "",
        isEmailConfirmed: false,
        isPhoneConfirmed: false,
        hasPassword: false
    }
}

export const profileReducer = (state = initialState, action: ProfileAction | EmailConfirmAction): ProfileState => {
    switch (action.type) {
        case ProfileActionTypes.GET_PROFILE:
            return {
                ...state,
                userInfo: action.payload,
            }
        case ConfirmEmailActionTypes.IS_EMAIL_CONFIRMED:
            return {
                ...state,
                userInfo: {
                    ...state.userInfo,
                    isEmailConfirmed: action.payload
                }
            }
        default:
            return state;
    }
}