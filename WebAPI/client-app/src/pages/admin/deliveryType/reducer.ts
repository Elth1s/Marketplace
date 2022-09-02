import { DeliveryTypeAction, DeliveryTypeActionTypes, DeliveryTypeState } from "./types";

const initialState: DeliveryTypeState = {
    selectedDeliveryType: {
        englishName: "",
        ukrainianName: "",
        darkIcon: "",
        lightIcon: "",
    },
    deliveryTypes: [],
    deliveryTypesForSelect: [],
    count: 0,
}

export const deliveryTypeReducer = (state = initialState, action: DeliveryTypeAction): DeliveryTypeState => {
    switch (action.type) {
        case DeliveryTypeActionTypes.GET_DELIVERY_TYPES:
            return {
                ...state,
                deliveryTypes: action.payload,
            }
        case DeliveryTypeActionTypes.SEARCH_DELIVERY_TYPES:
            return {
                ...state,
                deliveryTypes: action.payload.values,
                count: action.payload.count,
            }
        case DeliveryTypeActionTypes.GET_DELIVERY_TYPES_FOR_SELECT:
            return {
                ...state,
                deliveryTypesForSelect: action.payload,
            }
        case DeliveryTypeActionTypes.GET_BY_ID_DELIVERY_TYPES:
            return {
                ...state,
                selectedDeliveryType: action.payload,
            }
        case DeliveryTypeActionTypes.CREATE_DELIVERY_TYPE:
            return {
                ...state,
                ...action.payload
            }
        case DeliveryTypeActionTypes.UPDATE_DELIVERY_TYPE:
            return {
                ...state,
                ...action.payload
            }
        default:
            return state;
    }
}