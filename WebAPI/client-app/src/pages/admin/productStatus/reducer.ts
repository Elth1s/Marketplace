import {
    ProductStatusAction,
    ProductStatusActionTypes,
    ProductStatusState
} from "./types";

const initialState: ProductStatusState = {
    selectedProductStatus: {
        name: "",
    },
    count: 0,
    productStatuses: []
}

export const productStatusReducer = (state = initialState, action: ProductStatusAction): ProductStatusState => {
    switch (action.type) {
        case ProductStatusActionTypes.GET_PRODUCT_STATUSES:
            return {
                ...state,
                productStatuses: action.payload,
            }
        case ProductStatusActionTypes.SEARCH_PRODUCT_STATUSES:
            return {
                ...state,
                count: action.payload.count,
                productStatuses: action.payload.values,
            }
        case ProductStatusActionTypes.GET_BY_ID_PRODUCT_STATUS:
            return {
                ...state,
                selectedProductStatus: action.payload,
            }
        case ProductStatusActionTypes.CREATE_PRODUCT_STATUS:
            return {
                ...state,
                ...action.payload
            }
        case ProductStatusActionTypes.UPDATE_PRODUCT_STATUS:
            return {
                ...state,
                ...action.payload
            }
        default:
            return state;
    }
}