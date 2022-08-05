import {
    ProductAction,
    ProductActionTypes,
    ProductState
} from "./types";

const initialState: ProductState = {
    count: 0,
    products: [],
    productStatuses: [],
    categories: [],
}

export const productSellerReducer = (state = initialState, action: ProductAction): ProductState => {
    switch (action.type) {
        case ProductActionTypes.GET_PRODUCTS:
            return {
                ...state,
                products: action.payload,
            }
        case ProductActionTypes.GET_PRODUCT_STATUSES:
            return {
                ...state,
                productStatuses: action.payload,
            }
        case ProductActionTypes.GET_CATEGORIES_WITHOUT_CHILDREN:
            return {
                ...state,
                categories: action.payload,
            }
        case ProductActionTypes.SEARCH_PRODUCTS:
            return {
                ...state,
                count: action.payload.count,
                products: action.payload.values,
            }
        default:
            return state;
    }
}