import {
    CityAction,
    CityActionTypes,
    CityState
} from "./types";

const initialState: CityState = {
    selectedCity: {
        englishName: "",
        ukrainianName: "",
        countryId: 0,
    },
    count: 0,
    cities: []
}

export const cityReducer = (state = initialState, action: CityAction): CityState => {
    switch (action.type) {
        case CityActionTypes.GET_CITIES:
            return {
                ...state,
                cities: action.payload,
            }
        case CityActionTypes.SEARCH_CITIES:
            return {
                ...state,
                count: action.payload.count,
                cities: action.payload.values,
            }
        case CityActionTypes.GET_BY_ID_CITY:
            return {
                ...state,
                selectedCity: action.payload,
            }
        case CityActionTypes.CREATE_CITY:
            return {
                ...state,
                ...action.payload
            }
        case CityActionTypes.UPDATE_CITY:
            return {
                ...state,
                ...action.payload
            }
        default:
            return state;
    }
}