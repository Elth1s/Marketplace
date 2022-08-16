import {
    FilterValueAction,
    FilterValueActionTypes,
    FilterValueState
} from "./types";

const initialState: FilterValueState = {
    selectedFilterValue: {
        englishValue: "",
        ukrainianValue: "",
        filterNameId: 0,
        min: "",
        max: ""
    },
    count: 0,
    filterValues: []
}

export const filterValueReducer = (state = initialState, action: FilterValueAction): FilterValueState => {
    switch (action.type) {
        case FilterValueActionTypes.GET_FILTER_VALUES:
            return {
                ...state,
                filterValues: action.payload,
            }
        case FilterValueActionTypes.SEARCH_FILTER_VALUE:
            return {
                ...state,
                count: action.payload.count,
                filterValues: action.payload.values,
            }
        case FilterValueActionTypes.GET_BY_ID_FILTER_VALUE:
            return {
                ...state,
                selectedFilterValue: {
                    englishValue: action.payload.englishValue,
                    ukrainianValue: action.payload.ukrainianValue,
                    filterNameId: action.payload.filterNameId,
                    min: action.payload.min == null ? "" : action.payload.min,
                    max: action.payload.max == null ? "" : action.payload.max,
                },
            }
        case FilterValueActionTypes.CREATE_FILTER_VALUE:
            return {
                ...state,
                ...action.payload
            }
        case FilterValueActionTypes.UPDATE_FILTER_VALUE:
            return {
                ...state,
                ...action.payload
            }
        default:
            return state;
    }
}