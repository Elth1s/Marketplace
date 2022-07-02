import {
    UnitAction,
    UnitActionTypes,
    UnitState
} from "./types";

const initialState: UnitState = {
    selectedUnit: {
        measure: ""
    },
    count: 0,
    units: []
}

export const unitReducer = (state = initialState, action: UnitAction): UnitState => {
    switch (action.type) {
        case UnitActionTypes.GET_UNITS:
            return {
                ...state,
                units: action.payload,
            }
        case UnitActionTypes.SEARCH_UNITS:
            return {
                ...state,
                count: action.payload.count,
                units: action.payload.values,
            }
        case UnitActionTypes.GET_BY_ID_UNIT:
            return {
                ...state,
                selectedUnit: action.payload,
            }
        case UnitActionTypes.CREATE_UNIT:
            return {
                ...state,
                ...action.payload
            }
        case UnitActionTypes.UPDATE_UNIT:
            return {
                ...state,
                ...action.payload
            }
        default:
            return state;
    }
}