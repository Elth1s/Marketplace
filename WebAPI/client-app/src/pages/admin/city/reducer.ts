import {
    CityAction,
    CityActionTypes,
    CityState
} from "./types";

const initialState: CityState = {
    cityInfo: {
        id: 0,
        name: "",
        countryName: "",
    },
    cities: []
}

export const cityReducer = (state = initialState, action: CityAction): CityState => {
    switch (action.type) {
        case CityActionTypes.GET_CITIES:
            return {
                ...state,
                cities: action.payload,
            }
        case CityActionTypes.GET_BY_ID_CITY:
            return {
                ...state,
                cityInfo: action.payload,
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