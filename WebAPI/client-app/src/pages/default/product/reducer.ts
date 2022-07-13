import { ProductAction, ProductActionTypes, ProductState } from "./types";

const initialState: ProductState = {
    parents: [],
    product: {
        isInBasket: false,
        name: "",
        shopName: "",
        productStatus: "",
        images: [],
        price: 0,
        filters: []
    },
}

export const productReducer = (state = initialState, action: ProductAction): ProductState => {
    switch (action.type) {
        case ProductActionTypes.GET_PRODUCT_BY_URLSLUG:
            return {
                ...state,
                parents: action.payload.parents,
                product: action.payload.product,
            }
        case ProductActionTypes.UPDATE_SELECTED_PRODUCT:
            return {
                ...state,
                product: {
                    ...state.product,
                    isInBasket: !state.product.isInBasket
                }
            }
        default:
            return state;
    }
}