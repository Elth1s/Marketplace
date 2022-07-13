import {
    UserAction,
    UserActionTypes,
    UserState
} from "./types";

const initialState: UserState = {
    count: 0,
    users: []
}

export const userReducer = (state = initialState, action: UserAction): UserState => {
    switch (action.type) {
        case UserActionTypes.GET_USERS:
            return {
                ...state,
                users: action.payload,
            }
        case UserActionTypes.SEARCH_USERS:
            return {
                ...state,
                count: action.payload.count,
                users: action.payload.values,
            }
        default:
            return state;
    }
}