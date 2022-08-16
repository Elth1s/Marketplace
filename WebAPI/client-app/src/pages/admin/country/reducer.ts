import {
    CountryAction,
    CountryActionTypes,
    CountryState
} from "./types";

const initialState: CountryState = {
    selectedCountry: {
        englishName: "",
        ukrainianName: "",
        code: ""
    },
    count: 0,
    countries: []
}

export const countryReducer = (state = initialState, action: CountryAction): CountryState => {
    switch (action.type) {
        case CountryActionTypes.GET_COUNTRIES:
            return {
                ...state,
                countries: action.payload,
            }
        case CountryActionTypes.SEARCH_COUNTRIES:
            return {
                ...state,
                count: action.payload.count,
                countries: action.payload.values,
            }
        case CountryActionTypes.GET_BY_ID_COUNTRY:
            return {
                ...state,
                selectedCountry: action.payload,
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