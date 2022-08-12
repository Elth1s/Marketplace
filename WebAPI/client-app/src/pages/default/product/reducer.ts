import { ProductAction, ProductActionTypes, ProductState, ReviewAction, ReviewActionTypes } from "./types";

const initialState: ProductState = {
    parents: [],
    product: {
        isInBasket: false,
        name: "",
        shopId: 0,
        shopName: "",
        productStatus: "",
        images: [],
        price: 0,
        filters: []
    },
    similarProducts: [],
    reviews: [],
    reviewsCount: 0
}

export const productReducer = (state = initialState, action: ProductAction | ReviewAction): ProductState => {
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
        case ReviewActionTypes.GET_REVIEWS:
            return {
                ...state,
                reviews: action.payload.values,
                reviewsCount: action.payload.count
            }
        case ReviewActionTypes.GET_MORE_REVIEWS:
            return {
                ...state,
                reviews: [...state.reviews, ...action.payload]
            }
        default:
            return state;
    }
}