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
    similarProducts: []
}

export const productReducer = (state = initialState, action: ProductAction): ProductState => {
    switch (action.type) {
        case ProductActionTypes.GET_PRODUCT_BY_URLSLUG:
            return {
                ...state,
                parents: action.payload.parents,
                product: action.payload.product,
            }
        case ProductActionTypes.GET_SIMILAR_PRODUCTS:
            return {
                ...state,
                similarProducts: action.payload,
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