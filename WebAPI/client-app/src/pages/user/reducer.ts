import {
    ConfirmEmailActionTypes,
    EmailConfirmAction,
    GenderAction,
    GenderActionTypes,
    OrderAction,
    OrderActionTypes,
    ProfileAction,
    ProfileActionTypes,
    ProfileState,
} from "./types";

const initialState: ProfileState = {
    userInfo: {
        firstName: "",
        secondName: "",

        // birthDate: "",
        // languageOfCommunication: 0,
        genderId: null,
        countryId: null,
        cityId: null,
        address: "",
        postalCode: "",

        email: "",
        phone: "",

        photo: "",
        isEmailConfirmed: false,
        isPhoneConfirmed: false,
        isFacebookConnected: false,
        isGoogleConnected: false,
        hasPassword: false,
    },
    orderProducts: [],
    ordersForUser: [],
    genders: []
}

export const profileReducer = (state = initialState, action: ProfileAction | EmailConfirmAction | OrderAction | GenderAction): ProfileState => {
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
        case ProfileActionTypes.GOOGLE_CONNECT_SUCCESS:
            return {
                ...state,
                userInfo: {
                    ...state.userInfo,
                    isGoogleConnected: true
                },
            }
        case ProfileActionTypes.FACEBOOK_CONNECT_SUCCESS:
            return {
                ...state,
                userInfo: {
                    ...state.userInfo,
                    isFacebookConnected: true
                },
            }
        case OrderActionTypes.GET_ORDER_PRODUCTS:
            return {
                ...state,
                orderProducts: action.payload,
            }
        case OrderActionTypes.GET_ORDER_FOR_USER:
            return {
                ...state,
                ordersForUser: action.payload,
            }
        case GenderActionTypes.GET_GENDERS:
            return {
                ...state,
                genders: action.payload,
            }
        default:
            return state;
    }
}

function newFunction(): Date | null {
    return null;
}
