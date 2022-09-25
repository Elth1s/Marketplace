import { OrderAction, OrderActionTypes, OrderState } from "./type";

const initialState: OrderState = {
    selectedOrder: {
        id: 0,
        consumerFirstName: "",
        consumerSecondName: "",
        consumerPhone: "",
        consumerEmail: "",
        orderStatusId: 0,
        orderStatusName: "",
        trackingNumber: "",
        deliveryType: "",
        city: "",
        department: "",
        totalPrice: 0,
        date: "",
        canUpdate: false,
        orderProductsResponse: []
    },
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
        case OrderActionTypes.GET_BY_ID:
            return {
                ...state,
                selectedOrder: action.payload,
            }
        default:
            return state;
    }
}