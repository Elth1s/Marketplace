import {
    FilterGroupAction,
    FilterGroupActionTypes,
    FilterGroupState
} from "./types";

const initialState: FilterGroupState = {
    selectedFilterGroup: {
        name: "",
    },
    count: 0,
    filterGroups: []
}

export const filterGroupReducer = (state = initialState, action: FilterGroupAction): FilterGroupState => {
    switch (action.type) {
        case FilterGroupActionTypes.SEARCH_FILTER_GROUPS:
            return {
                ...state,
                count: action.payload.count,
                filterGroups: action.payload.values,
            }
        case FilterGroupActionTypes.GET_FILTER_GROUPS:
            return {
                ...state,
                filterGroups: action.payload,
            }
        case FilterGroupActionTypes.GET_BY_ID_FILTER_GROUP:
            return {
                ...state,
                selectedFilterGroup: action.payload,
            }
        case FilterGroupActionTypes.CREATE_FILTER_GROUP:
            return {
                ...state,
                ...action.payload
            }
        case FilterGroupActionTypes.UPDATE_FILTER_GROUP:
            return {
                ...state,
                ...action.payload
            }
        default:
            return state;
    }
}