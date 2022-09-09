import { OrderAction, OrderActionTypes, OrderState } from "./type";

const initialState: OrderState = {
    count: 0,
    orders: []
}

export const orderReducer = (state = initialState, action: OrderAction): OrderState => {
    switch (action.type) {
        case OrderActionTypes.SEARCH_ORDERS:
            return {
                ...state,
                count: action.payload.count,
                orders: action.payload.values,
            }
        default:
            return state;
    }
}