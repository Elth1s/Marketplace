import {
    FilterValueAction,
    FilterValueActionTypes,
    FilterValueState
} from "./types";

const initialState: FilterValueState = {
    selectedFilterValue: {
        value: "",
        filterNameId: 0,
        min: undefined,
        max: undefined
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
                selectedFilterValue: action.payload,
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