import {
    CountryAction,
    CountryActionTypes,
    CountryState
} from "./types";

const initialState: CountryState = {
    countryInfo: {
        id: 0,
        name: "",
    },
    countries: []
}

export const countryReducer = (state = initialState, action: CountryAction): CountryState => {
    switch (action.type) {
        case CountryActionTypes.GET_COUNTRIES:
            return {
                ...state,
                countries: action.payload,
            }
        case CountryActionTypes.GET_BY_ID_COUNTRY:
            return {
                ...state,
                countryInfo: action.payload,
            }
        case CountryActionTypes.CREATE_COUNTRY:
            return {
                ...state,
                ...action.payload
            }
        case CountryActionTypes.UPDATE_COUNTRY:
            return {
                ...state,
                ...action.payload
            }
        default:
            return state;
    }
}