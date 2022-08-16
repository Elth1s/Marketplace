import {
    OrderStatusAction,
    OrderStatusActionTypes,
    OrderStatusState
} from "./types";

const initialState: OrderStatusState = {
    selectedOrderStatus: {
        englishName: "",
        ukrainianName: "",
    },
    count: 0,
    orderStatuses: []
}

export const orderStatusReducer = (state = initialState, action: OrderStatusAction): OrderStatusState => {
    switch (action.type) {
        case OrderStatusActionTypes.GET_ORDER_STATUSES:
            return {
                ...state,
                orderStatuses: action.payload,
            }
        case OrderStatusActionTypes.SEARCH_ORDER_STATUSES:
            return {
                ...state,
                count: action.payload.count,
                orderStatuses: action.payload.values,
            }
        case OrderStatusActionTypes.GET_BY_ID_ORDER_STATUS:
            return {
                ...state,
                selectedOrderStatus: action.payload,
            }
        case OrderStatusActionTypes.CREATE_ORDER_STATUS:
            return {
                ...state,
                ...action.payload
            }
        case OrderStatusActionTypes.UPDATE_ORDER_STATUS:
            return {
                ...state,
                ...action.payload
            }
        default:
            return state;
    }
}