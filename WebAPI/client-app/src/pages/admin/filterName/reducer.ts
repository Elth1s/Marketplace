import {
    FilterNameAction,
    FilterNameActionTypes,
    FilterNameState
} from "./types";

const initialState: FilterNameState = {
    selectedFilterName: {
        englishName: "",
        ukrainianName: "",
        filterGroupId: 0,
        unitId: 0,
    },
    count: 0,
    filterNames: []
}

export const filterNameReducer = (state = initialState, action: FilterNameAction): FilterNameState => {
    switch (action.type) {
        case FilterNameActionTypes.GET_FILTER_NAMES:
            return {
                ...state,
                filterNames: action.payload,
            }
        case FilterNameActionTypes.SEARCH_FILTER_NAMES:
            return {
                ...state,
                count: action.payload.count,
                filterNames: action.payload.values,
            }
        case FilterNameActionTypes.GET_BY_ID_FILTER_NAME:
            return {
                ...state,
                selectedFilterName: action.payload,
            }
        case FilterNameActionTypes.CREATE_FILTER_NAME:
            return {
                ...state,
                ...action.payload
            }
        case FilterNameActionTypes.UPDATE_FILTER_NAME:
            return {
                ...state,
                ...action.payload
            }
        default:
            return state;
    }
}